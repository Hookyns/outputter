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
        public TokenType TokenType => tokeType ??= GetTokenType();

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
            PropertyName = propertyName;
            Format = format;
            Arg = arg;
        }

        /// <inheritdoc />
        public void Write(TextWriter outputTextWriter, IFormatProvider? formatProvider = null)
        {
            outputTextWriter.Write(ToString(formatProvider));
        }

        /// <summary>
        /// Convert token to string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToString(null);
        }

        /// <summary>
        /// Convert token to string.
        /// </summary>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public string ToString(IFormatProvider? formatProvider)
        {
            return string.Format(
                formatProvider,
                string.IsNullOrWhiteSpace(Format) ? "{0}" : $"{{0:{Format}}}",
                Arg
            );
        }

        /// <summary>
        /// Return TokenType based on argument's type.
        /// </summary>
        /// <returns></returns>
        private TokenType GetTokenType()
        {
            if (Arg == null)
            {
                return TokenType.ArgumentGeneric;
            }

            Type type = Arg.GetType();

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