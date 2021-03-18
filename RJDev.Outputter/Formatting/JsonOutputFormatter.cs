// using System;
// using System.Globalization;
//
// namespace RJDev.Outputter.Formatters
// {
//     /// <summary>
//     /// 
//     /// </summary>
//     public class JsonOutputFormatter : IOutputFormatter
//     {
//         /// <inheritdoc />
//         public object? GetFormat(Type? formatType)
//         {
//             return formatType == typeof(JsonOutputFormatter) ? this : null;
//         }
//         
//         /// <inheritdoc />
//         public string Format(string? format, object? value, IFormatProvider? formatProvider)
//         {
//             string? stringValue = Convert.ToString(value, CultureInfo.InvariantCulture);
//             
//             if (stringValue == null)
//             {
//                 return string.Empty;
//             }
//
//             // NUMBER
//             if (double.TryParse(stringValue, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out double number))
//             {
//                 return this.FormatNumber(format, number);
//             }
//             
//             // BOOL
//             if (bool.TryParse(stringValue, out bool boolean))
//             {
//                 return this.FormatBoolean(format, boolean);
//             }
//
//             // REST
//             return this.FormatString(format, stringValue);
//         }
//
//         private string FormatString(string? format, string? stringValue)
//         {
//             // TODO: Implement
//             return string.Empty;
//         }
//
//         private string FormatBoolean(string? format, bool boolean)
//         {
//             // TODO: Implement
//             return string.Empty;
//         }
//
//         private string FormatNumber(string? format, double result)
//         {
//             // TODO: Implement
//             return string.Empty;
//         }
//     }
// }