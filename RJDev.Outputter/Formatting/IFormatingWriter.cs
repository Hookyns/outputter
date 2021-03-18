using System.IO;

namespace RJDev.Outputter.Formatting
{
    /// <summary>
    /// Output entry formater for sinks.
    /// </summary>
    public interface IFormatingWriter //: ICustomFormatter, IFormatProvider
    {
        /// <summary>
        /// Format entry and write it to the output text writer.
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="outputTextWriter"></param>
        void Write(OutputEntry entry, TextWriter outputTextWriter);
    }
}