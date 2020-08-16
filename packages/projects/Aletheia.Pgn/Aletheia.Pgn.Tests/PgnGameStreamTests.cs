namespace Aletheia.Pgn.Tests
{
    using System.IO;
    using System.Text;
    using Xunit;

    public class PgnGameStreamTests
    {
        [Fact(DisplayName = "Ignores curator comment")]
        public void IgnoresCuratorComment()
        {
            using var ms = new MemoryStream(Encoding.UTF8.GetBytes(TestResources.GameWithCuratorComment));
            var gameStream = new PgnGameStream(ms);
            var games = gameStream.ParseRemainingGames();
            Assert.Equal(2, games.Count);
        }
    }
}