using RJDev.Outputter.Formatting;

namespace RJDev.Outputter.Sinks.Console.Formatting
{
    public interface IFormattingWriterFactory
    {
        /// <summary>
        /// Create instance of formatting writer.
        /// </summary>
        /// <param name="consoleSinkOptions"></param>
        /// <returns></returns>
        IFormatingWriter Create(ConsoleSinkOptions consoleSinkOptions);
    }
}