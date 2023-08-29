using System;
using System.Runtime.CompilerServices;
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
        /// <param name="entryType"></param>
        /// <param name="args"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Write(StringCast message, EntryType entryType = EntryType.General, params object?[] args)
		{
            if (outputter.Completed)
            {
                throw new InvalidOperationException("Writing into completed writer.");
            }

            outputter.Bufferblock.Post(new OutputEntry(message.String, entryType, args));
        }

        /// <summary>
        /// Write message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Write(StringCast message, params object?[] args)
		{
            Write(message, EntryType.General, args);
        }

        /// <summary>
        /// Write message with new line
        /// </summary>
        /// <param name="message"></param>
        /// <param name="entryType"></param>
        /// <param name="args"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void WriteLine(StringCast message, EntryType entryType = EntryType.General, params object?[] args)
		{
            Write(message.String + Environment.NewLine, entryType, args);
		}
		
        /// <summary>
        /// Write message with new line
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        /// <exception cref="InvalidOperationException"></exception>
		public void WriteLine(StringCast message, params object?[] args)
		{
            WriteLine(message, EntryType.General, args);
		}
        
        /// <summary>
        /// Write message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="entryType"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Write(FormattableString message, EntryType entryType = EntryType.General)
        {
            if (outputter.Completed)
            {
                throw new InvalidOperationException("Writing into completed writer.");
            }
            
            outputter.Bufferblock.Post(new OutputEntry(message, entryType));
        }

        /// <summary>
        /// Write message with new line
        /// </summary>
        /// <param name="message"></param>
        /// <param name="entryType"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void WriteLine(FormattableString message, EntryType entryType = EntryType.General)
        {
            Write(FormattableStringFactory.Create(message.Format + Environment.NewLine, message.GetArguments()), entryType);
        }
    }
}