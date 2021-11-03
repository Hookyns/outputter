using System;
using System.IO;

namespace RJDev.Outputter.Parsing
{
    public class ArgToken : IEntryToken
    {
        /// <summary>
        /// Backing field of token type.
        /// </summary>
        private TokenType? tokeType;

        /// <inheritdoc />
        public TokenType TokenType => this.tokeType ??= this.GetTokenType();

        /// <summary>
        /// Format
        /// </summary>
        public string? Format { get; }

        /// <summary>
        /// Name of the property.
        /// </summary>
        /// <remarks>
        /// Eg. for JSON serialization.
        /// </remarks>
        public string PropertyName { get; }

        /// <summary>
        /// Argument value.
        /// </summary>
        public object? Arg { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="format"></param>
        /// <param name="arg"></param>
        public ArgToken(string propertyName, string? format, object? arg)
        {
            this.PropertyName = propertyName;
            this.Format = format;
            this.Arg = arg;
        }

        /// <inheritdoc />
        public void Write(TextWriter outputTextWriter, IFormatProvider? formatProvider = null)
        {
            outputTextWriter.Write(
                string.Format(
                    formatProvider,
                    string.IsNullOrWhiteSpace(this.Format) ? "{0}" : $"{{0:{this.Format}}}",
                    this.Arg
                )
            );
        }

        /// <summary>
        /// Return TokenType based on argument's type.
        /// </summary>
        /// <returns></returns>
        private TokenType GetTokenType()
        {
            if (this.Arg == null)
            {
                return TokenType.ArgumentGeneric;
            }

            Type type = this.Arg.GetType();

            if (type.IsValueType)
            {
                if (type == typeof(DateTime))
                {
                    return TokenType.ArgumentDateTime;
                }

                return TokenType.ArgumentStruct;
            }

            return TokenType.ArgumentObject;
        }
    }
}