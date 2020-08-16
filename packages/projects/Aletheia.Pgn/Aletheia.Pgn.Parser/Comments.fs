module Aletheia.Pgn.Parser.Comments

open FParsec
open System.Runtime.InteropServices

type AnnotationPart = {
    Text: string
}

type CommandPart = {
    CommandName: string
    Operands: string list
}

type CommentPart =
    | Annotation of AnnotationPart
    | Command of CommandPart

type CommentPart with
  member x.TryAsAnnotation([<Out>] annotation:byref<AnnotationPart>) =
    match x with
    | Annotation value -> annotation <- value; true
    | _ -> false
  member x.TryAsCommand([<Out>] command:byref<CommandPart>) =
    match x with
    | Command value -> command <- value; true
    | _ -> false

let commandText s = pstring s
let commandTextParser : Parser<string,unit> =
    let escape =  anyChar
                  |>> function
                      | 'b' -> "\b"
                      | 'f' -> "\u000C"
                      | 'n' -> "\n"
                      | 'r' -> "\r"
                      | 't' -> "\t"
                      | '[' -> "["
                      | '%' -> "%"
                      | ']' -> "]"
                      | c   -> string "\\" + c.ToString() // every other char is mapped to itself

    let unicodeEscape =
        /// converts a hex char ([0-9a-fA-F]) to its integer number (0-15)
        let hex2int c = (int c &&& 15) + (int c >>> 6)*9

        commandText "u" >>. pipe4 hex hex hex hex (fun h3 h2 h1 h0 ->
            (hex2int h3)*4096 + (hex2int h2)*256 + (hex2int h1)*16 + hex2int h0
            |> char |> string
        )

    let escapedCharSnippet = commandText "\\" >>. (escape <|> unicodeEscape)
    let normalCharSnippet  = manySatisfy (fun c -> c <> '[' && c <> ']' && c <> '%' && c <> '\\')

    between (commandText "[%") (commandText "]")
            (stringsSepBy1 normalCharSnippet escapedCharSnippet)

let commandPartParser = commandTextParser |>> fun text ->
    let splitParts = text.Split(' ') |> Array.toList
    CommentPart.Command({
        CommandName=splitParts.Head
        Operands=splitParts.Tail
    })

let annotationPartParser =
    many1Satisfy (fun c -> c <> '[') |>>
    fun text ->
        CommentPart.Annotation({
            Text=text
        })

let commentPartParser = commandPartParser <|> annotationPartParser

let commentPartsParser = many commentPartParser

let parseCommentText commentText =
    match run commentPartsParser commentText with
    | Success (res, _, _) -> res
    | Failure (_) ->
        let fallbackAnnotation = CommentPart.Annotation({
            Text=commentText
        })
        [ fallbackAnnotation ]
