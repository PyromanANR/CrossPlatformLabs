using System;
using System.Diagnostics;

public class Start
{

    public void Start_()
    {
        Console.WriteLine("Оберіть яку лабораторну хочете зробити:");
        Console.WriteLine("1 - Перша лабораторна");
        Console.WriteLine("0 - Закрити вікно");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                // Запуск першої лабораторної
                RunLab1();
                break;

            case "0":
                Console.WriteLine("Програма завершена.");
                Environment.Exit(0);
                break;

            default:
                Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                Start_(); 
                break;
        }
    }

    private void RunLab1()
    {
        Console.WriteLine("Запуск першої лабораторної...");
        string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string projectDirectory = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\..\"));

        // Скласти шлях до папки LAB1
        string lab1Directory = Path.Combine(projectDirectory, "LAB1");

        var process = new System.Diagnostics.Process();
        process.StartInfo.WorkingDirectory = lab1Directory; 
        process.StartInfo.FileName = "dotnet";
        process.StartInfo.Arguments = "run";
        process.Start();
        process.WaitForExit(); 

        RunTests(projectDirectory); // Викликаємо метод для запуску тестів
    }

    private void RunTests(string projectDirectory)
    {
        Console.WriteLine("Запуск тестів...");     
        string testsDirectory = Path.Combine(projectDirectory, "LAB1.Tests");

        var process = new Process();
        process.StartInfo.WorkingDirectory = testsDirectory; 
        process.StartInfo.FileName = "dotnet";
        process.StartInfo.Arguments = "test"; 
        process.Start();
        process.WaitForExit(); 
    }


    static void Main(string[] args)
    {
        Start start = new Start(); 
        start.Start_(); 
    }
}



