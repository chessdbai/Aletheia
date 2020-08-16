//-----------------------------------------------------------------------
// <copyright file="PeriodTimeControlPart.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Aletheia.Pgn.Model.TimeControls
{
    using System;

    /// <summary>
    /// A time control with a set number of moves.
    /// </summary>
    public class PeriodTimeControlPart : TimeControlPart
    {
        /// <inheritdoc cref="TimeControlPart" />
        public override TimeControlPartType Type => TimeControlPartType.Period;

        /// <summary>
        /// Gets or sets the number of moves this time control period lasts for.
        /// </summary>
        public int Moves { get; set; }

        /// <summary>
        /// Gets or sets the total duration of this time control period.
        /// </summary>
        public TimeSpan PeriodDuration { get; set; }
    }
}