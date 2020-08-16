//-----------------------------------------------------------------------
// <copyright file="GameTimeControl.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Aletheia.Pgn.Model.TimeControls
{
    using System.Collections.Generic;
    using Aletheia.Pgn.Model.TimeControls.Extended;

    /// <summary>
    /// A time control for a PGN game.
    /// </summary>
    public class GameTimeControl
    {
        /// <summary>
        /// Gets or sets the time control parts that comprise the
        /// game time control as given by the traditional PGN 'TimeControl'
        /// tag.
        /// </summary>
        public List<TimeControlPart> TimeControlParts { get; set; }

        /// <summary>
        /// Gets or sets the list of extended time control parts.
        /// The extended time control components are not part of the
        /// PGN standard, but are simpler and more flexible to reflect
        /// real-world and online time controls in use today.
        /// </summary>
        public List<ExtendedTimeControlPart> ExtendedTimeControlParts { get; set; }
    }
}