using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using FileWriter;

public class MyFileWriter
{
    public static void Main(string[] args)
    {
        // Считываем число.
        int n = 0;
        Console.WriteLine("Введите целое число 0 < N <= 13.");
        bool input = int.TryParse(Console.ReadLine(), out n);
        while (true)
        {
            if (!input)  // Если строка это не целое число, запрашиваем ввод повторно.
            {
                Console.WriteLine("Некорректный ввод. Повторите попытку.");
                input = int.TryParse(Console.ReadLine(), out n);
            }
            else if (0 < n && n <= 13)  // Если строка это целое число, и оно входит в нужный диапозон.
            {
                Console.WriteLine($"Вы ввели число {n}!");  // Продолжаем работу алгоритма.
                break;
            }
            else  // Если число не входит в диапозон, запрашиваем ввод повторно.
            {
                Console.WriteLine("Число не входит в указанный диапозон.");
                input = int.TryParse(Console.ReadLine(), out n);
            }
        }
        // Создаем структуру данных.
        double[,] arr = MyArrayMethods.FillArr(n);  // Заполняем структуру данных с помощью метода.
        var stringBuilder = new StringBuilder();  // Создаем экземпляр StringBuilder, для преобразования структуры данных в строчку.
        for (int i = 0; i < n; i++)  // Добавляем значения структуры данных в строку.
        {
            stringBuilder.Append(arr[i, 0].ToString("0.00"));  // Делаем это с первым элементом каждой строки отдельно, чтобы соответствовало формату.
            for (int j = 1; j < n; j++)
            {
                stringBuilder.Append(" " + arr[i, j].ToString("0.00"));  // Добавляем каждый следующий элемент с одним пробелом впереди.
            }
            stringBuilder.Append(";\n");  // Добавляем разделение по строчкам и ";".
        }

        Console.WriteLine("Введите название файла, в который запишется результат.");
        string path = "";
        while (true)
        {
            path = Path.Combine(Environment.CurrentDirectory, Console.ReadLine());  // Считываем название файла для записи.
            try  // Проверяем получилось ли создать файл.
            {
                File.WriteAllText(path, stringBuilder.ToString());
                Console.WriteLine("Данные успешно записаны.");
                break;
            }
            catch (ArgumentException)  // Если введено некорректное название, сообщаем об этом пользователю.
            {
                Console.Write("Введено некоректное название файла. Повторите попытку.");
            }
            catch (Exception)  // Отлавливаем все остальные ошибки.
            {
                Console.Write("Произошла непредвиденная ошибка.  Повторите попытку.");
            }
        }
    }
}