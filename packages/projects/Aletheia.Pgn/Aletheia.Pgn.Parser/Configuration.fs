namespace Aletheia.Pgn.Parser.Configuration

open Aletheia.Pgn.Parser
open Notation
open FParsec

type ParserConfiguration() =
    member val Charsets : NotationCharSets = [ SanCharset; FigurineCharset ] with get, set