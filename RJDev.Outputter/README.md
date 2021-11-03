# Outputter
> Manager of Your application or library text output.

Imagine you are building library which has async semantic text output, which can be formatted and written into the console, file or streamed somewhere else, all at once.
Well that is what the Outputter can do.

## Examples
Simple example:
```c#
using RJDev.Outputter;
using RJDev.Outputter.Sinks;

// Create outputter instance
var outputter = new Outputter();

// Write something into the output
outputter.OutputWriter.WriteLine($"Hello World! {Math.PI:N4}");
outputter.OutputWriter.WriteLine("Hello World! {0:N4}", Math.PI);

// No more messages expected
outputter.Complete();


// Use Pipe() to consume output entries; can/should be async
await outputter.OutputReader
    .Pipe(new SimpleLambdaSink(entry =>
    {
        Console.Write(entry.ToString());
    }));
```

Example using Tokenizer:
```c#
using RJDev.Outputter;
using RJDev.Outputter.Sinks;
using RJDev.Outputter.Parsing;

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
```