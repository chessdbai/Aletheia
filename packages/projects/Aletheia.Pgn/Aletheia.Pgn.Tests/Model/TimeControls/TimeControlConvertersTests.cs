namespace Aletheia.Pgn.Tests.Model.TimeControls
{
    using System.Linq;
    using Aletheia.Pgn.Model.TimeControls;
    using Xunit;

    public class TimeControlConvertersTests
    {
        private const string UntimedTimeControl = "-";
        private const string UnknownTimeControl = "?";
        private const string IncrementalTimeControl = "180+2";
        private const string PeriodTimeControl = "40/3600";
        private const string HourglassTimeControl = "*60";
        private const string SuddenDeathTimeControl = "300";

        [Fact(DisplayName = "Converts untimed time controls correctly.")]
        public void ConvertsUntimedTimeControlCorrectly()
        {
            var gtc = TimeControlConverters.TimeControlFromTimeControlString(UntimedTimeControl);
            Assert.Single(gtc.TimeControlParts);
            Assert.IsType<UntimedTimeControlPart>(gtc.TimeControlParts[0]);
            var gtcString = TimeControlConverters.ToStandardTimeControlString(gtc);
            Assert.Equal(UntimedTimeControl, gtcString);
        }

        [Fact(DisplayName = "Converts unknown time controls correctly.")]
        public void ConvertsUnknownTimeControlCorrectly()
        {
            var gtc = TimeControlConverters.TimeControlFromTimeControlString(UnknownTimeControl);
            Assert.Single(gtc.TimeControlParts);
            Assert.IsType<UnknownTimeControlPart>(gtc.TimeControlParts[0]);
            var gtcString = TimeControlConverters.ToStandardTimeControlString(gtc);
            Assert.Equal(UnknownTimeControl, gtcString);
        }

        [Fact(DisplayName = "Converts incremental time controls correctly.")]
        public void ConvertsIncrementalTimeControlCorrectly()
        {
            var gtc = TimeControlConverters.TimeControlFromTimeControlString(IncrementalTimeControl);
            Assert.Single(gtc.TimeControlParts);
            Assert.IsType<IncrementalTimeControlPart>(gtc.TimeControlParts[0]);
            var incremental = (IncrementalTimeControlPart) gtc.TimeControlParts.First();
            Assert.Equal(3, incremental.TotalDuration.Minutes);
            Assert.Equal(2, incremental.IncrementSeconds);
            var gtcString = TimeControlConverters.ToStandardTimeControlString(gtc);
            Assert.Equal(IncrementalTimeControl, gtcString);
        }

        [Fact(DisplayName = "Converts period time controls correctly.")]
        public void ConvertsPeriodTimeControlCorrectly()
        {
            var gtc = TimeControlConverters.TimeControlFromTimeControlString(PeriodTimeControl);
            Assert.Single(gtc.TimeControlParts);
            Assert.IsType<PeriodTimeControlPart>(gtc.TimeControlParts[0]);
            var period = (PeriodTimeControlPart) gtc.TimeControlParts.First();
            Assert.Equal(40, period.Moves);
            Assert.Equal(1, period.PeriodDuration.Hours);
            var gtcString = TimeControlConverters.ToStandardTimeControlString(gtc);
            Assert.Equal(PeriodTimeControl, gtcString);
        }

        [Fact(DisplayName = "Converts hourglass time controls correctly.")]
        public void ConvertsHourglassControlCorrectly()
        {
            var gtc = TimeControlConverters.TimeControlFromTimeControlString(HourglassTimeControl);
            Assert.Single(gtc.TimeControlParts);
            Assert.IsType<HourglassTimeControlPart>(gtc.TimeControlParts[0]);
            var hourglass = (HourglassTimeControlPart) gtc.TimeControlParts.First();
            Assert.Equal(1, hourglass.TotalDuration.Minutes);
            var gtcString = TimeControlConverters.ToStandardTimeControlString(gtc);
            Assert.Equal(HourglassTimeControl, gtcString);
        }

        [Fact(DisplayName = "Converts sudden death time controls correctly.")]
        public void ConvertsSuddenDeathTimeControlCorrectly()
        {
            var gtc = TimeControlConverters.TimeControlFromTimeControlString(SuddenDeathTimeControl);
            Assert.Single(gtc.TimeControlParts);
            Assert.IsType<SuddenDeathTimeControlPart>(gtc.TimeControlParts[0]);
            var suddenDeath = (SuddenDeathTimeControlPart) gtc.TimeControlParts.First();
            Assert.Equal(5, suddenDeath.TotalDuration.Minutes);
            var gtcString = TimeControlConverters.ToStandardTimeControlString(gtc);
            Assert.Equal(SuddenDeathTimeControl, gtcString);
        }
    }
}