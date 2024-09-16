using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

/*
 
 Щоб розв’язати задачу, нам потрібно зрозуміти, як обчислити кількість різних салатів, 
які можна приготувати з заданої кількості інгредієнтів.
Салат визначається як комбінація двох або більше інгредієнтів. Якщо ми маємо N інгредієнтів, то:
1.	Загальна кількість комбінацій усіх інгредієнтів (включаючи порожню комбінацію і комбінації з одного інгредієнта) 
можна обчислити за формулою 2^N.
2.	Ми повинні виключити:
o	Порожню комбінацію (1)
o	Усі комбінації з одного інгредієнта (N)
Отже, кількість різних салатів буде:
Кількість салатів = 2^N−1−N

 */


namespace LAB1
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            try
            {
                Console.OutputEncoding = Encoding.UTF8;
                string inputFilePath = "INPUT.TXT";
                string outputFilePath = "OUTPUT.TXT";

                string[] lines = File.ReadAllLines(inputFilePath);

                ValidateInput(lines);

                string result = ProcessLines(lines);
                File.WriteAllText(outputFilePath, result.Trim());

                Console.WriteLine("Файл OUTPUT.TXT успішно створено");
                Console.WriteLine("Лабораторна №1");
                Console.WriteLine("Вхідні дані:");
                Console.WriteLine(string.Join(Environment.NewLine, lines).Trim());
                Console.WriteLine("Вихідні дані:");
                Console.WriteLine(result.Trim());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            Console.WriteLine('\n');
        }

        public static void ValidateInput(string[] lines)
        {
            if (lines.Length > 32)
            {
                throw new InvalidOperationException("Кількість інгредієнтів не повинна перевищувати 32.");
            }

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.Contains(" "))
                {
                    throw new InvalidOperationException("В одному рядку може бути тільки одне число.");
                }

                if (!int.TryParse(line.Trim(), out int N) || N <= 0)
                {
                    throw new InvalidOperationException($"'{line}' не є дійсним числом.");
                }
            }
        }

        public static string ProcessLines(string[] lines)
        {
            StringBuilder result = new StringBuilder();
            foreach (string line in lines)
            {
                if (int.TryParse(line.Trim(), out int N) && N > 0)
                {
                    int numberOfSalads = (1 << N) - 1 - N;
                    result.AppendLine(numberOfSalads.ToString());
                }
            }
            return result.ToString().Replace("\r\n", "\n"); // Заміна \r\n на \n для однорідності
        }

    }
}

