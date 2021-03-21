using System;
using System.IO;

namespace RJDev.Outputter.Parsing
{
    public interface IEntryToken
    {
        /// <summary>
        /// Type of token
        /// </summary>
        TokenType TokenType { get; }
        
        /// <summary>
        /// Write token into output text writer
        /// </summary>
        /// <param name="outputTextWriter"></param>
        /// <param name="formatProvider"></param>
        void Write(TextWriter outputTextWriter, IFormatProvider? formatProvider = null);
    }
}