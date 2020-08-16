//-----------------------------------------------------------------------
// <copyright file="TimeControlPart.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Aletheia.Pgn.Model.TimeControls
{
    /// <summary>
    /// A base class for time control parts.
    /// </summary>
    public abstract class TimeControlPart
    {
        /// <summary>
        /// Gets the type of time control that this portion of the time control is.
        /// </summary>
        public abstract TimeControlPartType Type { get; }
    }
}