//-----------------------------------------------------------------------
// <copyright file="GameResult.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Aletheia.Pgn.Model
{
    /// <summary>
    /// The result of a game.
    /// </summary>
    public enum GameResult
    {
        /// <summary>
        /// White has won the game.
        /// </summary>
        WhiteWins,

        /// <summary>
        /// Black has won the game.
        /// </summary>
        BlackWins,

        /// <summary>
        /// Draw.
        /// </summary>
        Draw,

        /// <summary>
        /// The result is ongoing or unspecified.
        /// </summary>
        Ongoing,
    }
}