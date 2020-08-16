//-----------------------------------------------------------------------
// <copyright file="PgnGameParser.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Aletheia.Pgn
{
    using Aletheia.Pgn.Model;
    using Aletheia.Pgn.Parser;
    using Aletheia.Pgn.Parser.Configuration;

    /// <summary>
    /// A class that interops with the F# library that does the heavy lifting
    /// to parse a PGN Chess game.
    /// </summary>
    public class PgnGameParser
    {
        /// <summary>
        /// Gets or sets the configuration to use when parsing the PGN text.
        /// Options such as the character set for pieces in SAN or FAN notation
        /// can be set through this configuration.
        /// </summary>
        public ParserConfiguration Configuration { get; set; } = new ParserConfiguration();

        /// <summary>
        /// Parse a string into a PGN Chess game.
        /// </summary>
        /// <param name="pgnText">The text of the game.</param>
        /// <returns>The parsed game.</returns>
        public PgnGame ParseGame(string pgnText)
        {
            Game.TokenizedPgnGame parsedGame = Game.parseGame(pgnText, this.Configuration);
            return GameAssembler.AssembleGameFromTokens(parsedGame);
        }
    }
}