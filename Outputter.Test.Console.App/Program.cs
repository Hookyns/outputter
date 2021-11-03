using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RJDev.Outputter;
using RJDev.Outputter.Parsing;
using RJDev.Outputter.Sinks;

// namespace JetBrains.Annotations
// {
//     public class StringFormatMethodAttribute : Attribute
//     {
//         public StringFormatMethodAttribute(string formatType)
//         {
//         }
//     }
// }

namespace Outputter.Test.Console.App
{
    class A
    {
        public string Foo { get; set; }
        
        public A()
        {
            
        }
    }
    
    class Program
    {
        static async Task Main(string[] args)
        {
            var outputter = new RJDev.Outputter.Outputter();
            
            Task writeTask = Task.Run(async () =>
            {
                OutputWriter output = outputter.OutputWriter;
                
                output.WriteLine($"Hello World!");
                await Task.Delay(300);
                output.WriteLine($"Hello World! {2:N}");
                await Task.Delay(300);
                
                output.WriteLine("Hello World! {0}", 10);
                output.WriteLine("Hello World! {number}", new { number = 10 });
                await Task.Delay(300);
                output.Write("Hello World!");
                await Task.Delay(300);
                output.Write($" IsThird: {true:|isThird}\u2713");
                
                outputter.OutputWriter.WriteLine("Visual testing", EntryType.Major);
                outputter.OutputWriter.WriteLine("".PadLeft(80, '='), EntryType.Minor);

                outputter.OutputWriter.WriteLine("Hello number {0:N4}!", Math.PI);
                outputter.OutputWriter.WriteLine($"{1.5:N2} Hello World! {true:|booleanName}");
                outputter.OutputWriter.WriteLine($"Obj {new { Foo = "bar" }}");
                
                outputter.OutputWriter.WriteLine("Some error happened", EntryType.Error);
                outputter.OutputWriter.WriteLine("Some warn", EntryType.Warn);
                outputter.OutputWriter.WriteLine("Some info", EntryType.Info);
                
                outputter.OutputWriter.WriteLine("".PadLeft(80, '='), EntryType.Minor);
                outputter.OutputWriter.WriteLine($"Visual testing finished \u2713 {DateTime.Now}", EntryType.Success);
                
                
                outputter.Complete();
            });

            var tokenizer = new Tokenizer();
            
            await outputter.OutputReader
                .Pipe(new SimpleLambdaSink(entry =>
                {
                    IEnumerable<IEntryToken> tokens = tokenizer.Tokenize(entry.MessageTemplate, entry.Args);

                    foreach (IEntryToken token in tokens)
                    {
                        token.Write(System.Console.Out);
                    }
                }))
                .Pipe(new SimpleLambdaSink(entry =>
                {
                    IEnumerable<IEntryToken> tokens = tokenizer.Tokenize(entry.MessageTemplate, entry.Args);

                    foreach (IEntryToken token in tokens)
                    {
                        System.Console.ForegroundColor = (TokenType.Argument & token.TokenType) != 0 ? ConsoleColor.Cyan : ConsoleColor.Red;
                        token.Write(System.Console.Out);
                        System.Console.ForegroundColor = ConsoleColor.White;
                    }
                }))
                ;

            // System.Console.OutputEncoding = Encoding.UTF8;
            //
            // string name = "World";
            // Tokenize($"\u2713 Hello {{{name}}} IsThird: {true:|isThird}");
            // System.Console.WriteLine();
            // Tokenize("\u2713 Hello {{{name}}} IsThird: {true}", new object?[]{ "World", true });
        }
    }
}