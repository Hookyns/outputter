using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using RJDev.Outputter.Sinks;

namespace RJDev.Outputter
{
    public class Outputter
    {
        /// <summary>
        /// Message buffer.
        /// </summary>
        private readonly BufferBlock<OutputEntry> bufferblock;

        /// <summary>
        /// CTS linked to received token; allowing to cancel manually.
        /// </summary>
        private readonly CancellationTokenSource cancellationTokenSource;

        /// <summary>
        /// True when read completed
        /// </summary>
        private bool readCompleted;

        /// <summary>
        /// True after writer finished.
        /// </summary>
        public bool Completed { get; private set; }

        /// <summary>
        /// Output writer instance.
        /// </summary>
        public OutputWriter OutputWriter { get; }

        /// <summary>
        /// Output reader instance.
        /// </summary>
        public OutputReader OutputReader { get; }

        /// <summary>
        /// Message buffer.
        /// </summary>
        internal BufferBlock<OutputEntry> Bufferblock => this.bufferblock;

        /// <summary>
        /// Ctor
        /// </summary>
        public Outputter(CancellationToken token = default)
        {
            this.cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(token);
            this.bufferblock = new(new DataflowBlockOptions()
            {
                CancellationToken = this.cancellationTokenSource.Token
            });

            this.OutputWriter = new OutputWriter(this);
            this.OutputReader = new OutputReader(this);
        }

        /// <summary>
        /// Complete writing. Close for next writing.
        /// </summary>
        public void Complete()
        {
            if (this.Completed)
            {
                return;
            }
            
            this.Completed = true;
            this.bufferblock.Complete();
        }

        /// <summary>
        /// Wait and get text written to output.
        /// </summary>
        /// <returns></returns>
        internal async IAsyncEnumerable<OutputEntry> Read()
        {
            if (this.readCompleted)
            {
                throw new InvalidOperationException("Reading from completed reader.");
            }

            while (await this.bufferblock.OutputAvailableAsync())
            {
                if (this.bufferblock.TryReceive(out OutputEntry? entry))
                {
                    yield return entry;
                }
            }

            // Invoke Completion of BufferBlock; Cancel token after timeout to prevent deadlock in case that some messages left in buffer
            this.cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(5));
            await this.bufferblock.Completion;
            this.cancellationTokenSource.Dispose();

            this.readCompleted = true;
        }

        /// <summary>
        /// Pipe all writes into sink.
        /// </summary>
        /// <param name="sink"></param>
        internal async Task Pipe(IOutputterSink sink)
        {
            await foreach (OutputEntry entry in this.Read())
            {
                await sink.Emit(entry);
            }
        }
    }
}