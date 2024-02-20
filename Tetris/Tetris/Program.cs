using GameClasses;
using System.IO;

namespace Tetris
{
    public static class MainProgram
    {
        public static void Main(string[] args)
        {
            long n1 = C(4, 2) + C(4, 1);
            long n2 = C(4, 3) + C(4, 1) + A(4, 2);
            long n3 = C(4, 1) + A(4, 2) + A(4, 2) +
                C(4, 2) + A(4, 3) + C(4, 3) +
                C(4, 2) + C(4, 1) + A(4, 3) / 2;

            Console.WriteLine(n1);
            Console.WriteLine(n2);
            Console.WriteLine(n3);


            long A(long n, long k)
            {
                return F(n) / F(n - k);
            }

            long C(long n, long k)
            {
                return F(n) / (F(k) * F(n - k));
            }

            // Factorial
            long F(long n)
            {
                if (n <= 1) return 1;
                return n * F(n - 1);
            }
        }
    }
}