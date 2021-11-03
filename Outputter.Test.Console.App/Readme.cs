using System;
using System.Threading.Tasks;
using RJDev.Outputter;
using RJDev.Outputter.Sinks;

class Readme
{
    public static async Task Main()
    {
        var outputter = new Outputter();

        outputter.OutputWriter.WriteLine($"Hello World! {Math.PI:N4}");
        outputter.OutputWriter.WriteLine("Hello World! {0:N4}", Math.PI);
        
        // No more messages expected
        outputter.Complete();

        await outputter.OutputReader
            .Pipe(new SimpleLambdaSink(entry =>
            {
                Console.Write(entry.ToString());
            }));
        
    }
}