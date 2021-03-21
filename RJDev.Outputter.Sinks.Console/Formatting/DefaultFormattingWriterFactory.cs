using RJDev.Outputter.Formatting;

namespace RJDev.Outputter.Sinks.Console.Formatting
{
    public class DefaultFormattingWriterFactory : IFormattingWriterFactory
    {
        /// <inheritdoc />
        public IFormatingWriter Create(ConsoleSinkOptions consoleSinkOptions)
        {
            return new ConsoleFormattingWriter(consoleSinkOptions);
        }
    }
}