module internal Aletheia.Pgn.Parser.Header

open System
open Aletheia.Pgn.Parser.Configuration
open FParsec

let openBracket : Parser<unit, unit> = skipChar '['
let doubleQuote = '\"'
let nameEndWhen = nextCharSatisfies (fun x -> x = '"')
let headerPropName =
    many1CharsTill anyChar nameEndWhen |>> fun s -> s.TrimEnd()

let str s = pstring s
let stringLiteral =
    let escape =  anyChar
                  |>> function
                      | 'b' -> "\b"
                      | 'f' -> "\u000C"
                      | 'n' -> "\n"
                      | 'r' -> "\r"
                      | 't' -> "\t"
                      | '"' -> "\""
                      | c   -> string "\\" + c.ToString() // every other char is mapped to itself

    let unicodeEscape =
        /// converts a hex char ([0-9a-fA-F]) to its integer number (0-15)
        let hex2int c = (int c &&& 15) + (int c >>> 6)*9

        str "u" >>. pipe4 hex hex hex hex (fun h3 h2 h1 h0 ->
            (hex2int h3)*4096 + (hex2int h2)*256 + (hex2int h1)*16 + hex2int h0
            |> char |> string
        )

    let escapedCharSnippet = str "\\" >>. (unicodeEscape <|> escape)
    let normalCharSnippet  = manySatisfy (fun c -> c <> '"' && c <> '\\')

    between (str "\"") (str "\"")
            (stringsSepBy normalCharSnippet escapedCharSnippet)

let headerLine =
    pchar '[' >>.
    tuple2 headerPropName stringLiteral .>>
    pchar ']'

let headerStanza : Parser<(string*string) list, ParserConfiguration> =
    spaces >>.
    sepEndBy headerLine skipNewline .>>
    spaces

let parseHeaderStanza headerText config =
    match (runParserOnString headerStanza config "header" headerText) with
    | Success (res, _, _) -> Result.Ok res
    | Failure (err, _, _) -> Result.Error err