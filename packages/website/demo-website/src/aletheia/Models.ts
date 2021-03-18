export interface GameTag {
   Name: string,
   Value: string
}

export interface PlyNag {
  NumericalValue: number,
  Description: string,
  Symbol: string
}

export interface Annotation {
  Text: string
}

export interface GamePly {
  PlyNumber: number,
  San: string,
  SanIsNullMove: boolean,
  Nags: PlyNag[],
  Annotations: Annotation[],
  PreviousPly: GamePly,
  NextPlyInMainLine: GamePly,
  AlternateNextMoves: GamePly[]
}

export interface Player {
  Name: string,
  Rating?: number,
  Title?: string
}

export type GameTagDictionary = {
  [index: string] : GameTag
}

export interface PgnGame {
  AllTags: GameTagDictionary,
  WhitePlayer: Player,
  BlackPlayer: Player
}

export interface PgnParsingException {
  Line: number,
  Column: number,
  Message: string
}

export interface ParseResult {
  IsError: boolean,
  ParsingException: PgnParsingException,
  Game: PgnGame
}