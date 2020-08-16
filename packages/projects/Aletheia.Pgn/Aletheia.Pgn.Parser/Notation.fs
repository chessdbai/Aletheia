module Aletheia.Pgn.Parser.Notation

open FParsec

type NotationCharSet = {
    KingChars: char list
    QueenChars: char list
    RookChars: char list
    BishopChars: char list
    KnightChars: char list
    PawnChars: char list
}

type NotationCharSets = NotationCharSet list

let SanCharset : NotationCharSet = {
    KingChars=['K']
    QueenChars=['Q']
    RookChars=['R']
    BishopChars=['B']
    KnightChars=['N']
    PawnChars=['P']
}

let FigurineCharset : NotationCharSet = {
    KingChars=['♔';'♚']
    QueenChars=['♕';'♛']
    RookChars=['♖';'♜']
    BishopChars=['♗';'♝']
    KnightChars=['♘';'♞']
    PawnChars=['♙';'♟']
}

let notationParserForCharset charset =
    let baseRegex = "[{0}]?[\ufe0e]?[?[a-h]?[1-8]?x?[a-h][1-8](=[{1}])?\+?#?( ?((e ?p)|(e\. ?p\.)))?"

    let movableChars = List.concat [
        charset.KingChars
        charset.QueenChars
        charset.RookChars
        charset.KnightChars
        charset.BishopChars
        charset.PawnChars
    ]
    let promotableChars = List.concat [
        charset.QueenChars
        charset.RookChars
        charset.KnightChars
        charset.BishopChars
    ]
    let formatted = System.String.Format(baseRegex,
                                         System.String.Join("", movableChars),
                                         System.String.Join("", promotableChars))
    regex formatted

let notationParserForCharsets charsets =
    let mapper charset = notationParserForCharset charset
    choice (charsets |> List.map mapper)