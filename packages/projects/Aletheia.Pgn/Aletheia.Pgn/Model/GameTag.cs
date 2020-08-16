//-----------------------------------------------------------------------
// <copyright file="GameTag.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Aletheia.Pgn.Model
{
    using System;

    /// <summary>
    /// Represents a tag key value pair found in the header of a PGN game string.
    /// </summary>
    public class GameTag
    {
        /// <summary>
        /// Gets or sets the name of this tag.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value of this tag as a string.
        /// </summary>
        public string Value { get; set; }
    }
}