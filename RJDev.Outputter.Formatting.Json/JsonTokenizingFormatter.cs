using System.Collections.Generic;
using Newtonsoft.Json;
using RJDev.Outputter.Parsing;

namespace RJDev.Outputter.Formatting.Json
{
    public class JsonTokenizingFormatter : IFormatter
    {
        /// <summary>
        /// Tokenizer used to tokenize messages.
        /// </summary>
        private readonly Tokenizer tokenizer;

        /// <summary>
        /// Ctor
        /// </summary>
        public JsonTokenizingFormatter()
        {
            tokenizer = new Tokenizer();
        }

        /// <inheritdoc />
        public string Format(OutputEntry entry)
        {
            List<string> entries = new();

            foreach (var token in tokenizer.Tokenize(entry.MessageTemplate, entry.Args))
            {
                entries.Add($"{{\"type\":{(int)token.TokenType},\"value\":{JsonConvert.ToString(token.ToString())}}}");
            }

            return $"{{\"type\":{(int)entry.EntryType},\"tokens\":[" + string.Join(",", entries) + "]}";
        }
    }
}