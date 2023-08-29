using System.Collections.Generic;
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
        /// Wait and get entry written to output.
        /// </summary>
        /// <returns></returns>
        public IAsyncEnumerable<OutputEntry> Read()
        {
            return outputter.Read();
        }

        /// <summary>
        /// Pipe all writes into sink.
        /// </summary>
        /// <param name="sink"></param>
        public OutputReaderChain Pipe(IOutputterSink sink)
        {
            return new OutputReaderChain(this, sink, outputter.Pipe);
        }
    }
}