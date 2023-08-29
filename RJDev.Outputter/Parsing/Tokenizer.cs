using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RJDev.Outputter.Parsing
{
    public class Tokenizer
    {
        /// <summary>
        /// Regex parsing arguments format
        /// </summary>
        private static readonly Regex PropertyNameMatcher =
            new("^([a-zA-Z0-9_@]+[a-zA-Z0-9_]*)(?::([^|]*)(?:\\|(.*))?)?", RegexOptions.Compiled);

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

            Lazy<Dictionary<string, object?>?> objectArg = TryGetAnonymousObjectArgument(args);

            int arg = 0;

            while (true)
            {
                int argIndex = GetArgIndex(format);

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
                else
                {
                    format = format[1..];
                }

                int endIndex = format.IndexOf('}');

                if (endIndex == -1)
                {
                    yield return new TextToken(format);
                    yield break;
                }

                yield return ParseArgToken(format[..endIndex], args, arg, objectArg);
                arg++;
                format = format[(endIndex + 1)..];

                if (string.IsNullOrEmpty(format))
                {
                    yield break;
                }
            }
        }

        /// <summary>
        /// Check if args contains just one arguments which is an object/class with properties.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static Lazy<Dictionary<string, object?>?> TryGetAnonymousObjectArgument(object?[] args)
        {
            if (args.Length != 1 || args[0] == null)
            {
                return new Lazy<Dictionary<string, object?>?>(() => null);
            }

            object arg = args[0]!;
            Type argType = arg.GetType();

            if (argType.IsClass)
            {
                return new Lazy<Dictionary<string, object?>?>(() =>
                    arg
                        .GetType()
                        .GetProperties()
                        .ToDictionary(x => x.Name, x => x.GetValue(arg, null))!
                );
            }

            return new Lazy<Dictionary<string, object?>?>(() => null);
        }

        /// <summary>
        /// Get index of argument
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        private static int GetArgIndex(string format)
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
        /// <param name="args"></param>
        /// <param name="argIndex"></param>
        /// <param name="objectArg"></param>
        /// <returns></returns>
        private static ArgToken ParseArgToken(
            string format,
            object?[] args,
            int argIndex,
            Lazy<Dictionary<string, object?>?> objectArg
        )
        {
            Match match = PropertyNameMatcher.Match(format);
            string property = format;
            string? formatting = null;

            if (match.Success)
            {
                property = match.Groups[1].Value;
                formatting = match.Groups[2].Value;

                // TODO: Match property name pipes.
            }

            // Number index?
            if (int.TryParse(property, out int matchedArgIndex))
            {
                argIndex = matchedArgIndex;
                return new ArgToken(property, formatting, args[argIndex]);
            }

            // ELSE - Name of property in object argument
            return new ArgToken(
                property,
                formatting,
                objectArg.Value?[property] ??
                throw new InvalidOperationException("Single argument expected for strings using named parameters.")
            );
        }
    }
}