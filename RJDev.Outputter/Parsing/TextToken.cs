using System;
using System.IO;

namespace RJDev.Outputter.Parsing
{
    public class TextToken : IEntryToken
    {
        /// <inheritdoc />
        public TokenType TokenType => TokenType.Text;
        
        /// <summary>
        /// Text
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="text"></param>
        public TextToken(string text)
        {
            this.Text = text;
        }

        /// <inheritdoc />
        public void Write(TextWriter outputTextWriter, IFormatProvider? formatProvider = null)
        {
            outputTextWriter.Write(this.Text);
        }
    }
}