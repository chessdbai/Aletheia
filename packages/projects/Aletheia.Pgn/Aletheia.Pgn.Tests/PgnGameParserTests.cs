namespace Aletheia.Pgn.Tests
{
    using System.Linq;
    using Xunit;

    public class PgnGameParserTests
    {
        [Fact(DisplayName = "Parses chess.com game.")]
        public void ParsesLichessOrgGame()
        {
            var parser = new PgnGameParser();
            var game = parser.ParseGame(TestResources.JohnDavisBeatsSieica);
            var mainLineList = game.MainLineAsList;
            Assert.Equal(97, mainLineList.Count);

            var firstPly = mainLineList.First();
            Assert.Equal("e4", firstPly.San);
        }

        [Fact(DisplayName = "Can parse debug game.")]
        public void CanParseDebugGame()
        {
            var parser = new PgnGameParser();
            var game = parser.ParseGame(TestResources.GameWithNullComment);
            var mainLineList = game.MainLineAsList;
            Assert.Equal(103, mainLineList.Count);
        }

        [Fact(DisplayName = "Player information is extracted from the header tags correctly.")]
        public void PlayerInformationExtractsCorrectly()
        {
            var parser = new PgnGameParser();
            var game = parser.ParseGame(TestResources.JohnDavisBeatsSieica);
            Assert.Equal("johndavis_59", game.WhitePlayer.Name);
            Assert.Equal(1972, game.WhitePlayer.Rating!.Value);
            Assert.Null(game.WhitePlayer.Title);
        }
    }
}