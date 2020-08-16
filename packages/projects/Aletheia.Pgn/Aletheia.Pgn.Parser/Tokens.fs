module Aletheia.Pgn.Parser.Tokens

open System.Runtime.InteropServices
open FParsec
open Aletheia.Pgn.Parser.Comments
open Aletheia.Pgn.Parser.Configuration
open Aletheia.Pgn.Parser.Notation

type NagToken = {
    Number: int
}

type CommentToken = {
    Parts: CommentPart list
}

type PlyToken = {
    San: string
}

type MoveNumberToken = {
    Number: int
}

type BlackMoveNumberToken = {
    Number: int
}

type ResultToken = WhiteWins | BlackWins | Draw | Unspecified

type AngleToken = {
    Text: string
}

type NullToken = {
    NullMoveText: string
}

type LineToken = {
    Line: Token list
}

and Token =
    | Comment of CommentToken
    | Line of LineToken
    | MoveNumber of MoveNumberToken
    | Nag of NagToken
    | BlackMoveNumber of BlackMoveNumberToken
    | Ply of PlyToken
    | Null of NullToken
    | Angle of AngleToken
    | Result of ResultToken

type Token with
  member x.TryAsComment([<Out>] comment:byref<CommentToken>) =
    match x with
    | Comment value -> comment <- value; true
    | _ -> false
  member x.TryAsLine([<Out>] line:byref<LineToken>) =
    match x with
    | Line value -> line <- value; true
    | _ -> false
  member x.TryAsMoveNumber([<Out>] moveNumber:byref<MoveNumberToken>) =
    match x with
    | MoveNumber value -> moveNumber <- value; true
    | _ -> false
  member x.TryAsNag([<Out>] nag:byref<NagToken>) =
    match x with
    | Nag value -> nag <- value; true
    | _ -> false
  member x.TryAsBlackMoveNumber([<Out>] blackMoveNumber:byref<BlackMoveNumberToken>) =
    match x with
    | BlackMoveNumber value -> blackMoveNumber <- value; true
    | _ -> false
  member x.TryAsNull([<Out>] nullToken:byref<NullToken>) =
    match x with
    | Null value -> nullToken <- value; true
    | _ -> false
  member x.TryAsPly([<Out>] ply:byref<PlyToken>) =
    match x with
    | Ply value -> ply <- value; true
    | _ -> false
  member x.TryAsAngle([<Out>] angle:byref<AngleToken>) =
    match x with
    | Angle value -> angle <- value; true
    | _ -> false
  member x.TryAsResult([<Out>] result:byref<ResultToken>) =
    match x with
    | Result value -> result <- value; true
    | _ -> false

type GameState = ParserConfiguration
let commentStr s = pstring s
let normalCommentParser =
    let str s = pstring s
    let normalCharSnippet = manySatisfy (fun c -> c <> '\\' && c <> '}')
    let escapedChar = str "\\" >>. (anyChar |>> function
                                                        | 'n' -> "\n"
                                                        | 'r' -> "\r"
                                                        | 't' -> "\t"
                                                        | c   -> string ('\\' + c))
    between (str "{") (str "}")
            (stringsSepBy normalCharSnippet escapedChar)

let semicolonCommentParser =
    skipChar ';' >>.
    restOfLine true |>>
    fun commentTxt ->
        let parts = parseCommentText commentTxt
        Token.Comment({
            Parts=parts
        })

let commentTokenParser =
    normalCommentParser |>>
    fun txt ->
        let parts = parseCommentText txt
        Token.Comment({
            Parts=parts
        })

let nagTokenParser = skipChar '$' >>. pint32 |>> fun num -> Token.Nag({
    Number = num
})

let period = skipChar '.'
let triplePeriods = skipString "..."
let moveNumberParser = pint32 .>> period
let blackMoveNumberParser = pint32 .>>? triplePeriods

let moveNumberTokenParser = moveNumberParser |>> fun mn -> Token.MoveNumber({
    Number = mn
})

let blackMoveNumberTokenParser = blackMoveNumberParser |>> fun mn -> Token.BlackMoveNumber({    Number = mn
})

let nullMoveSymbols = [
    "--" // Used by SCID and 'other software'
    "{--}" // Optional format used by SCID
    "Z0" // Used by ChessBase sometimes
    "0000" // UCI
    "@@@@" // WinBoard
    "pass" // WinBoard
    "null" // WinBoard
]
let mapper nullMoveString = pstring nullMoveString
let vals = nullMoveSymbols |> List.map mapper
let nullTokenParser = choice(vals) |>> fun str -> Token.Null({
    NullMoveText=str
})

let whiteWinsParser = skipString "1-0" |>> fun _ -> Token.Result(ResultToken.WhiteWins)
let blackWinsParser = skipString "0-1" |>> fun _ -> Token.Result(ResultToken.BlackWins)
let drawParser = skipString "1/2-1/2" |>> fun _ -> Token.Result(ResultToken.Draw)
let unspecifiedParser = skipChar '*' |>> fun _ -> Token.Result(ResultToken.Unspecified)
let resultParser = whiteWinsParser <|> blackWinsParser <|> drawParser <|> unspecifiedParser

let nonCastlingMoveParser : Parser<string, ParserConfiguration> =
    fun text ->
            let charsets = getUserState(text).Result.Charsets
            let parser = notationParserForCharsets charsets
            parser(text)
let castlingMoveParser = regex "O-O(-O)?\+?#?"
let moveTokenParser = (nonCastlingMoveParser <|> castlingMoveParser) |>> fun moveStr -> Token.Ply({
    San = moveStr
})

let lineEndParser = skipChar ')'


let createParserForwardedToRef config =
    let dummyParser = fun stream -> failwith "a parser created with createParserForwardedToRef was not initialized"
    let r = ref dummyParser
    (fun stream -> !r stream), r : Parser<_,'u> * Parser<_,'u> ref

let lineParser, lineParserImpl = createParserForwardedToRef()

let anyTokenParser = choice [
    semicolonCommentParser
    nullTokenParser
    resultParser
    commentTokenParser
    nagTokenParser
    blackMoveNumberTokenParser
    moveNumberTokenParser
    moveTokenParser
    lineParser
]
let tokenParser : Parser<Token, GameState> = anyTokenParser <?> "Some kind of token start character"


do lineParserImpl := skipChar '('
    >>. spaces
    >>. sepEndBy tokenParser spaces
    .>> lineEndParser
    |>> fun tl -> Token.Line({
        Line = tl
    })

let multipleTokenParser = sepEndBy tokenParser spaces