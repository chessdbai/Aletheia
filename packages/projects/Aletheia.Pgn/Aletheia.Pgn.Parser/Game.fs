module Aletheia.Pgn.Parser.Game

open System
open Aletheia.Pgn.Parser
open Aletheia.Pgn.Parser.Configuration
open FParsec

type PgnParsingException(message: string, error : ParserError) =
   inherit Exception(message)

   member this.Line = error.Position.Line
   member this.Column = error.Position.Column


type GameHeader = (string*string)

type TokenizedPgnGame = {
    Tags: GameHeader list
    Tokens: Tokens.Token list
}

let gameParser : Parser<TokenizedPgnGame, ParserConfiguration> =
    Header.headerStanza
    .>>. sepEndBy Tokens.tokenParser spaces
    |>> fun (headerTags, tokens) ->
        {
            Tags=headerTags
            Tokens=tokens
        }


let preprocess (gameTxt : string) = gameTxt.Replace("/\\", "/\\\\")

let parseGame pgnText config =
    let processed = preprocess pgnText
    match runParserOnString gameParser config "game" processed with
    | Success (res, _, _) -> res
    | Failure (errMessage, errDetails, _) -> raise(PgnParsingException(errMessage, errDetails))