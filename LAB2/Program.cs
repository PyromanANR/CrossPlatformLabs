using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

/*
Задача полягає в тому, щоб знайти кількість способів вибору трьох приладів із заданої кількості 𝑁приладів. 
Вибір здійснюється за допомогою наступного алгоритму:
Якщо кількість приладів 
𝑁 ≤ 3 , то вибір неможливий, оскільки недостатньо приладів для експерименту.
Якщо приладів більше трьох, їх поділяють на дві групи: прилади з парними та непарними номерами. 
Процедуру ділення повторюють до тих пір, поки в одній із груп не залишиться три або менше приладів.
Коли кількість приладів дорівнює трьом, вони беруться для експерименту.
*/


namespace LAB2
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                Console.OutputEncoding = Encoding.UTF8;
                string inputFilePath = args.Length > 0 ? args[0] : Path.Combine("LAB2", "INPUT.TXT");
                string outputFilePath = Path.Combine("LAB2", "OUTPUT.TXT");

                string[] lines = File.ReadAllLines(inputFilePath);

                ValidateInput(lines);

                string result = ProcessLines(lines);
                File.WriteAllText(outputFilePath, result.Trim());

                Console.WriteLine("File OUTPUT.TXT successfully created");
                Console.WriteLine("LAB #2");
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

        public static void ValidateInput(string[] lines)
        {
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.Contains(" "))
                {
                    throw new InvalidOperationException("There can be only one number in one line.");
                }

                if (!long.TryParse(line.Trim(), out long N) || N <= 0 || N > 2147483647)
                {
                    throw new InvalidOperationException($"'{line}' is not a valid number in the allowed range (1 ≤ N ≤ 2147483647).");
                }
            }
        }

        public static string ProcessLines(string[] lines)
        {
            StringBuilder result = new StringBuilder();
            foreach (string line in lines)
            {
                if (long.TryParse(line.Trim(), out long N) && N > 0)
                {
                    long numberOfWays = CountWays(N);
                    result.AppendLine(numberOfWays.ToString());
                }
            }
            return result.ToString().Replace("\r\n", "\n"); // Заміна \r\n на \n для однорідності
        }

        public static long CountWays(long N)
        {
            if (N < 3) return 0;
            if (N == 3) return 1;

            // Мемоізація для запам'ятовування результатів
            Dictionary<long, long> memo = new Dictionary<long, long>();
            return CountWaysRecursive(N, memo);
        }

        private static long CountWaysRecursive(long n, Dictionary<long, long> memo)
        {
            if (n < 3) return 0;
            if (n == 3) return 1;

            if (!memo.ContainsKey(n))
            {
                long half1 = n / 2;
                long half2 = n - half1;
                memo[n] = CountWaysRecursive(half1, memo) + CountWaysRecursive(half2, memo);
            }

            return memo[n];
        }
    }
}
