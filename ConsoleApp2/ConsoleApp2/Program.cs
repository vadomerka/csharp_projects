// Пронюшкин Радомир БПИ234-1
using ConsoleApp2;
using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
// using ProcessingClasses;

namespace Pronyushkin_CWH
{
    public class MyFileReader
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Вы запустили программу.");

            bool program = true;
            string input = "start";
            while (input != "exit")
            {
                Console.Clear();
                Circle circle = new Circle();
                while (true) {
                    try {
                        Console.WriteLine("Введите значение диаметра: ");
                        double D = double.Parse(Console.ReadLine());
                        circle = new Circle(D);
                        break;
                    }
                    catch {
                        Console.WriteLine("Данные неверны.");
                    }
                }
                Console.WriteLine($"Создан объект \"Окружность\": {circle}");

                Console.WriteLine("Для выхода из программы введите 'exit'.");
                input = Console.ReadLine();
            }
        }
    }

}