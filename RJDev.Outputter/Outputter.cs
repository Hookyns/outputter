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
        internal BufferBlock<OutputEntry> Bufferblock => bufferblock;

        /// <summary>
        /// Ctor
        /// </summary>
        public Outputter(CancellationToken token = default)
        {
            cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(token);
            bufferblock = new(new DataflowBlockOptions()
            {
                CancellationToken = cancellationTokenSource.Token
            });

            OutputWriter = new OutputWriter(this);
            OutputReader = new OutputReader(this);
        }

        /// <summary>
        /// Complete writing. Close for next writing.
        /// </summary>
        public void Complete()
        {
            if (Completed)
            {
                return;
            }
            
            Completed = true;
            bufferblock.Complete();
        }

        /// <summary>
        /// Wait and get text written to output.
        /// </summary>
        /// <returns></returns>
        internal async IAsyncEnumerable<OutputEntry> Read()
        {
            if (readCompleted)
            {
                throw new InvalidOperationException("Reading from completed reader.");
            }

            while (await bufferblock.OutputAvailableAsync())
            {
                if (bufferblock.TryReceive(out OutputEntry? entry))
                {
                    yield return entry;
                }
            }

            // Invoke Completion of BufferBlock; Cancel token after timeout to prevent deadlock in case that some messages left in buffer
            cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(5));
            await bufferblock.Completion;
            cancellationTokenSource.Dispose();

            readCompleted = true;
        }

        /// <summary>
        /// Pipe all writes into sink.
        /// </summary>
        /// <param name="sinks"></param>
        internal async Task Pipe(IOutputterSink[] sinks)
        {
            await foreach (OutputEntry entry in Read())
            {
                foreach (IOutputterSink sink in sinks)
                {
                    await sink.Emit(entry);
                }
            }
        }
    }
}