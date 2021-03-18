using System.IO;
using Colorify;
using Colorify.UI;
using RJDev.Outputter.Formatting;

namespace RJDev.Outputter.Sinks.Console.Formatting
{
    public class ConsoleFormattingWriter : IFormatingWriter
    {
        /// <summary>
        /// Color theme.
        /// </summary>
        private readonly ITheme theme;

        /// <summary>
        /// Text formatter.
        /// </summary>
        private readonly Formatter formatter;
        
        /// <summary>
        /// Colorify's Format
        /// </summary>
        private readonly Format format;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="theme"></param>
        public ConsoleFormattingWriter(ITheme theme)
        {
            this.theme = theme;
            this.format = new Format(theme);
            this.formatter = new Formatter(this.format);
        }

        /// <inheritdoc />
        public void Write(OutputEntry entry, TextWriter outputTextWriter)
        {
            // TODO: Set foreground and background colors by entry.EntryType from theme for whole write. Formatter will change just foreground color for special symbols.
            
            if (entry.FormattableString != null)
            {
                outputTextWriter.Write(entry.FormattableString.ToString(/*this.formatter*/));
            }
            else
            {
                outputTextWriter.Write(entry.MessageTemplate, entry.Args);
            }
            
            this.format.ResetColor();
        }
    }
}