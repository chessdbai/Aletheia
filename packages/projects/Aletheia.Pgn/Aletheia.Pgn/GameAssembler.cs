//-----------------------------------------------------------------------
// <copyright file="GameAssembler.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Aletheia.Pgn
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Aletheia.Pgn.Model;
    using Aletheia.Pgn.Parser;

    /// <summary>
    /// Static class with methods related to assembling a <see cref="PgnGame"/> from
    /// a tokenized (recently parsed) pgn game.
    /// </summary>
    internal static class GameAssembler
    {
        /// <summary>
        /// Create a <see cref="PgnGame" /> from a tokenized game hot off being parsed.
        /// </summary>
        /// <param name="tokenizedPgnGame">The just-parsed tokenized game.</param>
        /// <returns>The assembled PgnGame object.</returns>
        internal static PgnGame AssembleGameFromTokens(Game.TokenizedPgnGame tokenizedPgnGame)
        {
            var tagDictionary = tokenizedPgnGame.Tags
                .Select(t => new GameTag()
                {
                    Name = t.Item1,
                    Value = t.Item2,
                }).ToDictionary(
                    tag => tag.Name,
                    tag => new GameTag()
                    {
                        Name = tag.Name,
                        Value = tag.Value,
                    });
            (GamePly firstPly, GameResult _) = ParsePlies(tokenizedPgnGame.Tokens);
            return new PgnGame()
            {
                AllTags = tagDictionary,
                FirstPly = firstPly,
            };
        }

        /// <summary>
        /// Parses a tokenized line.
        /// </summary>
        /// <param name="lineTokens">A collection of tokens from the body of a game, or
        /// from a recursive line.</param>
        /// <returns>The root game ply and the final result token.</returns>
        /// <exception cref="PgnFormatException">Thrown if the content of a result token contained invalid data.</exception>
        internal static (GamePly, GameResult) ParsePlies(IEnumerable<Tokens.Token> lineTokens)
        {
            GameResult resultFromToken = GameResult.Ongoing;
            GamePly currentPly = new GamePly();
            GamePly resultPly = currentPly;
            var tokenBuffer = new List<Tokens.Token>();
            foreach (var t in lineTokens)
            {
                if (t.TryAsPly(out var ply))
                {
                    currentPly.San = ply.San;
                    if (currentPly.PreviousPly != null)
                    {
                        ApplyTokensToPly(currentPly.PreviousPly, tokenBuffer);
                    }

                    tokenBuffer.Clear();
                    currentPly.NextPlyInMainLine = new GamePly();
                    var previousPly = currentPly;
                    currentPly = currentPly.NextPlyInMainLine;
                    currentPly.PreviousPly = previousPly;
                    resultPly = previousPly;
                }
                else if (t.TryAsNull(out var nullMove))
                {
                    currentPly.San = nullMove.NullMoveText;
                    currentPly.SanIsNullMove = true;
                    if (currentPly.PreviousPly != null)
                    {
                        ApplyTokensToPly(currentPly.PreviousPly, tokenBuffer);
                    }

                    tokenBuffer.Clear();
                    currentPly.NextPlyInMainLine = new GamePly();
                    var previousPly = currentPly;
                    currentPly = currentPly.NextPlyInMainLine;
                    currentPly.PreviousPly = previousPly;
                    resultPly = previousPly;
                }
                else if (t.IsComment || t.IsLine || t.IsNag)
                {
                    tokenBuffer.Add(t);
                }
                else if (t.TryAsResult(out var resultToken))
                {
                    if (resultToken.IsWhiteWins)
                    {
                        resultFromToken = GameResult.WhiteWins;
                    }
                    else if (resultToken.IsBlackWins)
                    {
                        resultFromToken = GameResult.BlackWins;
                    }
                    else if (resultToken.IsDraw)
                    {
                        resultFromToken = GameResult.Draw;
                    }
                    else if (resultToken.IsUnspecified)
                    {
                        resultFromToken = GameResult.Ongoing;
                    }
                    else
                    {
                        throw new PgnFormatException($"An unknown result was encountered: '{resultToken}'");
                    }
                }
                else if (t.IsMoveNumber || t.IsBlackMoveNumber)
                {
                    // We don't really care about these
                }
                else
                {
                    throw new PgnFormatException("Unknown token or known tag in incorect context.");
                }
            }

            if (currentPly.PreviousPly == null)
            {
                return (null, resultFromToken);
            }

            GamePly firstPly = resultPly;
            if (resultPly.NextPlyInMainLine?.San == null)
            {
                resultPly.NextPlyInMainLine = null;
            }

            while (firstPly.PreviousPly != null)
            {
                firstPly = firstPly.PreviousPly;
            }

            return (firstPly, resultFromToken);
        }

        private static void ApplyTokensToPly(GamePly ply, List<Tokens.Token> metaTokens)
        {
            foreach (var mt in metaTokens)
            {
                if (mt.TryAsComment(out var comment))
                {
                    var annotations = comment.Parts
                        .Where(p => p.IsAnnotation)
                        .Select(p =>
                        {
                            if (p.TryAsAnnotation(out var annotation))
                            {
                                return annotation;
                            }

                            return null;
                        })
                        .ToList();
                    foreach (var annotation in annotations)
                    {
                        ply.Annotations.Add(new Annotation()
                        {
                            Text = annotation.Text,
                        });
                    }
                }
                else if (mt.TryAsLine(out var line))
                {
                    (GamePly linePly, _) = ParsePlies(line.Line);
                    ply.AlternateNextMoves.Add(linePly);
                }
            }
        }
    }
}