module GameTests

open System
open Aletheia.Pgn.Parser.Configuration
open FParsec
open Xunit
open Aletheia.Pgn.Parser
open Tokens

let defaultConfig = ParserConfiguration()

[<Fact>]
let ``Can parse single PGN header line`` () =
    let game = Game.parseGame TestResources.Games.SimpleNoBranchingGame defaultConfig
    Assert.Equal(13, game.Tags.Length)
    Assert.Equal(118, game.Tokens.Length)
    match game.Tokens.Item 117 with
    | Token.Result r ->
        Assert.Equal(ResultToken.BlackWins, r)
    | _ -> raise(Exception("Last token of game was not of the type 'Result'."))

[<Fact>]
let ``Can parse game with unicode comments`` () =
    let game = Game.parseGame TestResources.Games.GameWithUnicode defaultConfig
    Assert.Equal(67, game.Tokens.Length)

[<Fact>]
let ``Can parse game with lots of custom ChessBase tokens`` () =
    let game = Game.parseGame TestResources.Games.ChessBaseGameWithHeavyCustomTokens defaultConfig
    Assert.Equal(118, game.Tokens.Length)

[<Fact>]
let ``Can parse game with weird partial comments.`` () =
    let game = Game.parseGame TestResources.Games.GameWithWeirdPartialComments defaultConfig
    Assert.Equal(105, game.Tokens.Length)