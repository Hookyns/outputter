using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using RJDev.Outputter.Parsing;
using Xunit;

namespace RJDev.Outputter.Tests
{
    public class TokenizerTests
    {
        [Fact]
        public void TokenizeTest()
        {
            Tokenizer tokenizer = new();

            List<IEntryToken> tokens = tokenizer.Tokenize("{0:N2} Hello World! {1}", new object[] { 1.5, true }).ToList();

            Assert.Equal(3, tokens.Count);

            // [0]
            Assert.Equal(TokenType.ArgumentStruct, tokens[0].TokenType);
            Assert.True(tokens[0] is ArgToken);
            Assert.Equal(1.5, (tokens[0] as ArgToken)?.Arg);
            Assert.Equal("N2", (tokens[0] as ArgToken)?.Format);

            // [1]
            Assert.Equal(TokenType.Text, tokens[1].TokenType);
            Assert.True(tokens[1] is TextToken);
            Assert.Equal(" Hello World! ", (tokens[1] as TextToken)?.Text);

            // [2]
            Assert.Equal(TokenType.ArgumentStruct, tokens[2].TokenType);
            Assert.True(tokens[2] is ArgToken);
            Assert.Equal(true, (tokens[2] as ArgToken)?.Arg);
            Assert.True(string.IsNullOrEmpty((tokens[2] as ArgToken)?.Format));

            StringBuilder sb = new();
            StringWriter sw = new(sb);
            tokens.ForEach(token => token.Write(sw));

            Assert.Equal($"{1.5:N2} Hello World! {true}", sb.ToString());
        }

        [Fact]
        public void TokenizeObjectArgTest()
        {
            Tokenizer tokenizer = new();

            List<IEntryToken> tokens = tokenizer.Tokenize(
                "{number:N2} Hello World! {boolean}",
                new object[]
                {
                    new
                    {
                        number = 1.5,
                        boolean = true
                    }
                }
            ).ToList();

            Assert.Equal(3, tokens.Count);

            // [0]
            Assert.Equal(TokenType.ArgumentStruct, tokens[0].TokenType);
            Assert.True(tokens[0] is ArgToken);
            Assert.Equal(1.5, (tokens[0] as ArgToken)?.Arg);
            Assert.Equal("N2", (tokens[0] as ArgToken)?.Format);

            // [1]
            Assert.Equal(TokenType.Text, tokens[1].TokenType);
            Assert.True(tokens[1] is TextToken);
            Assert.Equal(" Hello World! ", (tokens[1] as TextToken)?.Text);

            // [2]
            Assert.Equal(TokenType.ArgumentStruct, tokens[2].TokenType);
            Assert.True(tokens[2] is ArgToken);
            Assert.Equal(true, (tokens[2] as ArgToken)?.Arg);
            Assert.True(string.IsNullOrEmpty((tokens[2] as ArgToken)?.Format));

            StringBuilder sb = new();
            StringWriter sw = new(sb);
            tokens.ForEach(token => token.Write(sw));

            Assert.Equal($"{1.5:N2} Hello World! {true}", sb.ToString());
        }
    }
}