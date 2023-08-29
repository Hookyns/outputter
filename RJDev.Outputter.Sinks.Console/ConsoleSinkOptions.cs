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
        /// Formatting writer.
        /// </summary>
        internal IFormatingWriter Formatter => formatter ??= FormattingWriterFactory.Create(this);

        /// <summary>
        /// Theme
        /// </summary>
        internal ColorTheme Theme { get; }

        /// <summary>
        /// Default system background color
        /// </summary>
        internal ConsoleColor DefaultBackgroundColor { get; }

        /// <summary>
        /// Default system font color.
        /// </summary>
        internal ConsoleColor DefaultFontColor { get; }

        /// <summary>
        /// Console.encoding.
        /// </summary>
        public Encoding ConsoleEncoding { internal get; set; } = Encoding.Default;

        /// <summary>
        /// Formatting writer factory.
        /// </summary>
        public IFormattingWriterFactory FormattingWriterFactory
        {
            internal get => formattingWriterFactory ?? new DefaultFormattingWriterFactory();
            set => formattingWriterFactory = value;
        }

        /// <summary>
        /// Format provider
        /// </summary>
        public IFormatProvider? FormatProvider { internal get; set; }
        
        /// <summary>
        /// Console window width
        /// </summary>
        public int? WindowWidth { internal get; set; }
        
        /// <summary>
        /// Console window height
        /// </summary>
        public int? WindowHeight { internal get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="theme"></param>
        public ConsoleSinkOptions(ColorTheme theme)
        {
            Theme = theme;

            // System.Console.ResetColor(); // MAYDO: Maybe reset to get real system colors?
            DefaultFontColor = System.Console.ForegroundColor;
            DefaultBackgroundColor = System.Console.BackgroundColor;
        }
    }
}