using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using FileReader;

public class HelloWorld
{
    
    public static void Main(string[] args)
    {
        Console.WriteLine("Введите название файла, который необходимо считать.");
        string path = "";
        double[,] nums = null;
        while (true)
        {
            path = Path.Combine(Environment.CurrentDirectory, Console.ReadLine());
            try
            {
                string readText = File.ReadAllText(path);
                string[] lines = MyArrayMethods.UsefulSplit(readText, ";\n");

                int rowc = lines.Length;
                int colc = lines[0].Split(new char[] { ' ' }).Length;
                if (rowc != colc)
                {
                    throw new FormatException("Wrong format." + rowc.ToString() + " " + colc.ToString());
                }
                nums = new double[rowc, colc];
                for (int i = 0; i < rowc; i++)
                {
                    string[] rarr = lines[i].Split(new char[] { ' ' });
                    for (int j = 0; j < colc; j++)
                    {
                        nums[i, j] = double.Parse(rarr[j]);
                    }
                }
                Console.WriteLine("Данные успешно считаны.");
                break;
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Введено некоректное название файла. Повторите попытку.");
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("У вас нет доступа к этому файлу. Повторите попытку.");
            }
            catch (FormatException e)
            {
                Console.WriteLine("Неверный формат файла. Повторите попытку.");
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("Неверный формат файла. Повторите попытку.");
            }
            catch (Exception e)
            {
                Console.Write("Произошла непредвиденная ошибка.  Повторите попытку.");
                Console.Write(e.ToString());
            }
        }

        MyArrayMethods.PrintArray(nums);
        MyArrayMethods.ChangeArray(nums);
        MyArrayMethods.PrintArray(nums);
    }
}
