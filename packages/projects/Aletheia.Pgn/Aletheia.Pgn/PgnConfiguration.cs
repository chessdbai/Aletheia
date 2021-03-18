//-----------------------------------------------------------------------
// <copyright file="PgnConfiguration.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Aletheia.Pgn
{
    using System.Linq;
    using Aletheia.Pgn.Parser;
    using Aletheia.Pgn.Parser.Configuration;

    /// <summary>
    /// The configuration to use for parsing and assembling the PGN string into the game object.
    /// </summary>
    public class PgnConfiguration
    {
        /// <summary>
        /// Gets or sets a value indicating whether the SAN moves should be rewritten from the input charset to the output charset.
        /// </summary>
        public bool RewriteSan { get; set; } = false;

        /// <summary>
        /// Gets or sets the input charsets to allow for SAN or FAN (figurine algebraic notation) move ply texts.
        /// </summary>
        public Notation.NotationCharSet[] InputCharsets { get; set; } = new ParserConfiguration().Charsets.ToArray();

        /// <summary>
        /// Gets or sets the output charset. Input characters from the input charset are converted
        /// into this charset for common reading.
        /// </summary>
        public Notation.NotationCharSet OutputCharsets { get; set; } = Notation.SanCharset;
    }
}