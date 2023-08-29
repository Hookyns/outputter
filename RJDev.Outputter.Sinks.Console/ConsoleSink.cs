using System;
using System.IO;
using System.Threading.Tasks;

namespace RJDev.Outputter.Sinks.Console
{
    public class ConsoleSink : IOutputterSink, IDisposable
    {
        /// <summary>
        /// Console sink options.
        /// </summary>
        private readonly ConsoleSinkOptions options;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="options"></param>
        public ConsoleSink(ConsoleSinkOptions options)
        {
            this.options = options;
            Setup();
        }

        /// <inheritdoc />
        public Task Emit(OutputEntry entry)
        {
            TextWriter textWriter = GetOutputStream();
            options.Formatter.Write(entry, textWriter);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Setup sink.
        /// </summary>
        private void Setup()
        {
            System.Console.OutputEncoding = options.ConsoleEncoding;

            if (options.WindowWidth.HasValue)
            {
                System.Console.WindowWidth = options.WindowWidth.Value;
            }
            
            if (options.WindowHeight.HasValue)
            {
                System.Console.WindowHeight = options.WindowHeight.Value;
            }
        }

        /// <summary>
        /// Return console Out stream.
        /// </summary>
        /// <returns></returns>
        private TextWriter GetOutputStream()
        {
            return System.Console.Out;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            // Reset console colors
            System.Console.ResetColor();
        }
    }
}