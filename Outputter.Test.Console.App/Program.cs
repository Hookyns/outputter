using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RJDev.Outputter;
using RJDev.Outputter.Formatting.Json;
using RJDev.Outputter.Parsing;
using RJDev.Outputter.Sinks;
using RJDev.Outputter.Sinks.Console;
using RJDev.Outputter.Sinks.Console.Themes;

class Program
{
    static async Task Main(string[] args)
    {
        var outputter = new Outputter();

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
            outputter.OutputWriter.WriteLine($"Visual testing finished \u2713 {DateTime.Now}",
                EntryType.Success); // TODO: toto způsobí chybu


            outputter.Complete();
        });

        Tokenizer tokenizer = new();
        JsonTokenizingFormatter jsonTokenFormatter = new();

        await outputter.OutputReader
                .Pipe(new SimpleLambdaSink(entry =>
                {
                    IEnumerable<IEntryToken> tokens = tokenizer.Tokenize(entry.MessageTemplate, entry.Args);

                    foreach (IEntryToken token in tokens)
                    {
                        token.Write(Console.Out);
                    }
                }))
                .Pipe(new ConsoleSink(new ConsoleSinkOptions(ColorTheme.DarkConsole)))
                .Pipe(new SimpleLambdaSink(entry => { Console.WriteLine(jsonTokenFormatter.Format(entry)); }))
            ;

        // System.Console.OutputEncoding = Encoding.UTF8;
        //
        // string name = "World";
        // Tokenize($"\u2713 Hello {{{name}}} IsThird: {true:|isThird}");
        // System.Console.WriteLine();
        // Tokenize("\u2713 Hello {{{name}}} IsThird: {true}", new object?[]{ "World", true });
    }
}