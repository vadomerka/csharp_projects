// Пронюшкин Радомир БПИ234-1 КДЗ-2-1
using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using ProcessingClasses;

namespace Pronyushkin_CWH
{
    public class MyFileReader
    {
        /// <summary>
        /// Главное меню программы. Реализовано через цикл, с повтором решения. Все методы подключены через MainUI.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Вы запустили программу для работы с csv файлами.");
            string[] fileData = { };
            CsvProcessing cv = null;
            try { cv = MainUI.GetFileData(ref fileData); } 
            catch { Console.WriteLine("Произошла непредвиденная ошибка"); }
            
            bool program = cv != null ? true : false;
            while (program)
            {
                Console.Clear();
                Console.WriteLine("Выберите опцию из предложенного списка:");
                Console.WriteLine("1) Произвести выборку значений по LandscapingZone.");
                Console.WriteLine("2) Произвести выборку значений по LocationPlace.");
                Console.WriteLine("3) Произвести выборку значений по LandscapingZone И ProsperityPeriod.");
                Console.WriteLine("4) Отсортировать таблицу по LatinName по алфавиту в прямом порядке.");
                Console.WriteLine("5) Отсортировать таблицу по LatinName по алфавиту в обратном порядке.");
                Console.WriteLine("6) Открыть новый файл.");
                Console.WriteLine("7) Выйти из программы.");

                // Выбор методов из меню через нажатие цифр на клавиатуре.
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        try { MainUI.UserFilter(fileData, cv, "LandscapingZone"); }
                        catch { Console.WriteLine("Произошла непредвиденная ошибка"); }
                        break;
                    case ConsoleKey.D2:
                        try { MainUI.UserFilter(fileData, cv, "LocationPlace"); }
                        catch { Console.WriteLine("Произошла непредвиденная ошибка"); }
                        break;
                    case ConsoleKey.D3:
                        try { MainUI.UserFilter(fileData, cv, new string[] { "LandscapingZone", "LocationPlace" }); }
                        catch { Console.WriteLine("Произошла непредвиденная ошибка"); }
                        break;
                    case ConsoleKey.D4:
                        try { MainUI.UserSort(fileData, cv, "LatinName", false); }
                        catch { Console.WriteLine("Произошла непредвиденная ошибка"); }
                        break;
                    case ConsoleKey.D5:
                        try { MainUI.UserSort(fileData, cv, "LatinName", true); }
                        catch { Console.WriteLine("Произошла непредвиденная ошибка"); }
                        break;
                    case ConsoleKey.D6:
                        // Функция открытия нового файла.
                        Console.Clear();
                        fileData = new string[] { };
                        cv = null;
                        try { cv = MainUI.GetFileData(ref fileData); }
                        catch { Console.WriteLine("Произошла непредвиденная ошибка"); }
                        program = cv != null ? true : false;
                        break;
                    case ConsoleKey.D7:
                        Console.WriteLine();
                        Console.Write("Выбран выход из программы.");
                        program = false;
                        break;
                }
            }
        }
    }

}