using System;
using ClassLibrary;

namespace MainProgram
{
    class Program
    { 
        public static void Main(string[] args) 
        {
            do
            {
                Console.WriteLine("Введите две точки, определяющие окружность в формате \"(x1;y1) (x2;y2)\".");
                string input = Console.ReadLine();
                Circle circleA;
                while (true)
                {     
                    try
                    {
                        circleA = new Circle(input);
                        Console.WriteLine("Окружность записана.");
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Неверный формат строки. Повторите попытку.");
                        input = Console.ReadLine();
                    }
                }
                Console.WriteLine(circleA);
                while (true)
                {
                    string path = "input.txt";
                    string[] data = File.ReadAllLines(path);
                    if (data.Length < 3)
                    {
                        Console.WriteLine("Файл \"input.txt\" слишком короткий.");
                        Console.WriteLine("Измените файл и повторите попытку.");
                        continue;
                    }

                    for (int i = 0; i < data.Length; i++)
                    {
                        Circle fileCircle = new Circle(data[i]);
                        if (circleA > fileCircle) Console.WriteLine($"Окружность {fileCircle} входит в А");
                        else if (circleA < fileCircle) Console.WriteLine($"Окружность А входит в {fileCircle}");
                        else if (circleA == fileCircle) Console.WriteLine($"Окружность А равна {fileCircle}");
                        else Console.WriteLine("Окружности не связаны");
                    }
                    break;
                }

                Console.WriteLine("Введите \"Enter\" чтобы продолжить.");
            } 
            while (Console.ReadKey().Key == ConsoleKey.Enter);
        }
    }
}