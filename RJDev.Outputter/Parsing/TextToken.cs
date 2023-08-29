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
            Text = text;
        }

        /// <inheritdoc />
        public void Write(TextWriter outputTextWriter, IFormatProvider? formatProvider = null)
        {
            outputTextWriter.Write(ToString());
        }
        
        /// <summary>
        /// Convert token to string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Text;
        }
    }
}