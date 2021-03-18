using System;
using System.Threading.Tasks;
using RJDev.Outputter.Sinks;
using Xunit;
using Xunit.Abstractions;

namespace RJDev.Outputter.Tests
{
    public class OutputterTests
    {
        private readonly ITestOutputHelper outputHelper;

        // Test of writes
        public OutputterTests(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
        }

        // Writing with Complete().
        [Fact]
        public async Task WriteTest()
        {
            var outputter = this.GetOutputter();
            outputter.OutputWriter.WriteLine($"Hello World!");
            outputter.OutputWriter.WriteLine($"Hello World! {2}");
            outputter.OutputWriter.Write("Hello World!");
            outputter.OutputWriter.Write($" IsThird: {true}");
            outputter.Complete();
        }

        // Writing after Complete() throws Exception.
        [Fact]
        public async Task WriteAfterCompletedFailsTest()
        {
            var outputter = this.GetOutputter();
            outputter.OutputWriter.WriteLine($"Hello World! {1}");
            outputter.Complete();

            Assert.Throws<InvalidOperationException>(() => { outputter.OutputWriter.WriteLine($"Hello World! {2}"); });
        }

        // Write few messages and then Read.
        [Fact]
        public async Task WriteReadTest()
        {
            var outputter = this.GetOutputter();
            outputter.OutputWriter.WriteLine($"Hello World!");
            outputter.OutputWriter.WriteLine($"Hello World! {2}");
            outputter.OutputWriter.Write("Hello World!");
            outputter.OutputWriter.Write($" IsThird: {true}");
            outputter.Complete();

            await foreach (var entry in outputter.OutputReader.Read())
            {
                this.outputHelper.WriteLine(entry.ToString());
            }
        }

        // Two Reads fails.
        [Fact]
        public async Task SecondReadFailsTest()
        {
            var outputter = this.GetOutputter();
            outputter.OutputWriter.WriteLine($"Hello World!");
            outputter.OutputWriter.WriteLine($"Hello World! {2}");
            outputter.OutputWriter.Write("Hello World!");
            outputter.OutputWriter.Write($" IsThird: {true}");
            outputter.Complete();

            await foreach (var entry in outputter.OutputReader.Read())
            {
                this.outputHelper.WriteLine(entry.ToString());
            }

            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await foreach (var entry in outputter.OutputReader.Read())
                {
                    this.outputHelper.WriteLine(entry.ToString());
                }
            });
        }

        // Write and Read in parallel from two Tasks.
        [Fact]
        public async Task WriteReadParalelTest()
        {
            var outputter = this.GetOutputter();

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
                    this.outputHelper.WriteLine(entry.ToString());
                }
            });

            await Task.WhenAll(writeTask, readTask);
        }

        // Start Read first, delay between iterations; Write in parallel, ending before Read ends.
        [Fact]
        public async Task ReadFirstWithDelayTest()
        {
            var outputter = this.GetOutputter();

            Task readTask = Task.Run(async () =>
            {
                await foreach (var entry in outputter.OutputReader.Read())
                {
                    this.outputHelper.WriteLine(entry.ToString());
                    await Task.Delay(500);
                }
            });

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

            await Task.WhenAll(writeTask, readTask);
        }

        // Reading by piped Sink
        [Fact]
        public async Task ReadingByPipedSinkTest()
        {
            var outputter = this.GetOutputter();

            Task.Run(async () =>
            {
                outputter.OutputWriter.WriteLine($"Hello World!");
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
                    this.outputHelper.WriteLine(entry.ToString());
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