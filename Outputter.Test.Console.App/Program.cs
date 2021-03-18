using System.Threading.Tasks;
using Colorify.UI;
using RJDev.Outputter;
using RJDev.Outputter.Sinks.Console;
using RJDev.Outputter.Sinks.Console.Formatting;

namespace Outputter.Test.Console.App
{
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
                output.WriteLine($"Hello World! {2}");
                await Task.Delay(300);
                output.Write("Hello World!");
                await Task.Delay(300);
                output.Write($" IsThird: {true}");
                outputter.Complete();
            });

            await outputter.OutputReader.Pipe(new ConsoleSink(Theme.Dark, new ConsoleFormattingWriter(Theme.Dark)));
        }
    }
}