namespace Aletheia.Pgn.Tests
{
    using System.Linq;
    using Aletheia.Pgn.Parser;
    using Aletheia.Pgn.Parser.Configuration;
    using Xunit;

    public class GameAssemblerTests
    {
        [Fact(DisplayName = "Null tokens turned into null ply moves")]
        public void NullTokensTurnedIntoNullPlyMoves()
        {
            var config = new PgnConfiguration();
            var parser = Game.parseGame(
                TestResources.ShortLegalNullMoveGame,
                new ParserConfiguration());
            var assembledGame = GameAssembler.AssembleGameFromTokens("", parser, config);
            var altLine = assembledGame.MainLineAsList[13].AlternateNextMoves.First();
            Assert.True(altLine.SanIsNullMove);
        }
    }
}