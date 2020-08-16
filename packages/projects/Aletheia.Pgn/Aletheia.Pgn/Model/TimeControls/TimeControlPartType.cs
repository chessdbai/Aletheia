//-----------------------------------------------------------------------
// <copyright file="TimeControlPartType.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Aletheia.Pgn.Model.TimeControls
{
    /// <summary>
    /// The different variations on Chess time controls
    /// that compose a game's time control.
    /// </summary>
    public enum TimeControlPartType
    {
        /// <summary>
        /// The time control was not specified.
        /// </summary>
        Unknown,

        /// <summary>
        /// The game was un-timed.
        /// </summary>
        Untimed,

        /// <summary>
        /// The time control includes increment time.
        /// </summary>
        Incremental,

        /// <summary>
        /// The time control has no increment.
        /// </summary>
        SuddenDeath,

        /// <summary>
        /// The time control comes with a set number of moves.
        /// </summary>
        Period,

        /// <summary>
        /// The time control uses an hourglass, where time
        /// is drained from one player while being added to
        /// another player.
        /// </summary>
        Hourglass,
    }
}