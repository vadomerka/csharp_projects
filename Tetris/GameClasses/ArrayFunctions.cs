using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClasses
{
    public static class ArrayFunctions
    {
        public static int[,] Copy(int[,] origArray)
        {
            int h = origArray.GetLength(0);
            int w = origArray.GetLength(1);
            int[,] ints = new int[h, w];
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                    ints[i, j] = origArray[i, j];
            }
            return ints;
        }
    }
}
