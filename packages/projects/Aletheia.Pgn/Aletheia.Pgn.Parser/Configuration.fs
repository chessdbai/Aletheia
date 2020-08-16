namespace Aletheia.Pgn.Parser.Configuration

open Aletheia.Pgn.Parser
open Notation
open FParsec

type ParserConfiguration() =
    member this.Charsets : NotationCharSets = [ SanCharset; FigurineCharset ]