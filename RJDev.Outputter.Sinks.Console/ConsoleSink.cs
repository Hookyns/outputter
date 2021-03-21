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
            this.Setup();
        }

        /// <inheritdoc />
        public Task Emit(OutputEntry entry)
        {
            TextWriter textWriter = this.GetOutputStream();
            this.options.Formatter.Write(entry, textWriter);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Setup sink.
        /// </summary>
        private void Setup()
        {
            System.Console.OutputEncoding = this.options.ConsoleEncoding;
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