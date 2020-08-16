//-----------------------------------------------------------------------
// <copyright file="UntimedTimeControlPart.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Aletheia.Pgn.Model.TimeControls
{
    /// <summary>
    /// A time control for un-timed games.
    /// </summary>
    public class UntimedTimeControlPart : TimeControlPart
    {
        /// <inheritdoc cref="TimeControlPart" />
        public override TimeControlPartType Type => TimeControlPartType.Untimed;
    }
}