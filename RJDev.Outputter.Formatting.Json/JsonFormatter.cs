// using System.Text;
// using RJDev.Outputter.Parsing;
//
// namespace RJDev.Outputter.Formatting.Json
// {
//     public class JsonFormatter : IFormatter
//     {
//         /// <summary>
//         /// JSON serializer for values.
//         /// </summary>
//         private readonly JsonSerializer valueSerializer;
//
//         /// <summary>
//         /// Tokenizer used to tokenize messages.
//         /// </summary>
//         private readonly Tokenizer tokenizer;
//
//         /// <summary>
//         /// Ctor
//         /// </summary>
//         public JsonFormatter()
//             : this(new JsonSerializer())
//         {
//         }
//
//         /// <summary>
//         /// Ctor
//         /// </summary>
//         public JsonFormatter(JsonSerializer valueSerializer)
//         {
//             this.valueSerializer = valueSerializer;
//             this.tokenizer = new Tokenizer();
//         }
//
//         /// <inheritdoc />
//         public string Format(OutputEntry entry)
//         {
//             StringBuilder sb = new("[");
//
//             foreach (var token in this.tokenizer.Tokenize(entry.MessageTemplate, entry.Args))
//             {
//                 if (token is TextToken textToken)
//                 {
//                     messageBuilder.Append(textToken.Text);
//                 }
//                 else if (token is ArgToken argToken)
//                 {
//                     messageBuilder.Append("{" + argToken.PropertyName + "}");
//                     args[argToken.PropertyName] = argToken.
//                 }
//                 // TODO: Implement
//                 // if (token)
//             }
//
//             sb.Append(']');
//             return sb.ToString();
//         }
//     }
// }