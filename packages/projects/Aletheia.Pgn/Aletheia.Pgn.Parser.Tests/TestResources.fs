namespace TestResources

open System
open System.IO

module Common =

    type private Dummy =
        { A: string
          B: string
        }

    let loadResource (str) =
        let fullName = "Aletheia.Pgn.Parser.Tests.TestResources." + str + ".pgn"
        let assembly = typedefof<Dummy>.Assembly
        let stream = assembly.GetManifestResourceStream(fullName)
        let reader = new StreamReader(stream)
        let text = reader.ReadToEnd()
        reader.Close()
        reader.Dispose()
        text

module Header =

    let rec SingleHeader =
        Common.loadResource(nameof SingleHeader)
    let rec SimpleHeader =
        Common.loadResource(nameof SimpleHeader)
    let rec HeaderWithEscapes =
        Common.loadResource(nameof HeaderWithEscapes)
    let rec HeaderWithInvalidEscape =
        Common.loadResource(nameof HeaderWithInvalidEscape)

module Tokens =
    let MoveTokens = [
        "Nd4"
        "Nd4+"
        "Nd4#"
        "d4=N"
        "d4=B+"
        "d4=R#"
        "d4=Q"
        "Qxb2"
        "♔a1"
        "♚b2"
        "♕c3+"
        "♛d4#"
        "♖e5"
        "♜f6"
        "♗g7"
        "♝h8"
        "♘h1"
        "♞g2"
        "♙f3"
        "♟︎e4"
    ]

    let ResultTokens = [
        "1-0"
        "0-1"
        "1/2-1/2"
        "*"
    ]

    let MoveNumberToken = "32."
    let BlackMoveNumberToken = "24..."

    let CommentToken = "{This is the Najdorf}"
    let WeirdCommentToken = "{/\}"
    let CommentTokenWithEscapes = "{This is a\\nmultiline comment with an escaped '\}'.}"

    let NagToken = "$15"

    let LineToken = "(1... d5 $9 {This is a terrible move} 2. Nf6)"

    let AngleToken = "<reserved-for-future-use>"

    let MultipleTokens = "17. O-O Qxc2 18. Rb1 Qxd2"
    let MultipleRecursiveTokensWithWeirdComment = "5... Nc6 (5... c6 {/\} 6. Nf3 Bg4)"

    let TokensWithVendorChessComClock = "1. e4 {[%clk 0:02:59.9]} 1... c5 {[%clk 0:02:58]}"

    let SemicolonComment = ";This is a comment"

module Games =
    let rec SimpleNoBranchingGame =
        Common.loadResource(nameof SimpleNoBranchingGame)

    let rec GameWithUnicode =
        Common.loadResource(nameof GameWithUnicode)

    let rec ChessBaseGameWithHeavyCustomTokens =
        Common.loadResource(nameof ChessBaseGameWithHeavyCustomTokens)

    let rec GameWithWeirdPartialComments =
        Common.loadResource(nameof GameWithWeirdPartialComments)

module Comments =

    let PureTextComment = "This is a bad move."
    let PureCommandComment = "[%clk 00:00:01]"
    let MixedComment = "Annotation with inline [%clk 00:00:01] command!"