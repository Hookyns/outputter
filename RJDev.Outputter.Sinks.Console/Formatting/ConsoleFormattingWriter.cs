using System;
using System.Collections.Generic;
using System.IO;
using RJDev.Outputter.Formatting;
using RJDev.Outputter.Parsing;
using RJDev.Outputter.Sinks.Console.Themes;

namespace RJDev.Outputter.Sinks.Console.Formatting
{
    public class ConsoleFormattingWriter : IFormatingWriter
    {
        /// <summary>
        /// Console sink options.
        /// </summary>
        private readonly ConsoleSinkOptions consoleSinkOptions;

        /// <summary>
        /// Tokenizer instance
        /// </summary>
        private readonly Tokenizer tokenizer;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="consoleSinkOptions"></param>
        public ConsoleFormattingWriter(ConsoleSinkOptions consoleSinkOptions)
        {
            this.consoleSinkOptions = consoleSinkOptions;
            this.tokenizer = new Tokenizer();
        }

        /// <inheritdoc />
        public void Write(OutputEntry entry, TextWriter outputTextWriter)
        {
            // Get color of entry
            Color color = this.consoleSinkOptions.Theme.GetColor(entry.EntryType);
            ConsoleColor backgroundColor = color.Background ?? this.consoleSinkOptions.DefaultBackgroundColor;
            
            // Set color for this entry
            SetConsoleColors(color.Font, backgroundColor);

            IEnumerable<IEntryToken> tokens = this.tokenizer.Tokenize(entry.MessageTemplate, entry.Args);

            foreach (IEntryToken token in tokens)
            {
                // Try to set token color if defined in template
                if (token.TokenType != TokenType.Text && this.consoleSinkOptions.Theme.TryGetTokenColor(token.TokenType, out Color tokenColor))
                {
                    SetConsoleColors(tokenColor.Font, tokenColor.Background ?? this.consoleSinkOptions.DefaultBackgroundColor);
                }

                token.Write(outputTextWriter, this.consoleSinkOptions.FormatProvider);
                
                // Restore entry color
                SetConsoleColors(color.Font, backgroundColor);
            }
        }

        /// <summary>
        /// Set console colors.
        /// </summary>
        /// <param name="foregroundColor"></param>
        /// <param name="backgroundColor"></param>
        private static void SetConsoleColors(ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            if (System.Console.ForegroundColor != foregroundColor)
            {
                System.Console.ForegroundColor = foregroundColor;
            }

            if (System.Console.BackgroundColor != backgroundColor)
            {
                System.Console.BackgroundColor = backgroundColor;
            }
        }
    }
}