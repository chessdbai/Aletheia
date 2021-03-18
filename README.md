# Aletheia

### What is Aletheia?

#### Aletheia is ChessDB's custom PGN parser.

## Components

### ChessDB.Aletheia.Pgn

This is the user-friendly parser that includes the ability to parse files with multiple games in them. It wraps the lower-level F# parser.

### ChessDB.Aletheia.Pgn.Parser

This the lower-level parser written in F#. As such, it isn't super friendly to use directly from C#.