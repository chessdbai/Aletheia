//-----------------------------------------------------------------------
// <copyright file="SuddenDeathTimeControlPart.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Aletheia.Pgn.Model.TimeControls
{
    using System;

    /// <summary>
    /// The time control has no increment.
    /// </summary>
    public class SuddenDeathTimeControlPart : TimeControlPart
    {
        /// <inheritdoc cref="TimeControlPart" />
        public override TimeControlPartType Type => TimeControlPartType.SuddenDeath;

        /// <summary>
        /// Gets or sets the duration of this time control.
        /// </summary>
        public TimeSpan TotalDuration { get; set; }
    }
}