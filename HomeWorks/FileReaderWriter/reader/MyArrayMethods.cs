using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader
{
    internal class MyArrayMethods
    {
        public static string[] UsefulSplit(string start, string dels)
        {
            char[] chs = new char[dels.Length];
            for (int i = 0; i < dels.Length; i++)
            {
                chs[i] = dels[i];
            }
            string[] nrarr = start.Split(chs);
            int lres = 0;
            foreach (string s in nrarr)
            {
                if (s != "")
                {
                    lres++;
                }
            }
            string[] res = new string[lres];
            int k = 0;
            for (int i = 0; i < nrarr.Length; i++)
            {
                if (nrarr[i] != "")
                {
                    res[k] = nrarr[i];
                    k++;
                }
            }
            return res;
        }

        public static void PrintArray(double[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                Console.Write(arr[i, 0].ToString("0.##"));
                for (int j = 1; j < arr.GetLength(1); j++)
                {
                    Console.Write("  " + arr[i, j].ToString("0.##"));
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static void ChangeArray(double[,] arr)
        {
            double dsum = 0;
            int n = arr.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                dsum += arr[i, i];
            }
            double med = dsum / ((double)n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i - 1 == j)
                    {
                        arr[i, j] = med;
                    }
                    else if (i == j)
                    {
                        arr[i, j] = 0;
                    }
                }
            }
        }

    }
}
