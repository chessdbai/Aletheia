//-----------------------------------------------------------------------
// <copyright file="ResultReason.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Aletheia.Pgn.Model
{
    /// <summary>
    /// The reason for a given result.
    /// </summary>
    public enum ResultReason
    {
        /// <summary>
        /// Indicates the game result was due to abandonment
        /// by one or more of the players.
        /// </summary>
        Abandoned,

        /// <summary>
        /// Indicates the result was decided by adjudication.
        /// </summary>
        Adjudication,

        /// <summary>
        /// One of the players died.
        /// </summary>
        Death,

        /// <summary>
        /// An emergency affected the result of the game.
        /// </summary>
        Emergency,

        /// <summary>
        /// The result was normal.
        /// </summary>
        Normal,

        /// <summary>
        /// One or both of the players committed a rules
        /// infraction.
        /// </summary>
        RulesInfraction,

        /// <summary>
        /// A player ran out of time.
        /// </summary>
        TimeForfeit,

        /// <summary>
        /// The game has not finished.
        /// </summary>
        Unterminated,
    }
}