using System.Text;
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
            this.tokenizer = new Tokenizer();
        }

        /// <inheritdoc />
        public string Format(OutputEntry entry)
        {
            StringBuilder sb = new("[");

            foreach (var token in this.tokenizer.Tokenize(entry.MessageTemplate, entry.Args))
            {
                sb.Append($"{{\"type\":{(int)token.TokenType},\"value\":\"{token.ToString().Replace("\n", "\\\\n").Replace("\r", "\\\\r".Replace("\t", "\\\\t"))}\"}}");
            }

            sb.Append(']');
            return sb.ToString();
        }
    }
}