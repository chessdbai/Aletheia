//-----------------------------------------------------------------------
// <copyright file="ResultExtensions.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Aletheia.Pgn.Model
{
    using System;

    /// <summary>
    /// Static class with extension methods for
    /// <see cref="GameResult"/> and <see cref="ResultReason"/>.
    /// </summary>
    public static class ResultExtensions
    {
        /// <summary>
        /// Try to convert the string to a <see cref="ResultReason"/>.
        /// </summary>
        /// <param name="resultReason">The result reason enum value.</param>
        /// <returns>The string value for the <see cref="ResultReason"/>.</returns>
        public static string ToResultReasonString(this ResultReason resultReason) => resultReason switch
        {
            ResultReason.Abandoned => "abandoned",
            ResultReason.Adjudication => "adjudication",
            ResultReason.Death => "death",
            ResultReason.Emergency => "emergency",
            ResultReason.Normal => "normal",
            ResultReason.Unterminated => "unterminated",
            ResultReason.RulesInfraction => "rulesinfraction",
            ResultReason.TimeForfeit => "timeforfeit",
            _ => throw new ArgumentException($"Unknown ResultReason value '{resultReason}'.")
        };

        /// <summary>
        /// Try to convert the string to a <see cref="ResultReason"/>.
        /// </summary>
        /// <param name="resultReasonString">The result reason string.</param>
        /// <returns>The <see cref="ResultReason"/> enum value.</returns>
        public static ResultReason AsResultReason(this string resultReasonString) => resultReasonString.ToLower() switch
        {
            "abandoned" => ResultReason.Abandoned,
            "adjudication" => ResultReason.Adjudication,
            "death" => ResultReason.Death,
            "emergency" => ResultReason.Emergency,
            "normal" => ResultReason.Normal,
            "unterminated" => ResultReason.Unterminated,
            "rulesInfraction" => ResultReason.RulesInfraction,
            "timeForfeit" => ResultReason.TimeForfeit,
            _ => throw new ArgumentException($"Unknown ResultReason string '{resultReasonString}'.")
        };

        /// <summary>
        /// Gets the string representation of a <see cref="GameResult" />.
        /// </summary>
        /// <param name="gameResult">The game result.</param>
        /// <returns>The string value of the <see cref="GameResult"/>.</returns>
        public static string ToResultReasonString(this GameResult gameResult) => gameResult switch
        {
            GameResult.WhiteWins => "1-0",
            GameResult.BlackWins => "0-1",
            GameResult.Draw => "1/2-1/2",
            GameResult.Ongoing => "*",
            _ => throw new ArgumentException($"Unknown GameResult value '{gameResult}'.")
        };

        /// <summary>
        /// Gets the value of a string as a <see cref="GameResult" />.
        /// </summary>
        /// <param name="resultString">The result string.</param>
        /// <returns>The <see cref="GameResult"/> enum value.</returns>
        public static GameResult AsResult(this string resultString) => resultString switch
        {
            "1-0" => GameResult.WhiteWins,
            "0-1" => GameResult.BlackWins,
            "1/2-1/2" => GameResult.Draw,
            "*" => GameResult.Ongoing,
            _ => throw new ArgumentException($"Unknown result string '{resultString}'.")
        };
    }
}