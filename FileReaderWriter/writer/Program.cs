using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using FileWriter;

public class MyFileWriter
{    
    public static void Main(string[] args)
    {
        int n = 0;
        Console.WriteLine("Введите целое число 0 < N <= 13.");
        bool input = int.TryParse(Console.ReadLine(), out n);
        while (true)
        {
            if (!input)
            {
                Console.WriteLine("Некорректный ввод.");
                input = int.TryParse(Console.ReadLine(), out n);
            }
            else if (0 < n && n <= 13)
            {
                Console.WriteLine($"Вы ввели число {n}!");
                break;
            }
            else
            {
                Console.WriteLine("Число не входит в указанный диапозон.");
                input = int.TryParse(Console.ReadLine(), out n);
            }
        }

        double[,] arr = MyArrayMethods.FillArr(n);
        var stringBuilder = new StringBuilder();
        for (int i = 0; i < n; i++)
        {
            stringBuilder.Append(arr[i, 0].ToString("0.00"));
            for (int j = 1; j < n; j++)
            {
                stringBuilder.Append(" " + arr[i, j].ToString("0.00"));
            }
            stringBuilder.Append(";\n");
        }

        Console.WriteLine("Введите название файла, в который запишется результат.");
        string path = "";
        while (true)
        {
            path = Path.Combine(Environment.CurrentDirectory, Console.ReadLine());
            try
            {
                File.WriteAllText(path, stringBuilder.ToString());
                Console.WriteLine("Данные успешно записаны.");
                break;
            }
            catch (ArgumentException e)
            {
                Console.Write("Введено некоректное название файла. Повторите попытку.");
            }
            catch (Exception e)
            {
                Console.Write("Произошла непредвиденная ошибка.  Повторите попытку.");
            }
        }
    }
}