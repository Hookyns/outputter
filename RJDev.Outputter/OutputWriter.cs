using System;
using System.Threading.Tasks.Dataflow;

namespace RJDev.Outputter
{
	public class OutputWriter
	{
        /// <summary>
        /// Instance of Outputter
        /// </summary>
        private readonly Outputter outputter;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="outputter"></param>
        public OutputWriter(Outputter outputter)
        {
            this.outputter = outputter;
        }

        /// <summary>
        /// Write message
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="InvalidOperationException"></exception>
		public void Write(string message)
		{
            if (this.outputter.Completed)
            {
                throw new InvalidOperationException("Writing into completed writer.");
            }

            this.outputter.Bufferblock.Post(new OutputEntry(message, EntryType.General));
        }

        /// <summary>
        /// Write message
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="InvalidOperationException"></exception>
		public void Write(FormattableString message)
		{
            if (this.outputter.Completed)
            {
                throw new InvalidOperationException("Writing into completed writer.");
            }
            
            this.outputter.Bufferblock.Post(new OutputEntry(message, EntryType.General));
		}

        /// <summary>
        /// Write message with new line
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="InvalidOperationException"></exception>
		public void WriteLine(FormattableString message)
		{
            if (this.outputter.Completed)
            {
                throw new InvalidOperationException("Writing into completed writer.");
            }
            
            this.outputter.Bufferblock.Post(new OutputEntry(message + Environment.NewLine, EntryType.General));
		}
		
        /// <summary>
        /// Write message with new line
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="InvalidOperationException"></exception>
		public void WriteLine(string message)
		{
            if (this.outputter.Completed)
            {
                throw new InvalidOperationException("Writing into completed writer.");
            }
            
            this.outputter.Bufferblock.Post(new OutputEntry(message + Environment.NewLine, EntryType.General));
		}
    }
}