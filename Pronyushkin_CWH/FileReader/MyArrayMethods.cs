using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader
{
    internal class MyArrayMethods
    {
        public static void PrintArray(double[,] arr)  // Функция для правильного вывода структуры данных.
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                Console.Write(arr[i, 0].ToString("0.##"));  // Сначала выводим первый элемент в строке.
                for (int j = 1; j < arr.GetLength(1); j++)
                {
                    Console.Write("  " + arr[i, j].ToString("0.##"));  // Затем выводим все остальные с отступом в два пробела.
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static void ChangeArray(double[,] arr)  // Функция для изменения значений структуры данных.
        {
            double dsum = 0;
            int n = arr.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                dsum += arr[i, i];
            }
            double med = dsum / ((double)n);  // Вычисляем среднее арифметическое элементов главной диагонали.
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i - 1 == j)
                    {
                        arr[i, j] = med;  // Заменяем элементы под главной диагональю на среднее арифметическое.
                    }
                    else if (i == j)
                    {
                        arr[i, j] = 0;  // Заменяем элементы главной диагонали на нули.
                    }
                }
            }
        }

    }
}
