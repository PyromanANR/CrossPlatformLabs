using System;
using System.IO;
using Xunit;

namespace LAB1.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test_ValidInput()
        {
            string[] lines = { "3", "4", "5" };

            var ex = Record.Exception(() => Program.ValidateInput(lines));
            Assert.Null(ex);
        }

        [Fact]
        public void Test_InputExceedsLimit()
        {
            string[] lines = new string[33]; // Більше 32 рядків

            var ex = Record.Exception(() => Program.ValidateInput(lines));
            Assert.NotNull(ex);
            Assert.IsType<InvalidOperationException>(ex);
            Assert.Equal("Кількість інгредієнтів не повинна перевищувати 32.", ex.Message);
        }

        [Fact]
        public void Test_InvalidNumberFormat()
        {
            string[] lines = { "abc" };

            var ex = Record.Exception(() => Program.ValidateInput(lines));
            Assert.NotNull(ex);
            Assert.IsType<InvalidOperationException>(ex);
            Assert.Equal("'abc' не є дійсним числом.", ex.Message);
        }

        [Fact]
        public void Test_ValidProcessing()
        {
            string[] lines = { "3", "4", "5" };
            string expected = "4\n11\n26\n";

            string result = Program.ProcessLines(lines);

            Assert.Equal(expected, result);
        }
    }
}
