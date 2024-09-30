using System;
using System.IO;
using Xunit;
using LAB3; 

namespace LAB3.Tests
{
    public class LAB3Tests
    {
        // Helper method to create input file and run the Program
        private string RunTest(string[] inputLines)
        {
            string inputFilePath = "INPUT.TXT";
            string outputFilePath = "OUTPUT.TXT";

            // Create input file
            File.WriteAllLines(inputFilePath, inputLines);


            string[] lines = File.ReadAllLines(inputFilePath);
            string result = Program.ProcessGraph(lines);
            File.WriteAllText(outputFilePath, result.Trim());

            // Return the result written to output file
            return File.ReadAllText(outputFilePath).Trim();
        }

        [Fact]
        public void TestShortestPathExists()
        {
            // Arrange: A 3-vertex graph with a clear path from vertex 1 to vertex 3
            string[] inputLines = new string[]
            {
                "3",
                "0 1 0",
                "1 0 1",
                "0 1 0",
                "1 3"
            };

            // Act: Run the test
            string result = RunTest(inputLines);

            // Assert: The shortest path from 1 to 3 should be 2
            Assert.Equal("2", result);
        }

        [Fact]
        public void TestNoPathExists()
        {
            // Arrange: A 3-vertex graph where no path exists from vertex 1 to 3
            string[] inputLines = new string[]
            {
                "3",
                "0 1 0",
                "1 0 0",
                "0 0 0",
                "1 3"
            };

            // Act
            string result = RunTest(inputLines);

            // Assert: No path, so the result should be -1
            Assert.Equal("-1", result);
        }

        [Fact]
        public void TestSingleVertex()
        {
            // Arrange: A graph with only one vertex
            string[] inputLines = new string[]
            {
                "1",
                "0",
                "1 1"
            };

            // Act
            string result = RunTest(inputLines);

            // Assert: The start and end are the same, so the shortest path is 0
            Assert.Equal("0", result);
        }

        [Fact]
        public void TestFullyConnectedGraph()
        {
            // Arrange: A fully connected 3-vertex graph
            string[] inputLines = new string[]
            {
                "3",
                "0 1 1",
                "1 0 1",
                "1 1 0",
                "1 3"
            };

            // Act
            string result = RunTest(inputLines);

            // Assert: The shortest path from 1 to 3 should be 1
            Assert.Equal("1", result);
        }

        [Fact]
        public void TestUnconnectedGraph()
        {
            // Arrange: A 3-vertex graph with no edges
            string[] inputLines = new string[]
            {
                "3",
                "0 0 0",
                "0 0 0",
                "0 0 0",
                "1 3"
            };

            // Act
            string result = RunTest(inputLines);

            // Assert: No path, so the result should be -1
            Assert.Equal("-1", result);
        }
    }
}
