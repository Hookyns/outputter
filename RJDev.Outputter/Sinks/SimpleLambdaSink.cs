using System;
using System.Threading.Tasks;

namespace RJDev.Outputter.Sinks
{
    public class SimpleLambdaSink : IOutputterSink
    {
        /// <summary>
        /// Action used to output messages
        /// </summary>
        private readonly Action<OutputEntry> lambda;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="lambda"></param>
        public SimpleLambdaSink(Action<OutputEntry> lambda)
        {
            this.lambda = lambda;
        }

        /// <inheritdoc />
        public Task Emit(OutputEntry entry)
        {
            this.lambda.Invoke(entry);
            return Task.CompletedTask;
        }
    }
}