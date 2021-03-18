using System.Threading.Tasks;

namespace RJDev.Outputter.Sinks
{
    /// <summary>
    /// Interface for sinks
    /// </summary>
    public interface IOutputterSink
    {
        /// <summary>
        /// Write entry into the sink
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        Task Emit(OutputEntry entry);
    }
}