//-----------------------------------------------------------------------
// <copyright file="ExtendedTimeControl.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Aletheia.Pgn.Model.TimeControls.Extended
{
    /// <summary>
    /// A (potentially) compound time control.
    /// </summary>
    public class ExtendedTimeControl
    {
        /// <summary>
        /// Gets or sets the list of time control parts that make up this game's time control.
        /// </summary>
        public ExtendedTimeControlPart Parts { get; set; }
    }
}