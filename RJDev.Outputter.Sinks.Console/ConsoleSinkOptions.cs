using System;
using System.Text;
using RJDev.Outputter.Formatting;
using RJDev.Outputter.Sinks.Console.Formatting;
using RJDev.Outputter.Sinks.Console.Themes;

namespace RJDev.Outputter.Sinks.Console
{
    public class ConsoleSinkOptions
    {
        private IFormatingWriter? formatter;
        private IFormattingWriterFactory? formattingWriterFactory;

        /// <summary>
        /// Console.encoding.
        /// </summary>
        public Encoding ConsoleEncoding { get; set; } = Encoding.Default;

        /// <summary>
        /// Formatting writer.
        /// </summary>
        public IFormatingWriter Formatter => this.formatter ??= this.FormattingWriterFactory.Create(this);

        /// <summary>
        /// Formatting writer factory.
        /// </summary>
        public IFormattingWriterFactory FormattingWriterFactory
        {
            get => this.formattingWriterFactory ?? new DefaultFormattingWriterFactory();
            set => this.formattingWriterFactory = value;
        }

        /// <summary>
        /// Theme
        /// </summary>
        public ColorTheme Theme { get; }

        /// <summary>
        /// Default system background color
        /// </summary>
        public ConsoleColor DefaultBackgroundColor { get; }

        /// <summary>
        /// Default system font color.
        /// </summary>
        public ConsoleColor DefaultFontColor { get; }

        /// <summary>
        /// Format provider
        /// </summary>
        public IFormatProvider? FormatProvider { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="theme"></param>
        public ConsoleSinkOptions(ColorTheme theme)
        {
            this.Theme = theme;

            // System.Console.ResetColor(); // MAYDO: Maybe reset to get real system colors?
            this.DefaultFontColor = System.Console.ForegroundColor;
            this.DefaultBackgroundColor = System.Console.BackgroundColor;
        }
    }
}