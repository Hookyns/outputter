using System;

namespace RJDev.Outputter
{
	public class OutputEntry
    {
        private object?[] args = new object?[0];

        /// <summary>
        /// Message template
        /// </summary>
        public string MessageTemplate => this.FormattableString?.Format ?? this.Message ?? string.Empty;

        /// <summary>
        /// Message args
        /// </summary>
        public object?[] Args => this.FormattableString?.GetArguments() ?? this.args;

        /// <summary>
        /// Original formatable string
        /// </summary>
        public FormattableString? FormattableString { get; }
        
        /// <summary>
        /// Type of entry
        /// </summary>
        public EntryType EntryType { get; }

		/// <summary>
		/// Result message
		/// </summary>
		public string? Message { get; }

		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="formattableString"></param>
		/// <param name="entryType"></param>
		public OutputEntry(FormattableString formattableString, EntryType entryType)
        {
            this.FormattableString = formattableString;
            this.EntryType = entryType;
		}

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="entryType"></param>
        /// <param name="args"></param>
        public OutputEntry(string message, EntryType entryType, object?[] args)
        {
            this.Message = message;
            this.EntryType = entryType;
            this.args = args;
        }
        
        // public <TextToken>  GetTokens

        /// <inheritdoc />
        public override string ToString()
        {
            return this.FormattableString?.ToString() ?? this.Message ?? string.Empty;
        }
	}
}