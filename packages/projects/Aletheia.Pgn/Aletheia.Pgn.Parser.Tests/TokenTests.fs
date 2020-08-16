module TokenTests

open Aletheia.Pgn.Parser.Comments
open Aletheia.Pgn.Parser.Configuration
open Xunit
open System
open FParsec
open TestResources
open Aletheia.Pgn.Parser.Tokens

let parseString (parser, input) =
    let defaultConfig = ParserConfiguration()
    match runParserOnString parser defaultConfig "game" input with
    | Success (token, _, _) -> token
    | Failure (err, _, _) -> raise (Exception(err))


[<Fact>]
let ``Can parse ply tokens`` () =
    let mapper mt =
        let token = parseString (tokenParser, mt)
        match token with
            | Token.Ply (ply) -> (mt, ply)
            | _ -> raise(Exception("Wrong token type."))
    let vals = Tokens.MoveTokens |> List.map mapper
    let moveTokensParsed = vals |> List.forall(fun (mts, token) -> mts = token.San)
    Assert.True(moveTokensParsed)


[<Fact>]
let ``Can parse move number tokens`` () =
    let token = parseString (tokenParser, Tokens.MoveNumberToken)
    match token with
        | Token.MoveNumber (moveNum) ->
            Assert.Equal(32, moveNum.Number)
        | _ -> raise(Exception("Wrong token type."))


[<Fact>]
let ``Can parse black move number tokens`` () =
    let token = parseString (tokenParser, Tokens.BlackMoveNumberToken)
    match token with
        | Token.BlackMoveNumber (moveNum) ->
            Assert.Equal(24, moveNum.Number)
        | _ -> raise(Exception("Wrong token type."))


[<Fact>]
let ``Can parse comments`` () =
    let token = parseString (tokenParser, Tokens.CommentToken)
    match token with
        | Token.Comment (comment) ->
            Assert.Equal(1, comment.Parts.Length)
            let firstPart = comment.Parts.Head
            match firstPart with
                | CommentPart.Annotation (annotation) ->
                    Assert.Equal("This is the Najdorf", annotation.Text)
                | _ -> raise(Exception("Wrong comment part type."))
        | _ -> raise(Exception("Wrong token type."))

[<Fact>]
let ``Parses weird comment token as a annotation.`` () =
    let parts = parseCommentText Tokens.WeirdCommentToken
    Assert.Equal(1, parts.Length)
    let part = parts.Head
    match part with
    | CommentPart.Annotation (annotation) ->
        Assert.Equal("{/\}", annotation.Text)
    | _ -> raise(Exception("Incorrect comment part type."))

[<Fact>]
let ``Parses semicolon comments`` () =
    let part = parseString (tokenParser, Tokens.SemicolonComment)
    match part with
        | Token.Comment comment ->
            Assert.Equal(1, comment.Parts.Length)
            match comment.Parts.Head with
                | CommentPart.Annotation annotation ->
                    Assert.Equal("This is a comment", annotation.Text)
                | _ -> raise(Exception("Incorrect comment part type."))
        | _ -> raise(Exception("Incorrect token type."))

(*
[<Fact>]
let ``Can parse with escape characters`` () =
    let token = parseString (tokenParser, Tokens.CommentTokenWithEscapes)
    match token with
        | Token.Comment (comment) ->
            Assert.Equal(1, comment.Parts.Length)
            let firstPart = comment.Parts.Head
            match firstPart with
                | CommentPart.Annotation (annotation) ->
                    Assert.Equal("This is a\nmultiline comment with an escaped '}'.", annotation.Text)
                | _ -> raise(Exception("Wrong comment part type."))
        | _ -> raise(Exception("Wrong token type."))
*)

[<Fact>]
let ``Can parse NAGs`` () =
    let token = parseString (tokenParser, Tokens.NagToken)
    match token with
        | Token.Nag (nag) ->
            Assert.Equal(15, nag.Number)
        | _ -> raise(Exception("Wrong token type."))

[<Fact>]
let ``Can parse results`` () =
    let mapper mt =
        let token = parseString (tokenParser, mt)
        match token with
            | Token.Result (p) ->
                match p with
                    | ResultToken.WhiteWins -> "whiteWins"
                    | ResultToken.BlackWins -> "blackWins"
                    | ResultToken.Draw -> "draw"
                    | ResultToken.Unspecified -> "unspecified"
            | _ -> raise(Exception("Wrong token type."))
    let vals = Tokens.ResultTokens |> List.map mapper
    Assert.Equal("whiteWins", vals.Item 0)
    Assert.Equal("blackWins", vals.Item 1)
    Assert.Equal("draw", vals.Item 2)
    Assert.Equal("unspecified", vals.Item 3)

[<Fact>]
let ``Can parse multiple tokens`` () =
    let tokens = parseString (multipleTokenParser, Tokens.MultipleTokens)
    Assert.Equal(6, tokens.Length)

[<Fact>]
let ``Can parse line`` () =
    let token = parseString (tokenParser, Tokens.LineToken)
    match token with
        | Token.Line (line) ->
            Assert.Equal(6, line.Line.Length)
            match (line.Line.Item 0) with
                | Token.BlackMoveNumber (nmt) ->
                    Assert.Equal(1, nmt.Number)
                | _ -> raise(Exception("Token at index 0 was not a 'BlackMoveNumber' token."))
            match (line.Line.Item 1) with
                | Token.Ply (ply) ->
                    Assert.Equal("d5", ply.San)
                | _ -> raise(Exception("Token at index 1 was not a 'Ply' token."))
            match (line.Line.Item 2) with
                | Token.Nag (nag) ->
                    Assert.Equal(9, nag.Number)
                | _ -> raise(Exception("Token at index 2 was not a 'Nag' token."))
            match (line.Line.Item 3) with
                | Token.Comment (comment) ->
                    Assert.Equal(1, comment.Parts.Length)
                    let firstPart = comment.Parts.Head
                    match firstPart with
                        | CommentPart.Annotation (annotation) ->
                            Assert.Equal("This is a terrible move", annotation.Text)
                        | _ -> raise(Exception("Wrong comment part type."))
                | _ -> raise(Exception("Token at index 3 was not a 'Comment' token."))
            match (line.Line.Item 4) with
                | Token.MoveNumber (ply) ->
                    Assert.Equal(2, ply.Number)
                | _ -> raise(Exception("Token at index 4 was not a 'MoveNumber' token."))
            match (line.Line.Item 5) with
                | Token.Ply (ply) ->
                    Assert.Equal("Nf6", ply.San)
                | _ -> raise(Exception("Token at index 5 was not a 'Ply' token."))
        | _ -> raise(Exception("Wrong token type."))