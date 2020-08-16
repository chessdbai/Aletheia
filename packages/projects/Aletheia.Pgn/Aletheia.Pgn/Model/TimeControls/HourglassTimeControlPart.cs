//-----------------------------------------------------------------------
// <copyright file="HourglassTimeControlPart.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Aletheia.Pgn.Model.TimeControls
{
    using System;

    /// <summary>
    /// The time control uses an hourglass, where time
    /// is drained from one player while being added to
    /// another player.
    /// </summary>
    public class HourglassTimeControlPart : TimeControlPart
    {
        /// <inheritdoc cref="TimeControlPart" />
        public override TimeControlPartType Type => TimeControlPartType.Hourglass;

        /// <summary>
        /// Gets or sets the duration of the hourglass from the halfway point
        /// to tipping in complete favor with one side.
        /// </summary>
        public TimeSpan TotalDuration { get; set; }
    }
}