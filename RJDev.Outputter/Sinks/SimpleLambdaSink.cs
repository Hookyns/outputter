using System;
using System.Threading.Tasks;

namespace RJDev.Outputter.Sinks
{
    public class SimpleLambdaSink : IOutputterSink
    {
        /// <summary>
        /// Action used to output messages
        /// </summary>
        private readonly Action<OutputEntry> labda;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="labda"></param>
        public SimpleLambdaSink(Action<OutputEntry> labda)
        {
            this.labda = labda;
        }

        public Task Emit(OutputEntry entry)
        {
            this.labda.Invoke(entry);
            return Task.CompletedTask;
        }
    }
}