namespace RJDev.Outputter.Formatting
{
    public interface IFormatter
    {
        /// <summary>
        /// Format entry.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        string Format(OutputEntry entry);
    }
}