module HeaderTests

open System
open Aletheia.Pgn.Parser.Configuration
open FParsec
open Xunit
open TestResources
open Aletheia.Pgn.Parser.Header

let parseHeaderLine input =
    let config = ParserConfiguration()
    match runParserOnString headerLine config "header" input with
    | Success (res, _, _) -> Result.Ok res
    | Failure (err, _, _) -> Result.Error err

[<Fact>]
let ``Can parse single PGN header line`` () =
    let header = parseHeaderLine Header.SingleHeader
    match header with
    | Result.Ok ((headerName, headerValue)) ->
        Assert.Equal("Event", headerName)
        Assert.Equal("Live Chess", headerValue)
    | Result.Error (err) -> raise (Exception(err))

[<Fact>]
let ``Can parse entire header stanza`` () =
    let config = new ParserConfiguration()
    let header = parseHeaderStanza Header.SimpleHeader config
    match header with
    | Result.Ok (items) ->
        Assert.Equal(15, items.Length)
        let (header1, value1) = items.Head
        let (headerLast, valueLast) = items.Item 14
        Assert.Equal("Event", header1)
        Assert.Equal("Live Chess", value1)
        Assert.Equal("TimeControl", headerLast)
        Assert.Equal("600", valueLast)
    | Result.Error (err) -> raise (Exception(err))

[<Fact>]
let ``Can parse header with escaped quotes stanza`` () =
    let config = ParserConfiguration()
    let header = parseHeaderStanza Header.HeaderWithEscapes config
    match header with
    | Result.Ok (items) ->
        let (header1, value1) = items.Head
        Assert.Equal("Event", header1)
        Assert.Equal("A whole\nnew world", value1)

        let (header2, value2) = items.Item 1
        Assert.Equal("Site", header2)
        Assert.Equal("A website for \"Chess\"", value2)

        let (header5, value5) = items.Item 4
        Assert.Equal("White", header5)
        Assert.Equal("일리으", value5)

        let (header6, value6) = items.Item 5
        Assert.Equal("Black", header6)
        Assert.Equal("잘리", value6)

        let (header11, value11) = items.Item 10
        Assert.Equal("Annotator", header11)
        Assert.Equal("가", value11)
    | Result.Error (err) -> raise (Exception(err))


[<Fact>]
let ``Treat invalid escape sequences as regular, non-escaped text`` () =
    let header = parseHeaderLine Header.HeaderWithInvalidEscape
    match header with
    | Result.Ok ((headerName, headerValue)) ->
        Assert.Equal("Event", headerName)
        Assert.Equal("This is \$not a valid escape, so it should be ignored", headerValue)
    | Result.Error (err) -> raise (Exception(err))
