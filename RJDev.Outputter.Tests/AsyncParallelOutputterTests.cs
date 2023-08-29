using System;
using System.Threading.Tasks;
using RJDev.Outputter.Sinks;
using Xunit;
using Xunit.Abstractions;

namespace RJDev.Outputter.Tests
{
    public class AsyncParallelOutputterTests
    {
        private readonly ITestOutputHelper outputHelper;

        // Test of writes
        public AsyncParallelOutputterTests(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
        }

        // Write few messages and then Read.
        [Fact]
        public async Task WriteReadTest()
        {
            var outputter = GetOutputter();
            outputter.OutputWriter.WriteLine($"Hello World!");
            outputter.OutputWriter.WriteLine($"Hello World! {2}");
            outputter.OutputWriter.Write("Hello World!");
            outputter.OutputWriter.Write($" IsThird: {true}");
            outputter.Complete();

            await foreach (var entry in outputter.OutputReader.Read())
            {
                outputHelper.WriteLine(entry.ToString());
            }
        }

        // Two Reads fails.
        [Fact]
        public async Task SecondReadFailsTest()
        {
            var outputter = GetOutputter();
            outputter.OutputWriter.WriteLine($"Hello World!");
            outputter.OutputWriter.WriteLine($"Hello World! {2}");
            outputter.OutputWriter.Write("Hello World!");
            outputter.OutputWriter.Write($" IsThird: {true}");
            outputter.Complete();

            await foreach (var entry in outputter.OutputReader.Read())
            {
                outputHelper.WriteLine(entry.ToString());
            }

            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await foreach (var entry in outputter.OutputReader.Read())
                {
                    outputHelper.WriteLine(entry.ToString());
                }
            });
        }

        // Write and Read in parallel from two Tasks.
        [Fact]
        public async Task WriteReadParalelTest()
        {
            var outputter = GetOutputter();

            Task writeTask = Task.Run(async () =>
            {
                OutputWriter output = outputter.OutputWriter;

                output.WriteLine($"Hello World!");
                await Task.Delay(300);
                output.WriteLine($"Hello World! {2}");
                await Task.Delay(300);
                output.Write("Hello World!");
                await Task.Delay(300);
                output.Write($" IsThird: {true}");
                outputter.Complete();
            });

            Task readTask = Task.Run(async () =>
            {
                await foreach (var entry in outputter.OutputReader.Read())
                {
                    outputHelper.WriteLine(entry.ToString());
                }
            });

            await Task.WhenAll(writeTask, readTask);
        }

        // Start Read first, delay between iterations; Write in parallel, ending before Read ends.
        [Fact]
        public async Task ReadFirstWithDelayTest()
        {
            var outputter = GetOutputter();

            Task readTask = Task.Run(async () =>
            {
                await foreach (var entry in outputter.OutputReader.Read())
                {
                    outputHelper.WriteLine(entry.ToString());
                    await Task.Delay(500);
                }
            });

            Task writeTask = Task.Run(async () =>
            {
                OutputWriter output = outputter.OutputWriter;

                output.WriteLine("Hello World!");
                await Task.Delay(300);
                output.WriteLine($"Hello World! {2}");
                await Task.Delay(300);
                output.Write("Hello World!");
                await Task.Delay(300);
                output.Write($" IsThird: {true}");
                outputter.Complete();
            });

            await Task.WhenAll(writeTask, readTask);
        }

        // Reading by piped Sink
        [Fact]
        public async Task ReadingByPipedSinkTest()
        {
            var outputter = GetOutputter();

            Task.Run(async () =>
            {
                outputter.OutputWriter.WriteLine("Hello World!");
                await Task.Delay(300);
                outputter.OutputWriter.WriteLine($"Hello World! {2}");
                await Task.Delay(300);
                outputter.OutputWriter.Write("Hello World!");
                await Task.Delay(300);
                outputter.OutputWriter.Write($" IsThird: {true}");
                
                // Wait after last write
                await Task.Delay(300);
                outputter.Complete();
            });

            int reads = 0;

            await outputter.OutputReader.Pipe(
                new SimpleLambdaSink(entry =>
                {
                    outputHelper.WriteLine(entry.ToString());
                    reads++;
                })
            );

            // Writing should be Completed cuz Pipe waits for complete
            Assert.True(outputter.Completed);
            
            // 4 messages read
            Assert.Equal(4, reads);
        }

        private Outputter GetOutputter()
        {
            return new Outputter();
        }
    }
}