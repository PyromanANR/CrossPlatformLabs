using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LAB3
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                Console.OutputEncoding = Encoding.UTF8;
                string inputFilePath = args.Length > 0 ? args[0] : "INPUT.TXT";
                string outputFilePath = Path.Combine("LAB3", "OUTPUT.TXT");

                string[] lines = File.ReadAllLines(inputFilePath);

                ValidateInput(lines);

                string result = ProcessGraph(lines);
                File.WriteAllText(outputFilePath, result.Trim());

                Console.WriteLine("File OUTPUT.TXT successfully created");
                Console.WriteLine("LAB #3 - BFS Shortest Path");
                Console.WriteLine("Input data:");
                Console.WriteLine(string.Join(Environment.NewLine, lines).Trim());
                Console.WriteLine("Output data:");
                Console.WriteLine(result.Trim());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine('\n');
        }

        // Validate input file format and contents
        public static void ValidateInput(string[] lines)
        {
            int n = int.Parse(lines[0]);
            if (n < 1 || n > 100)
            {
                throw new InvalidOperationException("The number of vertices should be between 1 and 100.");
            }

            for (int i = 1; i <= n; i++)
            {
                string[] row = lines[i].Split();
                if (row.Length != n)
                {
                    throw new InvalidOperationException("Adjacency matrix must have N rows and N columns.");
                }

                foreach (string cell in row)
                {
                    if (cell != "0" && cell != "1")
                    {
                        throw new InvalidOperationException("Matrix values must be 0 or 1.");
                    }
                }
            }

            string[] vertices = lines[n + 1].Split();
            if (vertices.Length != 2 || !int.TryParse(vertices[0], out _) || !int.TryParse(vertices[1], out _))
            {
                throw new InvalidOperationException("Invalid format for start and finish vertices.");
            }
        }

        // Process the graph and calculate the shortest path using BFS
        public static string ProcessGraph(string[] lines)
        {
            int n = int.Parse(lines[0]); // Number of vertices
            int[,] adjacencyMatrix = new int[n, n];

            // Fill adjacency matrix
            for (int i = 0; i < n; i++)
            {
                string[] row = lines[i + 1].Split();
                for (int j = 0; j < n; j++)
                {
                    adjacencyMatrix[i, j] = int.Parse(row[j]);
                }
            }

            // Read start and finish vertices
            string[] vertices = lines[n + 1].Split();
            int start = int.Parse(vertices[0]) - 1; // Convert to 0-indexed
            int finish = int.Parse(vertices[1]) - 1; // Convert to 0-indexed

            // BFS to find the shortest path
            int[] distance = new int[n];
            for (int i = 0; i < n; i++) distance[i] = 1000000; // Initialize distance with a large number

            distance[start] = 0;
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                int current = queue.Dequeue();
                for (int j = 0; j < n; j++)
                {
                    if (adjacencyMatrix[current, j] == 1 && distance[j] > distance[current] + 1)
                    {
                        distance[j] = distance[current] + 1;
                        queue.Enqueue(j);
                    }
                }
            }

            // Return the result: either the shortest path length or -1 if no path
            return distance[finish] < 1000000 ? distance[finish].ToString() : "-1";
        }
    }
}
