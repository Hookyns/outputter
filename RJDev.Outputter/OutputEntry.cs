using System;

namespace RJDev.Outputter
{
	public class OutputEntry
    {
        private object?[] args = Array.Empty<object?>();

        /// <summary>
        /// Message template
        /// </summary>
        public string MessageTemplate => FormattableString?.Format ?? Message ?? string.Empty;

        /// <summary>
        /// Message args
        /// </summary>
        public object?[] Args => FormattableString?.GetArguments() ?? args;

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
            FormattableString = formattableString;
            EntryType = entryType;
		}

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="entryType"></param>
        /// <param name="args"></param>
        public OutputEntry(string message, EntryType entryType, object?[] args)
        {
            Message = message;
            EntryType = entryType;
            this.args = args;
        }
        
        // public <TextToken>  GetTokens

        /// <inheritdoc />
        public override string ToString()
        {
            if (FormattableString == null)
            {
                return string.Format(Message ?? string.Empty, Args);
            }
            
            return FormattableString.ToString();
        }
	}
}