//-----------------------------------------------------------------------
// <copyright file="Annotation.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Aletheia.Pgn.Model
{
    /// <summary>
    /// Marks that an annotator added some informative text to a ply.
    /// </summary>
    public class Annotation
    {
        /// <summary>
        /// Gets or sets the text added by the annotator to the ply.
        /// </summary>
        public string Text { get; set; }
    }
}