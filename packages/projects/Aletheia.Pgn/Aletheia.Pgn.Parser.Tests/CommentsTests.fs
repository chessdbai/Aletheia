module CommentsTests

open System
open FParsec
open Xunit
open TestResources
open Aletheia.Pgn.Parser.Comments

[<Fact>]
let ``Parses a purely-text comment as an annotation.`` () =
    let parts = parseCommentText Comments.PureTextComment
    Assert.Equal(1, parts.Length)
    let part = parts.Head
    match part with
    | CommentPart.Annotation (annotation) ->
        Assert.Equal("This is a bad move.", annotation.Text)
    | _ -> raise(Exception("Incorrect comment part type."))


[<Fact>]
let ``Parses a pure-command comment as a command.`` () =
    let parts = parseCommentText Comments.PureCommandComment
    Assert.Equal(1, parts.Length)
    let part = parts.Head
    match part with
    | CommentPart.Command (cmd) ->
        Assert.Equal("clk", cmd.CommandName)
        Assert.Equal(1, cmd.Operands.Length)
        Assert.Equal("00:00:01", cmd.Operands.Item 0)
    | CommentPart.Annotation (annotation) ->
        raise(
                 Exception(
                              "Did not interpret as command. Instead interpreted as annotation with text '" +
                              annotation.Text +
                              "'"
                          )
             )


[<Fact>]
let ``Parses a comment with an inline command correctly.`` () =
    let parts = parseCommentText Comments.MixedComment
    Assert.Equal(3, parts.Length)
    let annotationPart1 = parts.Item 0
    let commandPart = parts.Item 1
    let annotationPart2 = parts.Item 2
    match annotationPart1 with
        | CommentPart.Annotation (annotation) ->
            Assert.Equal("Annotation with inline ", annotation.Text)
        | _ -> raise(Exception("First token in mixed comment should be an annotation."))
    match commandPart with
        | CommentPart.Command (cmd) ->
            Assert.Equal("clk", cmd.CommandName)
        | _ -> raise(Exception("Second token in mixed comment should be a command."))
    match annotationPart2 with
        | CommentPart.Annotation (annotation) ->
            Assert.Equal(" command!", annotation.Text)
        | _ -> raise(Exception("Third token in mixed comment should be an annotation."))