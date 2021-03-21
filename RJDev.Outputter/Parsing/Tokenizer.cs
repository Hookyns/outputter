using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RJDev.Outputter.Parsing
{
    public class Tokenizer
    {
        /// <summary>
        /// Regex parsing arguments format
        /// </summary>
        private static readonly Regex PropertyNameMatcher = new("^([a-zA-Z0-9_@]+[a-zA-Z0-9_]*)(?::([^|]*)(?:\\|(.*))?)?", RegexOptions.Compiled);
        
        /// <summary>
        /// Tokenize string format
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args">Arguments used in format</param>
        /// <returns></returns>
        public IEnumerable<IEntryToken> Tokenize(string format, object?[] args)
        {
            if (string.IsNullOrEmpty(format))
            {
                yield return new TextToken(string.Empty);
                yield break;
            }

            int arg = 0;

            while (true)
            {
                int argIndex = this.GetArgIndex(format);

                if (argIndex == -1)
                {
                    yield return new TextToken(format);
                    yield break;
                }

                if (argIndex != 0)
                {
                    yield return new TextToken(format[..argIndex]);
                    format = format[(argIndex + 1)..];
                }

                int endIndex = format.IndexOf('}');
                yield return this.ParseArgToken(format[..endIndex], args[arg]);
                arg++;
                format = format[(endIndex + 1)..];
            }
        }

        /// <summary>
        /// Get index of argument
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        private int GetArgIndex(string format)
        {
            int offset = 0;

            while (true)
            {
                int findIndex = format.IndexOf('{', offset);

                if (findIndex == -1)
                {
                    return -1;
                }

                if (format[findIndex + 1] != '{')
                {
                    return findIndex;
                }

                offset = findIndex + 2;
            }
        }

        /// <summary>
        /// Parse Argument token
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        private ArgToken ParseArgToken(string format, object? arg)
        {
            Match match = PropertyNameMatcher.Match(format);

            if (match.Success)
            {
                return new ArgToken(match.Groups[1].Value, match.Groups[2].Value, arg);
            }
            
            return new ArgToken(format, string.Empty, arg);
        }
    }
}