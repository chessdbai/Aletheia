module Aletheia.Pgn.Parser.Notation

open FParsec

type NotationCharSet = {
    PawnChars: char list
    KnightChars: char list
    BishopChars: char list
    RookChars: char list
    QueenChars: char list
    KingChars: char list
}

type NotationCharSets = NotationCharSet list

let SanCharset : NotationCharSet = {
    PawnChars=['P']
    KnightChars=['N']
    BishopChars=['B']
    RookChars=['R']
    QueenChars=['Q']
    KingChars=['K']
}

let FigurineCharset : NotationCharSet = {
    PawnChars=['♙';'♟']
    KnightChars=['♘';'♞']
    BishopChars=['♗';'♝']
    RookChars=['♖';'♜']
    QueenChars=['♕';'♛']
    KingChars=['♔';'♚']
}


let CzechSanCharset : NotationCharSet = {
    PawnChars=['P']
    KnightChars=['J']
    BishopChars=['S']
    RookChars=['V']
    QueenChars=['D']
    KingChars=['K']
}
let DanishSanCharset : NotationCharSet = {
    PawnChars=['B']
    KnightChars=['S']
    BishopChars=['L']
    RookChars=['T']
    QueenChars=['D']
    KingChars=['K']
}
let EnglishSanCharset : NotationCharSet = {
    PawnChars=['B']
    KnightChars=['S']
    BishopChars=['L']
    RookChars=['T']
    QueenChars=['D']
    KingChars=['K']
}
let EstonianSanCharset : NotationCharSet = {
    PawnChars=['P']
    KnightChars=['R']
    BishopChars=['O']
    RookChars=['V']
    QueenChars=['L']
    KingChars=['K']
}
let FinnishSanCharset : NotationCharSet = {
    PawnChars=['P']
    KnightChars=['R']
    BishopChars=['L']
    RookChars=['T']
    QueenChars=['D']
    KingChars=['K']
}
let FrenchSanCharset : NotationCharSet = {
    PawnChars=['P']
    KnightChars=['C']
    BishopChars=['F']
    RookChars=['T']
    QueenChars=['D']
    KingChars=['R']
}
let GermanSanCharset : NotationCharSet = {
    PawnChars=['B']
    KnightChars=['S']
    BishopChars=['L']
    RookChars=['T']
    QueenChars=['D']
    KingChars=['K']
}
let HungarianSanCharset : NotationCharSet = {
    PawnChars=['G']
    KnightChars=['H']
    BishopChars=['F']
    RookChars=['B']
    QueenChars=['V']
    KingChars=['K']
}
let IcelandicSanCharset : NotationCharSet = {
    PawnChars=['P']
    KnightChars=['R']
    BishopChars=['B']
    RookChars=['H']
    QueenChars=['D']
    KingChars=['K']
}
let ItalianSanCharset : NotationCharSet = {
    PawnChars=['P']
    KnightChars=['C']
    BishopChars=['A']
    RookChars=['T']
    QueenChars=['D']
    KingChars=['R']
}
let NorwegianSanCharset : NotationCharSet = {
    PawnChars=['B']
    KnightChars=['S']
    BishopChars=['L']
    RookChars=['T']
    QueenChars=['D']
    KingChars=['K']
}
let PolishSanCharset : NotationCharSet = {
    PawnChars=['P']
    KnightChars=['S']
    BishopChars=['G']
    RookChars=['W']
    QueenChars=['H']
    KingChars=['K']
}
let PortugueseSanCharset : NotationCharSet = {
    PawnChars=['P']
    KnightChars=['C']
    BishopChars=['B']
    RookChars=['T']
    QueenChars=['D']
    KingChars=['R']
}
let RomanianSanCharset : NotationCharSet = {
    PawnChars=['P']
    KnightChars=['C']
    BishopChars=['N']
    RookChars=['T']
    QueenChars=['D']
    KingChars=['R']
}
let SpanishSanCharset : NotationCharSet = {
    PawnChars=['P']
    KnightChars=['C']
    BishopChars=['A']
    RookChars=['T']
    QueenChars=['D']
    KingChars=['R']
}
let SwedishSanCharset : NotationCharSet = {
    PawnChars=['B']
    KnightChars=['S']
    BishopChars=['L']
    RookChars=['T']
    QueenChars=['D']
    KingChars=['K']
}





let notationParserForCharset charset =
    let baseRegex = "[QRNBK]?[\ufe0e]?[?[a-h]?[1-8]?x?[a-h][1-8](=[QRNB])?\+?#?( ?((e ?p)|(e\. ?p\.)))?"

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