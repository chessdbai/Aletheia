//-----------------------------------------------------------------------
// <copyright file="PgnFormatException.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Aletheia.Pgn.Model
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// An exception thrown when the format of the PGN text is not following the specification.
    /// </summary>
    [Serializable]
    public class PgnFormatException : Exception
    {
        private const string DefaultErrorMessage = "The format of the PGN game was invalid.";

        /// <summary>
        /// Initializes a new instance of the <see cref="PgnFormatException"/> class.
        /// </summary>
        public PgnFormatException()
            : base(DefaultErrorMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PgnFormatException"/> class.
        /// </summary>
        /// <param name="message">A custom message to include in the exception.</param>
        public PgnFormatException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PgnFormatException"/> class.
        /// </summary>
        /// <param name="message">A custom message to include in the exception.</param>
        /// <param name="inner">An inner exception.</param>
        public PgnFormatException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PgnFormatException"/> class.
        /// </summary>
        /// <param name="info">Information about the serialization process.</param>
        /// <param name="context">Streaming context.</param>
        protected PgnFormatException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}