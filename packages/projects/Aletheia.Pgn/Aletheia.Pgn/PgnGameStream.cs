//-----------------------------------------------------------------------
// <copyright file="PgnGameStream.cs" company="ChessDB.AI">
// MIT Licensed.
// </copyright>
//-----------------------------------------------------------------------

namespace Aletheia.Pgn
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Aletheia.Pgn.Model;

    /// <summary>
    /// A class to read multiple PGN games from a <see cref="StreamReader"/>-esque interface.
    /// </summary>
    public class PgnGameStream : IDisposable
    {
        private readonly StreamReader reader;
        private readonly Stream stream;
        private readonly PgnGameParser parser = new PgnGameParser();
        private string leftoverLine = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="PgnGameStream"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public PgnGameStream(Stream stream)
        {
            this.reader = new StreamReader(stream);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PgnGameStream"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="encoding">The encoding of the text in the stream.</param>
        public PgnGameStream(Stream stream, Encoding encoding)
        {
            this.reader = new StreamReader(stream, encoding);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PgnGameStream"/> class.
        /// </summary>
        /// <param name="file">The file to read from.</param>
        public PgnGameStream(string file)
        {
            this.stream = File.OpenRead(file);
            this.reader = new StreamReader(this.stream);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PgnGameStream"/> class.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="encoding">The encoding of the text in the stream.</param>
        public PgnGameStream(string file, Encoding encoding)
        {
            this.stream = File.OpenRead(file);
            this.reader = new StreamReader(this.stream, encoding);
        }

        /// <summary>
        /// Gets a value indicating whether the stream is at its end.
        /// </summary>
        public bool EndOfStream
        {
            get
            {
                return this.reader.EndOfStream;
            }
        }

        /// <summary>
        /// Gets or sets the last parse attempt content.
        /// </summary>
        public string LastAttemptedParseContent { get; set; }

        /// <summary>
        /// Parses the next game in the stream.
        /// </summary>
        /// <returns>The parsed game.</returns>
        public PgnGame ParseNextGame()
        {
            if (this.EndOfStream)
            {
                return null;
            }

            string nextLine = this.GetNextGameString();
            this.LastAttemptedParseContent = nextLine;
            return this.parser.ParseGame(nextLine);
        }

        /// <summary>
        /// Parses all the games left in the stream.
        /// </summary>
        /// <returns>The list of remaining games.</returns>
        public List<PgnGame> ParseRemainingGames()
        {
            var ls = new List<PgnGame>();
            while (!this.EndOfStream)
            {
                ls.Add(this.ParseNextGame());
            }

            return ls;
        }

        /// <inheritdoc cref="IDisposable" />
        public void Dispose() => this.Dispose(true);

        /// <summary>
        /// Dumps all the remaining games to a file.
        /// </summary>
        /// <param name="file">The file to dump games to.</param>
        public void DumpRemainingToFile(string file)
        {
            using (var fs = File.OpenWrite(file))
            using (var writer = new StreamWriter(fs))
            {
                while (!this.EndOfStream)
                {
                    writer.WriteLine(this.reader.ReadLine());
                }
            }
        }

        /// <summary>
        /// Disposes the stream reader object.
        /// </summary>
        /// <param name="disposing">True if we're being called from the dispose method (vs the finalizer).</param>
        protected void Dispose(bool disposing)
        {
            this.reader.Dispose();
        }

        private string GetNextGameString()
        {
            var strBuilder = new StringBuilder();
            if (this.leftoverLine != null)
            {
                strBuilder.AppendLine(this.leftoverLine);
                this.leftoverLine = null;
            }

            do
            {
                strBuilder.AppendLine(this.reader.ReadLine());
            }
            while (this.reader.Peek() == '[' && !this.reader.EndOfStream);

            while (true)
            {
                if (this.reader.EndOfStream)
                {
                    break;
                }

                string nextLine = this.reader.ReadLine();
                if (nextLine.StartsWith("[") && nextLine.EndsWith("\"]"))
                {
                    this.leftoverLine = nextLine;
                    break;
                }
                else if (!nextLine.StartsWith("%"))
                {
                    strBuilder.AppendLine(nextLine);
                }
            }

            return strBuilder.ToString().TrimEnd();
        }
    }
}