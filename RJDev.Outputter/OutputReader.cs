using System.Collections.Generic;
using System.Threading.Tasks;
using RJDev.Outputter.Sinks;

namespace RJDev.Outputter
{
    public class OutputReader
    {
        /// <summary>
        /// Instance of Outputter
        /// </summary>
        private readonly Outputter outputter;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="outputter"></param>
        public OutputReader(Outputter outputter)
        {
            this.outputter = outputter;
        }

        /// <summary>
        /// Wait and get text written to output.
        /// </summary>
        /// <returns></returns>
        public IAsyncEnumerable<OutputEntry> Read()
        {
            return this.outputter.Read();
        }

        /// <summary>
        /// Pipe all writes into sink.
        /// </summary>
        /// <param name="sink"></param>
        public async Task Pipe(IOutputterSink sink)
        {
            // TODO: Make it right. Allow not awaiting returning Task -> like async void Pipe(). Exception must be handled right for this case.
            await this.outputter.Pipe(sink);
        }
    }
}