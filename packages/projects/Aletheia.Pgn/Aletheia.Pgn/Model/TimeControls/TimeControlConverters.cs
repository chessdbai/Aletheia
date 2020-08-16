//-----------------------------------------------------------------------
// <copyright file="TimeControlConverters.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Aletheia.Pgn.Model.TimeControls
{
    using System;
    using System.Linq;
    using Aletheia.Pgn.Parser;

    /// <summary>
    /// Static class with methods for converting to and from
    /// the time control types.
    /// </summary>
    public static class TimeControlConverters
    {
        /// <summary>
        /// Convert a string to a TimeControl object.
        /// </summary>
        /// <param name="timeControlString">The time control string.</param>
        /// <returns>The time control object.</returns>
        public static GameTimeControl TimeControlFromTimeControlString(string timeControlString)
        {
            var standardParts = TimeControl
                .parseTimeControl(timeControlString)
                .Select(DescriptorToTimeControlPart)
                .ToList();

            return new GameTimeControl()
            {
                TimeControlParts = standardParts,
            };
        }

        /// <summary>
        /// Convert a string to a TimeControl object.
        /// </summary>
        /// <param name="timeControl">The time control object.</param>
        /// <returns>The standardized PGN time control string.</returns>
        public static string ToStandardTimeControlString(GameTimeControl timeControl) =>
            string.Join(":", timeControl.TimeControlParts.Select(TimeControlPartToTimeControlString));

        /// <summary>
        /// Convert a single time control descriptor into a time control part.
        /// </summary>
        /// <param name="descriptor">The time control descriptor.</param>
        /// <returns>The time control part.</returns>
        internal static TimeControlPart DescriptorToTimeControlPart(
            TimeControl.TimeControlDescriptor descriptor) => descriptor switch
        {
            TimeControl.TimeControlDescriptor.Hourglass hourglass => new HourglassTimeControlPart()
            {
                TotalDuration = TimeSpan.FromSeconds(hourglass.Item.TimerSeconds),
            },
            TimeControl.TimeControlDescriptor.Incremental incremental => new IncrementalTimeControlPart()
            {
                IncrementSeconds = incremental.Item.IncrementSeconds,
                TotalDuration = TimeSpan.FromSeconds(incremental.Item.StartingSeconds),
            },
            TimeControl.TimeControlDescriptor.TimeControlPeriod period => new PeriodTimeControlPart()
            {
                PeriodDuration = TimeSpan.FromSeconds(period.Item.Seconds),
                Moves = period.Item.Moves,
            },
            TimeControl.TimeControlDescriptor.SuddenDeathTime period => new SuddenDeathTimeControlPart()
            {
                TotalDuration = TimeSpan.FromSeconds(period.Item.Seconds),
            },
            TimeControl.TimeControlDescriptor.Unknown _ => new UnknownTimeControlPart(),
            TimeControl.TimeControlDescriptor.Untimed _ => new UntimedTimeControlPart(),
            _ => throw new ArgumentException($"Unknown time control type: '{descriptor}'"),
        };

        /// <summary>
        /// Convert a single time control part into its string representation.
        /// </summary>
        /// <param name="part">The time control part.</param>
        /// <returns>The string representation of the time control part.</returns>
        internal static string TimeControlPartToTimeControlString(
            TimeControlPart part) => part switch
        {
            HourglassTimeControlPart hourglass => $"*{(int)hourglass.TotalDuration.TotalSeconds}",
            IncrementalTimeControlPart incremental => $"{(int)incremental.TotalDuration.TotalSeconds}+{incremental.IncrementSeconds}",
            PeriodTimeControlPart period => $"{period.Moves}/{(int)period.PeriodDuration.TotalSeconds}",
            SuddenDeathTimeControlPart suddenDeath => $"{(int)suddenDeath.TotalDuration.TotalSeconds}",
            UnknownTimeControlPart _ => $"?",
            UntimedTimeControlPart _ => $"-",
            _ => throw new ArgumentException($"Unknown time control type: '{part.GetType()}'"),
        };
    }
}