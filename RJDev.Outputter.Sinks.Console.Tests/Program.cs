using System;
using System.Text;
using System.Threading.Tasks;
using RJDev.Outputter.Sinks.Console.Themes;

namespace RJDev.Outputter.Sinks.Console.Tests
{
    /// <summary>
    /// For manual visual tests in console.
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length >= 1 && args[0] == "console")
            {
                // For LightConsole theme
                // System.Console.BackgroundColor = ConsoleColor.White;
                // System.Console.Clear();
                
                var outputter = new Outputter();
                using ConsoleSink sink = new ConsoleSink(
                    new ConsoleSinkOptions(ColorTheme.DarkConsole)
                    {
                        ConsoleEncoding = Encoding.UTF8,
                        WindowWidth = 200
                    }
                );

                var reader = outputter.OutputReader.Pipe(sink);

                outputter.OutputWriter.WriteLine("Visual testing", EntryType.Major);
                outputter.OutputWriter.WriteLine("{number} Hello World! {boolean}", new { number = 10, boolean = true });
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

                await reader;
            }
            else
            {
                await new SinkTest().ManualVisualTest();
            }
        }
    }
}