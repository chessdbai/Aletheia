//-----------------------------------------------------------------------
// <copyright file="IncrementalTimeControlPart.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Aletheia.Pgn.Model.TimeControls
{
    using System;

    /// <summary>
    /// A time control with extra time.
    /// </summary>
    public class IncrementalTimeControlPart : TimeControlPart
    {
        /// <inheritdoc cref="TimeControlPart" />
        public override TimeControlPartType Type => TimeControlPartType.Incremental;

        /// <summary>
        /// Gets or sets the duration of the time control.
        /// </summary>
        public TimeSpan TotalDuration { get; set; }

        /// <summary>
        /// Gets or sets the extra time each player receives per turn.
        /// </summary>
        public int IncrementSeconds { get; set; }
    }
}