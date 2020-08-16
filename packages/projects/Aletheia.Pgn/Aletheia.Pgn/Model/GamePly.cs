//-----------------------------------------------------------------------
// <copyright file="GamePly.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Aletheia.Pgn.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// A single move made in a game.
    /// </summary>
    public class GamePly
    {
        /// <summary>
        /// Gets or sets the ply number.
        /// </summary>
        public int PlyNumber { get; set; }

        /// <summary>
        /// Gets or sets the San text.
        /// </summary>
        public string San { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this move is actually a null move.
        /// Null moves are determined at the low-level parsing interface, and can result
        /// in a varied value for "San".
        /// </summary>
        public bool SanIsNullMove { get; set; }

        /// <summary>
        /// Gets or sets a list of comments or annotations on the position.
        /// </summary>
        public List<Annotation> Annotations { get; set; } = new List<Annotation>();

        /// <summary>
        /// Gets or sets a list of Nags.
        /// </summary>
        public List<PlyNag> Nags { get; set; } = new List<PlyNag>();

        /// <summary>
        /// Gets or sets the previous ply (if any).
        /// </summary>
        public GamePly PreviousPly { get; set; }

        /// <summary>
        /// Gets or sets the next ply played in the main line.
        /// </summary>
        public GamePly NextPlyInMainLine { get; set; }

        /// <summary>
        /// Gets or sets a list of alternate moves.
        /// </summary>
        public List<GamePly> AlternateNextMoves { get; set; } = new List<GamePly>();
    }
}