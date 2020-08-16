//-----------------------------------------------------------------------
// <copyright file="Player.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Aletheia.Pgn.Model
{
    /// <summary>
    /// A small class storing information about one of the players.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Gets or sets the name of the player.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the player.
        /// </summary>
        public int? Rating { get; set; }

        /// <summary>
        /// Gets or sets the name of the player.
        /// </summary>
        public string Title { get; set; }
    }
}