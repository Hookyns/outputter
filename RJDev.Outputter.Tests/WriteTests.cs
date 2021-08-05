using System;
using System.Threading.Tasks;
using Xunit;

namespace RJDev.Outputter.Tests
{
    public class WriteTests
    {
        // Writing with Complete().
        [Fact]
        public void WriteTest()
        {
            var outputter = new Outputter();
            outputter.OutputWriter.WriteLine($"Hello World!");
            outputter.OutputWriter.WriteLine($"Hello World! {2}");
            outputter.OutputWriter.Write("Hello World!");
            outputter.OutputWriter.Write($" IsThird: {true}");
            outputter.Complete();
        }

        // Writing after Complete() throws Exception.
        [Fact]
        public void WriteAfterCompletedFailsTest()
        {
            var outputter = new Outputter();
            outputter.OutputWriter.WriteLine($"Hello World! {1}");
            outputter.Complete();

            Assert.Throws<InvalidOperationException>(() => { outputter.OutputWriter.WriteLine($"Hello World! {2}"); });
        }
        
        // A lot of writes
        [Fact]
        public void ManyWritesTest()
        {
            var outputter = new Outputter();
            
            for (int i = 0; i < 1000000; i++)
            {
                outputter.OutputWriter.Write("Hello number {0:N4}!", Math.PI);
                outputter.OutputWriter.Write($"{1.5:N2} Hello World! {true:|booleanName}");
            }

            outputter.Complete();
        }
    }
}