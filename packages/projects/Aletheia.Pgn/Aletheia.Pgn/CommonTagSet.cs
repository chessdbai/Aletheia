//-----------------------------------------------------------------------
// <copyright file="CommonTagSet.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Aletheia.Pgn
{
    using System.Collections.Generic;
    using Aletheia.Pgn.Model;

    /// <summary>
    /// A class with common values that many PGN games have.
    /// </summary>
    public class CommonTagSet
    {
        private readonly Dictionary<string, GameTag> tags;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonTagSet" /> class.
        /// </summary>
        /// <param name="tags">The tag dictionary.</param>
        public CommonTagSet(Dictionary<string, GameTag> tags)
        {
            this.tags = tags;
        }
    }
}