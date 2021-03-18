using System;
using System.Globalization;
using Colorify;

namespace RJDev.Outputter.Sinks.Console.Formatting
{
    /// <summary>
    /// Text formatter
    /// </summary>
    public class Formatter : ICustomFormatter, IFormatProvider
    {
        /// <summary>
        /// Colorify's Format
        /// </summary>
        private readonly Format format;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="format"></param>
        public Formatter(Format format)
        {
            this.format = format;
        }

        /// <inheritdoc />
        public object? GetFormat(Type? formatType)
        {
            return formatType == typeof(Formatter) ? this : null;
        }
        
        /// <inheritdoc />
        public string Format(string? format, object? value, IFormatProvider? formatProvider)
        {
            string? stringValue = Convert.ToString(value, CultureInfo.InvariantCulture);
            
            if (stringValue == null)
            {
                return string.Empty;
            }

            // NUMBER
            if (double.TryParse(stringValue, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out double number))
            {
                return this.FormatNumber(format, number);
            }
            
            // BOOL
            if (bool.TryParse(stringValue, out bool boolean))
            {
                return this.FormatBoolean(format, boolean);
            }

            // REST
            return this.FormatString(format, stringValue);
        }

        private string FormatString(string? format, string? stringValue)
        {
            // TODO: Implement
            return string.Empty;
        }

        private string FormatBoolean(string? format, bool boolean)
        {
            // TODO: Implement
            return string.Empty;
        }

        private string FormatNumber(string? format, double result)
        {
            // TODO: Implement
            return string.Empty;
        }
    }
}