using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWriter
{
    internal class MyArrayMethods
    {
        public static double[,] FillArr(int n)
        {
            double[,] array = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    int id = (n * i + j + 1);
                    array[i, j] = (double)id + Math.Cbrt(3 - id * id * id);
                }
            }
            return array;
        }

    }
}
