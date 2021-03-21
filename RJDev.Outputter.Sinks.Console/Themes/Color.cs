using System;

namespace RJDev.Outputter.Sinks.Console.Themes
{
    public readonly struct Color
    {
        /// <summary>
        /// Font color
        /// </summary>
        public ConsoleColor Font { get; }

        /// <summary>
        /// Background color
        /// </summary>
        public ConsoleColor? Background { get; }
        
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="font"></param>
        /// <param name="background"></param>
        public Color(ConsoleColor font, ConsoleColor? background)
        {
            this.Font = font;
            this.Background = background;
        }
    }
}