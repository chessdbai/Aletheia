//-----------------------------------------------------------------------
// <copyright file="TagExtensions.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Aletheia.Pgn.Model
{
    using System;
    using System.Globalization;
    using Aletheia.Pgn.Model.TimeControls;

    /// <summary>
    /// Static class with extension methods for tags.
    /// </summary>
    public static class TagExtensions
    {
        /// <summary>
        /// Gets the value of this tag as an integer, if possible.
        /// </summary>
        /// <param name="gameTag">The game tag to get the integer value of.</param>
        /// <returns>The <see cref="int"/> value of this tag.</returns>
        public static int? ValueAsInt(this GameTag gameTag) =>
            int.TryParse(gameTag.Value, out var num)
                ? num
                : (int?)null;

        /// <summary>
        /// Sets the value of this tag as an integer.
        /// </summary>
        /// <param name="gameTag">The game tag to get the integer value of.</param>
        /// <param name="integerValue">The <see cref="int"/> value of this tag.</param>
        public static void SetIntValue(this GameTag gameTag, int integerValue) =>
            gameTag.Value = integerValue.ToString();

        /// <summary>
        /// Gets the value of this tag as an double, if possible.
        /// </summary>
        /// <param name="gameTag">The game tag to get the integer value of.</param>
        /// <returns>The <see cref="double"/> value of this tag.</returns>
        public static double? ValueAsDouble(this GameTag gameTag) =>
            double.TryParse(gameTag.Value, out var num)
                ? num
                : (double?)null;

        /// <summary>
        /// Sets the value of this tag as a double.
        /// </summary>
        /// <param name="gameTag">The <see cref="GameTag"/>.</param>
        /// <param name="doubleValue">The <see cref="double"/> value of this tag.</param>
        public static void SetDoubleValue(this GameTag gameTag, double doubleValue) =>
            gameTag.Value = doubleValue.ToString();

        /// <summary>
        /// Gets the value of this tag as an double, if possible.
        /// </summary>
        /// <param name="gameTag">The <see cref="GameTag"/>.</param>
        /// <returns>The <see cref="DateTime"/> value of this tag.</returns>
        public static DateTime? AsDateValue(this GameTag gameTag)
        {
            if (DateTime.TryParseExact(gameTag.Value, "yyyy.MM.dd", null, DateTimeStyles.None, out var simpleDate))
            {
                return simpleDate;
            }

            if (DateTime.TryParse(gameTag.Value, out var fullDate))
            {
                return fullDate;
            }

            return null;
        }

        /// <summary>
        /// Sets the value of this tag to a Date value.
        /// </summary>
        /// <param name="gameTag">The <see cref="GameTag"/>.</param>
        /// <param name="year">The year when this game was played.</param>
        /// <param name="month">The month when this game was played.</param>
        /// <param name="day">The day when this game was played.</param>
        public static void SetDateValue(this GameTag gameTag, int year, int month, int day) =>
            gameTag.Value = $"{year.ToString("0000")}.{month.ToString("00")}.{day.ToString("00")}";

        /// <summary>
        /// Sets the value of this tag to a Date value.
        /// </summary>
        /// <param name="gameTag">The <see cref="GameTag"/>.</param>
        /// <param name="date">The date.</param>
        public static void SetDateValue(this GameTag gameTag, DateTime date) =>
            gameTag.Value = date.ToString("yyyy.MM.dd");

        /// <summary>
        /// Gets the value of this tag as an double, if possible.
        /// </summary>
        /// <param name="gameTag">The <see cref="GameTag"/>.</param>
        /// <returns>The <see cref="DateTime"/> value of this tag.</returns>
        public static DateTime? AsDateTime(this GameTag gameTag) =>
            DateTime.TryParse(gameTag.Value, out var num)
                ? num
                : (DateTime?)null;

        /// <summary>
        /// Sets the value of this tag as a double.
        /// </summary>
        /// <param name="gameTag">The <see cref="GameTag"/>.</param>
        /// <param name="dateTime">The <see cref="DateTime"/> value of this tag.</param>
        public static void SetDateTimeValue(this GameTag gameTag, DateTime dateTime) =>
            gameTag.Value = dateTime.ToString();

        /// <summary>
        /// Gets the value of a tag in the format of a <see cref="ResultReason" />.
        /// </summary>
        /// <param name="gameTag">The tag.</param>
        /// <returns>The value of the tag as a result reason.</returns>
        public static ResultReason AsResultReason(this GameTag gameTag) =>
            gameTag.Value.AsResultReason();

        /// <summary>
        /// Gets the value of a tag in the format of a <see cref="ResultReason" />.
        /// </summary>
        /// <param name="gameTag">The tag.</param>
        /// <param name="reason">The result reason.</param>
        public static void SetResultReasonValue(this GameTag gameTag, ResultReason reason) =>
            gameTag.Value = reason.ToResultReasonString();

        /// <summary>
        /// Gets the value of a tag in the format of a <see cref="ResultReason" />.
        /// </summary>
        /// <param name="gameTag">The tag.</param>
        /// <returns>The value of the tag as a result reason.</returns>
        public static GameResult AsResult(this GameTag gameTag) => gameTag.Value.AsResult();

        /// <summary>
        /// Sets the value of a tag in the format of a <see cref="ResultReason" />.
        /// </summary>
        /// <param name="gameTag">The tag.</param>
        /// <param name="result">The result.</param>
        public static void SetResultValue(this GameTag gameTag, GameResult result)
        {
            var resultString = result switch
            {
                GameResult.WhiteWins => "1-0",
                GameResult.BlackWins => "0-1",
                GameResult.Draw => "1/2-1/2",
                GameResult.Ongoing => "*",
                _ => throw new ArgumentException($"Unknown ResultReason string '{gameTag.Value}'.")
            };
            gameTag.Value = resultString;
        }

        /// <summary>
        /// Gets the value of a tag in the format of a <see cref="ResultReason" />.
        /// </summary>
        /// <param name="gameTag">The tag.</param>
        /// <returns>The value of the tag as a result reason.</returns>
        public static GameTimeControl AsTimeControl(this GameTag gameTag) =>
            TimeControlConverters.TimeControlFromTimeControlString(gameTag.Value);

        /// <summary>
        /// Sets the value of a tag in the format of a <see cref="ResultReason" />.
        /// </summary>
        /// <param name="gameTag">The tag.</param>
        /// <param name="timeControl">The result.</param>
        public static void SetTimeControlValue(this GameTag gameTag, GameTimeControl timeControl) =>
            gameTag.Value = TimeControlConverters.ToStandardTimeControlString(timeControl);
    }
}