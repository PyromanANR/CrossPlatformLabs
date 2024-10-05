using System;
using System.IO;
using System.Text;
using LAB1;
using LAB2;
using LAB3;

public class LabRunner
{
    public void RunLab1(string inputFile, string outputFile)
    {
        try
        {
            Console.OutputEncoding = Encoding.UTF8;
            string[] lines = File.ReadAllLines(inputFile);

            LAB1.Program.ValidateInput(lines); // Виклик існуючого методу для перевірки
            string result = LAB1.Program.ProcessLines(lines); // Обробка даних

            File.WriteAllText(outputFile, result.Trim()); // Запис результату в файл

            Console.WriteLine("File OUTPUT.TXT successfully created");
            Console.WriteLine("LAB #1");
            Console.WriteLine("Input data:");
            Console.WriteLine(string.Join(Environment.NewLine, lines).Trim());
            Console.WriteLine("Output data:");
            Console.WriteLine(result.Trim());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public void RunLab2(string inputFile, string outputFile)
    {
        try
        {
            Console.OutputEncoding = Encoding.UTF8;
            string[] lines = File.ReadAllLines(inputFile);

            LAB2.Program.ValidateInput(lines); // Виклик існуючого методу для перевірки
            string result = LAB2.Program.ProcessLines(lines); // Обробка даних

            File.WriteAllText(outputFile, result.Trim()); // Запис результату в файл

            Console.WriteLine("File OUTPUT.TXT successfully created");
            Console.WriteLine("LAB #1");
            Console.WriteLine("Input data:");
            Console.WriteLine(string.Join(Environment.NewLine, lines).Trim());
            Console.WriteLine("Output data:");
            Console.WriteLine(result.Trim());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    public void RunLab3(string inputFile, string outputFile)
    {
        try
        {
            Console.OutputEncoding = Encoding.UTF8;
            string[] lines = File.ReadAllLines(inputFile);

            LAB3.Program.ValidateInput(lines); // Виклик існуючого методу для перевірки
            string result = LAB3.Program.ProcessGraph(lines); // Обробка даних

            File.WriteAllText(outputFile, result.Trim()); // Запис результату в файл

            Console.WriteLine("File OUTPUT.TXT successfully created");
            Console.WriteLine("LAB #1");
            Console.WriteLine("Input data:");
            Console.WriteLine(string.Join(Environment.NewLine, lines).Trim());
            Console.WriteLine("Output data:");
            Console.WriteLine(result.Trim());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
