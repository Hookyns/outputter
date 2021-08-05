using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using RJDev.Outputter.Sinks;

namespace RJDev.Outputter
{
    public sealed class OutputReaderChain
    {
        /// <summary>
        /// Output reader
        /// </summary>
        private readonly OutputReader outputReader;

        /// <summary>
        /// Emitting function
        /// </summary>
        private readonly Func<IOutputterSink[], Task> emitter;
        
        /// <summary>
        /// List of configured sinks
        /// </summary>
        private readonly List<IOutputterSink> sinkList = new();

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="outputReader"></param>
        /// <param name="firstSink"></param>
        /// <param name="emitter"></param>
        internal OutputReaderChain(OutputReader outputReader, IOutputterSink firstSink, Func<IOutputterSink[], Task> emitter)
        {
            this.outputReader = outputReader;
            this.emitter = emitter;
            this.sinkList.Add(firstSink);
        }
        
        /// <summary>
        /// Wait and get text entry to output.
        /// </summary>
        /// <returns></returns>
        public async IAsyncEnumerable<OutputEntry> Read()
        {
            await foreach (OutputEntry entry in this.outputReader.Read())
            {
                foreach (var sink in this.sinkList)
                {
                    await sink.Emit(entry);
                }
                
                yield return entry;
            }
        }
        
        /// <summary>
        /// Pipe all writes into sink.
        /// </summary>
        /// <param name="sink"></param>
        public OutputReaderChain Pipe(IOutputterSink sink)
        {
            this.sinkList.Add(sink);
            return this;
        }

        /// <summary>
        /// Return awaiter
        /// </summary>
        /// <returns></returns>
        public TaskAwaiter GetAwaiter()
        {
            return this.emitter(this.sinkList.ToArray()).GetAwaiter();
        }
    }
}