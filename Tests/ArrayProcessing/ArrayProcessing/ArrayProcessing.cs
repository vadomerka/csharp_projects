using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class ArrayProcessing
    {
        public static int[] CreateArray(int N)
        {
            int[] res = new int[N];
            var rng = new Random();
            for (int i = 0; i < N; i++)
            {
                res[i] = rng.Next(10, 65 + 1);
            }
            return res;
        }

        public static string AsString(int[] arr)
        {
            var stringbuilder = new StringBuilder();
            stringbuilder.Append(arr[0]);
            for (int i = 1; i < arr.Length; i++) 
            {
                stringbuilder.Append("; ");
                stringbuilder.Append(arr[i]);
            }
            return stringbuilder.ToString();
        }
    }
}
