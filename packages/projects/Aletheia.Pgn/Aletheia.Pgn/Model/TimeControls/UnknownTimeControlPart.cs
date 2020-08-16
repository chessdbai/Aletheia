//-----------------------------------------------------------------------
// <copyright file="UnknownTimeControlPart.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Aletheia.Pgn.Model.TimeControls
{
    /// <summary>
    /// A time control part that was unspecified in the PGN.
    /// </summary>
    public class UnknownTimeControlPart : TimeControlPart
    {
        /// <inheritdoc cref="TimeControlPart" />
        public override TimeControlPartType Type => TimeControlPartType.Unknown;
    }
}