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
        /// <param name="pgnText">The original pgn text.</param>
        /// <param name="tokenizedPgnGame">The just-parsed tokenized game.</param>
        /// <param name="pgnConfiguration">The configuration to use for the game.</param>
        /// <returns>The assembled PgnGame object.</returns>
        internal static PgnGame AssembleGameFromTokens(
            string pgnText,
            Game.TokenizedPgnGame tokenizedPgnGame,
            PgnConfiguration pgnConfiguration)
        {
            var tagDictionary = tokenizedPgnGame.Tags
                .Select(t => new GameTag()
                {
                    Name = t.Item1,
                    Value = t.Item2,
                })
                .GroupBy(tag => tag.Name)
                .ToDictionary(
                    grp => grp.Key,
                    grp => new GameTag()
                    {
                        Name = grp.First().Name,
                        Value = grp.First().Value,
                    });
            (GamePly firstPly, GameResult _) = ParsePlies(tokenizedPgnGame.Tokens, pgnConfiguration);
            return new PgnGame()
            {
                AllTags = tagDictionary,
                FirstPly = firstPly,
                OriginalPgnText = pgnText,
            };
        }

        /// <summary>
        /// Parses a tokenized line.
        /// </summary>
        /// <param name="lineTokens">A collection of tokens from the body of a game, or
        /// from a recursive line.</param>
        /// <param name="pgnConfiguration">The configuration to use for the game.</param>
        /// <returns>The root game ply and the final result token.</returns>
        /// <exception cref="PgnFormatException">Thrown if the content of a result token contained invalid data.</exception>
        internal static (GamePly, GameResult) ParsePlies(
            IEnumerable<Tokens.Token> lineTokens,
            PgnConfiguration pgnConfiguration)
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
                        ApplyTokensToPly(
                            currentPly.PreviousPly,
                            tokenBuffer,
                            pgnConfiguration);
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
                    if (pgnConfiguration.RewriteSan)
                    {
                        currentPly.San = RewriteSan(currentPly.San, pgnConfiguration);
                    }

                    currentPly.SanIsNullMove = true;
                    if (currentPly.PreviousPly != null)
                    {
                        ApplyTokensToPly(
                            currentPly.PreviousPly,
                            tokenBuffer,
                            pgnConfiguration);
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

        private static void ApplyTokensToPly(
            GamePly ply,
            List<Tokens.Token> metaTokens,
            PgnConfiguration pgnConfiguration)
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
                    (GamePly linePly, _) = ParsePlies(line.Line, pgnConfiguration);
                    ply.AlternateNextMoves.Add(linePly);
                }
            }
        }

        private static string RewriteSan(string san, PgnConfiguration pgnConfiguration)
        {
            string rewritten = san;
            foreach (var inputCharset in pgnConfiguration.InputCharsets)
            {
                (var pawnSan, var pawn) = ReplaceAny(
                    san,
                    inputCharset.PawnChars.ToArray(),
                    pgnConfiguration.OutputCharsets.PawnChars.First());
                if (pawn)
                {
                    return pawnSan;
                }

                (var bishopSan, var bishop) = ReplaceAny(
                    san,
                    inputCharset.BishopChars.ToArray(),
                    pgnConfiguration.OutputCharsets.BishopChars.First());
                if (bishop)
                {
                    return bishopSan;
                }

                (var knightSan, var knight) = ReplaceAny(
                    san,
                    inputCharset.KnightChars.ToArray(),
                    pgnConfiguration.OutputCharsets.RookChars.First());
                if (knight)
                {
                    return knightSan;
                }

                (var rookSan, var rook) = ReplaceAny(
                    san,
                    inputCharset.RookChars.ToArray(),
                    pgnConfiguration.OutputCharsets.RookChars.First());
                if (rook)
                {
                    return rookSan;
                }

                (var queenSan, var queen) = ReplaceAny(
                    san,
                    inputCharset.QueenChars.ToArray(),
                    pgnConfiguration.OutputCharsets.QueenChars.First());
                if (queen)
                {
                    return queenSan;
                }

                (var kingSan, var king) = ReplaceAny(
                    san,
                    inputCharset.KingChars.ToArray(),
                    pgnConfiguration.OutputCharsets.KingChars.First());
                if (king)
                {
                    return kingSan;
                }
            }

            return rewritten;
        }

        private static (string, bool) ReplaceAny(string text, char[] chars, char newChar)
        {
            string rewritten = text;
            bool replaced = false;
            foreach (var c in chars)
            {
                if (rewritten.Contains(c))
                {
                    replaced = true;
                    rewritten = rewritten.Replace(c, newChar);
                }
            }

            return (rewritten, replaced);
        }
    }
}