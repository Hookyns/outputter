using System;
using System.Collections;
using System.Collections.Generic;
using RJDev.Outputter.Parsing;

namespace RJDev.Outputter.Sinks.Console.Themes
{
	public class ColorTheme : IEnumerable
	{
        /// <summary>
        /// Default dark console theme.
        /// </summary>
		public static readonly ColorTheme DarkConsole = new()
		{
            // Entries
			{ EntryType.General, new Color(ConsoleColor.Gray, ConsoleColor.Black) },
			{ EntryType.Major, new Color(ConsoleColor.White, ConsoleColor.Black) },
			{ EntryType.Minor, new Color(ConsoleColor.DarkGray, ConsoleColor.Black) },
			{ EntryType.Success, new Color(ConsoleColor.DarkGreen, ConsoleColor.Black) },
			{ EntryType.Error, new Color(ConsoleColor.DarkRed, ConsoleColor.Black) },
			{ EntryType.Warn, new Color(ConsoleColor.Yellow, ConsoleColor.Black) },
			{ EntryType.Info, new Color(ConsoleColor.DarkCyan, ConsoleColor.Black) },
            
            // Tokens
			{ TokenType.ArgumentGeneric, new Color(ConsoleColor.DarkMagenta, ConsoleColor.Black) },
			{ TokenType.ArgumentStruct, new Color(ConsoleColor.Magenta, ConsoleColor.Black) },
			{ TokenType.ArgumentDateTime, new Color(ConsoleColor.DarkMagenta, ConsoleColor.Black) },
			{ TokenType.ArgumentObject, new Color(ConsoleColor.DarkBlue, ConsoleColor.Black) },
		};
		
        /// <summary>
        /// Default light console theme.
        /// </summary>
		public static readonly ColorTheme LightConsole = new()
		{
            // Entries
            { EntryType.General, new Color(ConsoleColor.Black, null) },
            { EntryType.Major, new Color(ConsoleColor.DarkGray, null) },
            { EntryType.Minor, new Color(ConsoleColor.Gray, null) },
            { EntryType.Success, new Color(ConsoleColor.DarkGreen, null) },
            { EntryType.Error, new Color(ConsoleColor.DarkRed, null) },
            { EntryType.Warn, new Color(ConsoleColor.DarkYellow, null) },
            { EntryType.Info, new Color(ConsoleColor.DarkCyan, null) },
            
            // Tokens
            { TokenType.ArgumentGeneric, new Color(ConsoleColor.Magenta, null) },
            { TokenType.ArgumentStruct, new Color(ConsoleColor.DarkMagenta, null) },
            { TokenType.ArgumentDateTime, new Color(ConsoleColor.Magenta, null) },
            { TokenType.ArgumentObject, new Color(ConsoleColor.Blue, null) },
		};

		/// <summary>
		/// Dictionary of EntryType colors.
		/// </summary>
		private readonly IDictionary<EntryType, Color> colors = new Dictionary<EntryType, Color>();

        /// <summary>
        /// Dictionary of TokenType colors.
        /// </summary>
        private IDictionary<TokenType, Color> tokenColors = new Dictionary<TokenType, Color>();

        /// <summary>
		/// Add entry type color.
		/// </summary>
		/// <param name="entryType"></param>
		/// <param name="color"></param>
		public void Add(EntryType entryType, Color color)
		{
			this.colors.Add(entryType, color);
		}

        /// <summary>
		/// Add token type color.
		/// </summary>
		/// <param name="tokenType"></param>
		/// <param name="color"></param>
		public void Add(TokenType tokenType, Color color)
		{
			this.tokenColors.Add(tokenType, color);
		}

		/// <inheritdoc />
		public IEnumerator GetEnumerator()
		{
			return this.colors.GetEnumerator();
		}

        /// <summary>
        /// Return color for entry type.
        /// </summary>
        /// <param name="entryType"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public Color GetColor(EntryType entryType)
        {
            if (!this.colors.TryGetValue(entryType, out Color color))
            {
                throw new KeyNotFoundException($"No color in theme found for EntryType {entryType}.");
            }

            return color;
        }

        /// <summary>
        /// Return color for token type.
        /// </summary>
        /// <param name="tokenType"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public bool TryGetTokenColor(TokenType tokenType, out Color color)
        {
            return this.tokenColors.TryGetValue(tokenType, out color);
        }
    }
}