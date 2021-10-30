using System.Collections.Generic;
using static System.ConsoleColor;

namespace Aletheia.Pgn.Splitter
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using Aletheia.Pgn.Model;
    using Konsole;

    class Program
    {
        /// <summary>
        /// Split a large PGN file into smaller batches.
        /// </summary>
        /// <param name="file">The PGN file.</param>
        /// <param name="batchSize">The batch size.</param>
        /// <param name="skipParse">Skip parsing, just split.</param>
        /// <param name="destPath">The output directory.</param>
        static void Main(
            string file,
            int batchSize = 1000,
            bool skipParse = false,
            string destPath = null)
        {
            var console = new ConcurrentWriter();

            string realDestDir = null;
            if (destPath == null)
            {
                realDestDir = Path.Join(Path.GetDirectoryName(file), "batches");
                Directory.CreateDirectory(realDestDir);
            }
            else if (!Directory.Exists(destPath))
            {
                console.WriteLine(DarkYellow, $"Destination directory '{destPath}' does not exist, creating...");
                try
                {
                    Directory.CreateDirectory(destPath);
                    realDestDir = destPath;
                }
                catch (Exception e)
                {
                    console.WriteLine(Red, $"Unable to create directory '{destPath}': {e.Message}");
                    return;
                }
            }
            else if (!IsDirectoryEmpty(destPath))
            {
                console.WriteLine(Red, $"Destination directory '{destPath}' is not empty.");
                return;
            }

            if (!File.Exists(file))
            {
                console.WriteLine(Red, $"Input PGN file '{file}' does not exist.");
                return;
            }

            PgnGameStream gameStream;
            try
            {
                gameStream = CreateGameStream(file);
            }
            catch (ArgumentException)
            {
                console.WriteLine(Red, $"Input PGN file '{file}' does not exist");
                return;
            }

            int batchCount = 0;
            using (gameStream)
            {
                var batch = new List<string>();
                while (!gameStream.EndOfStream)
                {
                    if (skipParse)
                    {
                        batch.Add(gameStream.GetNextGameString());
                    }
                    else
                    {
                        var nextGame = gameStream.ParseNextGame();
                        batch.Add(nextGame.OriginalPgnText);
                    }


                    if (batch.Count >= batchSize)
                    {
                        string fileName = Path.Join(realDestDir, $"batch{batchCount}.pgn");
                        WriteBatch(batch, fileName);
                        batchCount++;
                        batch.Clear();
                        GC.Collect();
                    }
                }

                if (batch.Count > 0)
                {
                    string fileName = Path.Join(destPath, $"batch{batchCount}.pgn");
                    WriteBatch(batch, fileName);
                    batch.Clear();
                    GC.Collect();
                }
            }
        }

        private static void WriteBatch(List<string> batch, string fileName)
        {
            using (var fileStream = File.OpenWrite(fileName))
            using (var writer = new StreamWriter(fileStream))
            {
                foreach (var g in batch)
                {
                    writer.WriteLine(g);
                    writer.WriteLine();
                }
            }
            Console.WriteLine($"Wrote batch with {batch.Count} games to {fileName}...");
        }

        private static bool IsDirectoryEmpty(string dir) =>
            Directory.GetFiles(dir).Length == 0;

        private static PgnGameStream CreateGameStream(string file)
        {
            if (file.EndsWith(".pgn"))
            {
                return new PgnGameStream(file);
            }
            else if (file.EndsWith(".gz"))
            {
                var fileStream = File.OpenRead(file);
                var compressionStream = new GZipStream(fileStream, CompressionMode.Decompress);
                return new PgnGameStream(compressionStream);
            }
            else
            {
                throw new ArgumentException($"Unknown input file type.");
            }
        }
    }
}