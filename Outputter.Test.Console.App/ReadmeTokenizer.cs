using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RJDev.Outputter;
using RJDev.Outputter.Parsing;
using RJDev.Outputter.Sinks;

class ReadmeTokenizer
{
    public static async Task Main()
    {
        // Create outputter instance
        var outputter = new Outputter();

        // Write something into the output
        outputter.OutputWriter.WriteLine($"Hello World! {Math.PI:N4}");
        outputter.OutputWriter.WriteLine("Hello World! {0:N4}", Math.PI);
        
        // No more messages expected
        outputter.Complete();

        // Use Pipe() to consume output entries; can/should be async
        Tokenizer tokenizer = new(); // Tokenizer is used to tokenize entries into parts
        
        await outputter.OutputReader
            .Pipe(new SimpleLambdaSink(entry =>
            {
                // Tokenize template
                IEnumerable<IEntryToken> tokens = tokenizer.Tokenize(entry.MessageTemplate, entry.Args);
                
                foreach (IEntryToken token in tokens)
                {
                    // If it is any of the argument tokens, use colored write
                    if ((token.TokenType & TokenType.Argument) != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        token.Write(Console.Out);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        token.Write(Console.Out);
                    }
                }
            }));
    }
}