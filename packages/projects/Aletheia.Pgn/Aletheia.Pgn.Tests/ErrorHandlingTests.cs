namespace Aletheia.Pgn.Tests
{
    using System;
    using Aletheia.Pgn.Parser;
    using Xunit;

    public class ErrorHandlingTests
    {
        [Fact(DisplayName = "Produces useful exceptions with properties set.")]
        public void ProducesUsefulExceptions()
        {
            var parser = new PgnGameParser();

            Exception parseEx = null;
            try
            {
                parser.ParseGame(TestResources.GameWithIllegalNullMoveIndicator);
            }
            catch (Exception e)
            {
                parseEx = e;
            }

            Assert.NotNull(parseEx);
            Assert.IsType<Game.PgnParsingException>(parseEx);
            var parsingEx = (Game.PgnParsingException) parseEx;
            Assert.Equal(35, parsingEx.Line);
            Assert.Equal(43, parsingEx.Column);
        }
    }
}