//-----------------------------------------------------------------------
// <copyright file="ExtendedTimeControlPart.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Aletheia.Pgn.Model.TimeControls.Extended
{
    /// <summary>
    /// A single component of an extended time control.
    /// </summary>
    public class ExtendedTimeControlPart
    {
        /// <summary>
        /// Gets or sets the amount of time white starts with for this part.
        /// </summary>
        public int? WhiteTimeSeconds { get; set; }

        /// <summary>
        /// Gets or sets the amount of extra time white receives, in seconds.
        /// </summary>
        public int? WhiteExtraTimeSeconds { get; set; }

        /// <summary>
        /// Gets or sets the way that extra time is added to the white player's clock.
        /// </summary>
        public ExtraTimeFormat? WhiteExtraTimeFormat { get; set; }

        /// <summary>
        /// Gets or sets the amount of time black starts with for this part.
        /// </summary>
        public int? BlackTimeSeconds { get; set; }

        /// <summary>
        /// Gets or sets the amount of extra time black receives, in seconds.
        /// </summary>
        public int? BlackExtraTimeSeconds { get; set; }

        /// <summary>
        /// Gets or sets the way that extra time is added to the black player's clock.
        /// </summary>
        public ExtraTimeFormat? BlackExtraTimeFormat { get; set; }
    }
}