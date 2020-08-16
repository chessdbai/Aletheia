//-----------------------------------------------------------------------
// <copyright file="PgnGame.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Aletheia.Pgn.Model
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a PGN game.
    /// </summary>
    public class PgnGame
    {
        /// <summary>
        /// Gets or sets the dictionary of tags.
        /// </summary>
        public Dictionary<string, GameTag> AllTags { get; set; } = new Dictionary<string, GameTag>();

        /// <summary>
        /// Gets or sets the first ply.
        /// </summary>
        public GamePly FirstPly { get; set; }

        /// <summary>
        /// Gets the main line of the game as a list of plies,
        /// which is easier for some access patterns than the tree
        /// structure.
        /// </summary>
        public List<GamePly> MainLineAsList => this.GetMainLineAsList();

        /// <summary>
        /// Gets the player information for the player with the white pieces.
        /// </summary>
        public Player WhitePlayer => this.GetPlayerWithColor("White");

        /// <summary>
        /// Gets the player information for the player with the black pieces.
        /// </summary>
        public Player BlackPlayer => this.GetPlayerWithColor("Black");

        private List<GamePly> GetMainLineAsList()
        {
            var mainLine = new List<GamePly>();
            if (this.FirstPly == null)
            {
                return mainLine;
            }

            GamePly current = this.FirstPly;
            do
            {
                mainLine.Add(current);
                current = current.NextPlyInMainLine;
            }
            while (current != null);

            return mainLine;
        }

        private Player GetPlayerWithColor(string colorName)
        {
            string name = this.AllTags.ContainsKey($"{colorName}") ? this.AllTags[$"{colorName}"].Value : null;
            int? elo = this.AllTags.ContainsKey($"{colorName}Elo") ? this.AllTags[$"{colorName}Elo"].ValueAsInt() : null;
            string title = this.AllTags.ContainsKey($"{colorName}Title") ? this.AllTags[$"{colorName}Title"].Value : null;
            return new Player()
            {
                Name = name,
                Title = title,
                Rating = elo,
            };
        }
    }
}