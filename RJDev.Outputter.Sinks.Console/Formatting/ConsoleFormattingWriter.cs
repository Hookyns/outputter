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
            tokenizer = new Tokenizer();
        }

        /// <inheritdoc />
        public void Write(OutputEntry entry, TextWriter outputTextWriter)
        {
            (ConsoleColor currentForeground, ConsoleColor currentBackground) = GetCurrentColors();
            
            // Get color of entry
            Color color = consoleSinkOptions.Theme.GetColor(entry.EntryType);
            ConsoleColor backgroundColor = color.Background ?? consoleSinkOptions.DefaultBackgroundColor;
            
            // Set color for this entry
            SetConsoleColors(color.Font, backgroundColor);

            IEnumerable<IEntryToken> tokens = tokenizer.Tokenize(entry.MessageTemplate, entry.Args);

            foreach (IEntryToken token in tokens)
            {
                // Try to set token color if defined in template
                if (token.TokenType != TokenType.Text && consoleSinkOptions.Theme.TryGetTokenColor(token.TokenType, out Color tokenColor))
                {
                    SetConsoleColors(tokenColor.Font, tokenColor.Background ?? consoleSinkOptions.DefaultBackgroundColor);
                }

                token.Write(outputTextWriter, consoleSinkOptions.FormatProvider);
                
                // Restore entry color
                SetConsoleColors(color.Font, backgroundColor);
            }
            
            // Restore original colors
            SetConsoleColors(currentForeground, currentBackground);
        }

        /// <summary>
        /// Get current colors set in console.
        /// </summary>
        /// <returns></returns>
        private static (ConsoleColor foreground, ConsoleColor background) GetCurrentColors()
        {
            return (System.Console.ForegroundColor, System.Console.BackgroundColor);
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