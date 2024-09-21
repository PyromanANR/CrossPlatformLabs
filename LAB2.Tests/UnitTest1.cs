using System;
using Xunit;

namespace LAB2.Tests
{
    public class UnitTest1
    {

        [Fact]
        public void CountWays_NEquals3_Returns1()
        {
            long n = 3;

            long result = Program.CountWays(n);

            Assert.Equal(1, result);
        }

        [Fact]
        public void CountWays_NEquals6_Returns2()
        {
            long n = 6;

            long result = Program.CountWays(n);

            Assert.Equal(2, result);
        }

        [Fact]
        public void ValidateInput_InvalidData_ThrowsException()
        {
            
            string[] invalidLines = { "2147483648" };
         
            Assert.Throws<InvalidOperationException>(() => Program.ValidateInput(invalidLines));
        }

        [Fact]
        public void Test_InvalidNumberFormat()
        {
            string[] lines = { "xyz" };

            var ex = Record.Exception(() => Program.ValidateInput(lines));
            Assert.NotNull(ex);
            Assert.IsType<InvalidOperationException>(ex);
            Assert.Equal("'xyz' is not a valid number in the allowed range (1 ≤ N ≤ 2147483647).", ex.Message);
        }

        [Fact]
        public void Test_ValidProcessing()
        {
            string[] lines = { "6", "7", "8" };
            string expected = "2\n1\n0\n";

            string result = Program.ProcessLines(lines);

            Assert.Equal(expected, result);
        }
    }
}
