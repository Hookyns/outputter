using System;
using System.IO;
using System.Threading.Tasks;
using RJDev.Outputter.Sinks.Console.Themes;
using Xunit;

namespace RJDev.Outputter.Sinks.Console.Tests
{
    public class SinkTest
    {
        [Fact]
        public async Task ManualVisualTest()
        {
            ResetConsoleOut();
            
            var outputter = new Outputter();
            using ConsoleSink sink = new ConsoleSink(new ConsoleSinkOptions(ColorTheme.DarkConsole));
            var reader = outputter.OutputReader.Pipe(sink);
            
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
            
            await reader;
        }

        /// <summary>
        /// Reset console output
        /// </summary>
        private static void ResetConsoleOut()
        {
            var standardOutput = new StreamWriter(System.Console.OpenStandardOutput())
            {
                AutoFlush = true
            };
            
            System.Console.SetOut(standardOutput);
        }
    }
}