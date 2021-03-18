using System.IO;
using System.Threading.Tasks;
using Colorify.UI;
using RJDev.Outputter.Formatting;

namespace RJDev.Outputter.Sinks.Console
{
    public class ConsoleSink : IOutputterSink
    {
        /// <summary>
        /// Color theme.
        /// </summary>
        private readonly ITheme theme;

        /// <summary>
        /// Ouput formatter.
        /// </summary>
        private readonly IFormatingWriter formatter;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="theme"></param>
        /// <param name="formatter"></param>
        public ConsoleSink(ITheme theme, IFormatingWriter formatter)
        {
            this.theme = theme;
            this.formatter = formatter;
        }

        /// <inheritdoc />
        public Task Emit(OutputEntry entry)
        {
            TextWriter textWriter = this.GetOutputStream();
            this.formatter.Write(entry, textWriter);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Return console Out stream
        /// </summary>
        /// <returns></returns>
        private TextWriter GetOutputStream()
        {
            return System.Console.Out;
        }
    }
}