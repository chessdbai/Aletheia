//-----------------------------------------------------------------------
// <copyright file="ExtraTimeFormat.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Aletheia.Pgn.Model.TimeControls.Extended
{
    /// <summary>
    /// Several variations on how extra time is given to the player.
    /// </summary>
    public enum ExtraTimeFormat
    {
        /// <summary>
        /// A format where the extra time is added to the player's clock at the beginning of the move.
        /// </summary>
        Increment,

        /// <summary>
        /// A format where the clock will seem to pause for the extra time seconds before resuming, giving
        /// the player at least <code>x</code> seconds, but the player cannot accumulate time.
        /// </summary>
        SimpleDelay,

        /// <summary>
        /// A little more nuanced, here's a quote from <a href="https://en.wikipedia.org/wiki/Chess_clock#Timing_methods">Wikipedia's description on time controls</a>:
        /// <code>
        /// Bronstein delay and Simple delay are mathematically equivalent. The advantage of Bronstein delay is that a player can always quickly see exactly how much time they have for their next move without having to mentally add the main and delay time. The advantage of Simple delay is that a player can always tell whether the time that is counting down is the delay time or the main time. Simple delay is the form of delay most often used in the US, while Bronstein delay is the form of delay most often used in most other countries.
        /// </code>
        /// </summary>
        BronsteinDelay,
    }
}