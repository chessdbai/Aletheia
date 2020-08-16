//-----------------------------------------------------------------------
// <copyright file="PlyNag.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Aletheia.Pgn.Model
{
    /// <summary>
    /// A symbolic attribute applied to a ply in a game
    /// the denotes an objective or subjective observation
    /// about the evaluation of the position.
    /// </summary>
    public readonly struct PlyNag
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlyNag"/> struct.
        /// </summary>
        /// <param name="numericalValue">The numerical value of this Nag.</param>
        /// <param name="symbol">The symbol, if any, to use in the San notation.</param>
        /// <param name="description">The description of the meaning of this Nag.</param>
        public PlyNag(int numericalValue, string symbol, string description)
        {
            this.NumericalValue = numericalValue;
            this.Symbol = symbol;
            this.Description = description;
        }

        /// <summary>
        /// Gets the numerical value of this nag.
        /// </summary>
        public int NumericalValue { get; }

        /// <summary>
        /// Gets the description of the meaning of this nag.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the symbol for this nag, if any.
        /// </summary>
        public string Symbol { get; }

        /// <summary>
        /// Determines if two nags are not equal.
        /// </summary>
        /// <param name="n1">The first nag in the equality check.</param>
        /// <param name="n2">The second nag in the equality check.</param>
        /// <returns>A value indicating whether or not the two nags are equal.</returns>
        public static bool operator ==(PlyNag n1, PlyNag n2) => n1.NumericalValue == n2.NumericalValue;

        /// <summary>
        /// Determines if two nags are not equal.
        /// </summary>
        /// <param name="n1">The first nag in the negative equality check.</param>
        /// <param name="n2">The second nag in the negative equality check.</param>
        /// <returns>A value indicating whether or not the two nags are not equal.</returns>
        public static bool operator !=(PlyNag n1, PlyNag n2) => n1.NumericalValue != n2.NumericalValue;

        /// <summary>
        /// Checks of the specified object is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare to this instance.</param>
        /// <returns>A value indicating whether or not the specified object is equal to this current instance.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is PlyNag))
            {
                return false;
            }

            return ((PlyNag)obj).NumericalValue == this.NumericalValue;
        }

        /// <summary>
        /// Gets the hash code of this PlyNag.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode() => this.NumericalValue.GetHashCode();
    }
}