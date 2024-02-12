using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using DataProcessing;
using System.Text.Json;
using System.Numerics;

namespace MainProgram
{
    /// <summary>
    /// Класс MainUI обеспечивает связь межжду пользователем и библиотекой классов.
    /// </summary>
    public static class MainUI
    {
        public static void GetData(out List<Patient>? patients, out Dictionary<int, Doctor>? doctors)
        {
            Console.WriteLine("Введите путь к файлу для чтения и записи данных.");
            do
            {
                try
                {
                    string fPath = Console.ReadLine() ?? "";
                    string jsonTextData = File.ReadAllText(fPath);
                    patients = JsonSerializer.Deserialize<List<Patient>>(jsonTextData);
                    if (patients == null) throw new ArgumentException();

                    doctors = new Dictionary<int, Doctor>();
                    PatientsProcessing.DoctorSeparator(patients, doctors);
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Произошла ошибка при считывании объектов.");
                    Console.WriteLine("Повторите попытку.");
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine("Файл не был найден.");
                    Console.WriteLine("Повторите попытку.");
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine("Файл невозможно считать.");
                    Console.WriteLine("Повторите попытку.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Произошла непредвиденная ошибка при считывании данных.");
                    Console.WriteLine("Повторите попытку.");
                }
            } while (true);
        }
        /*
        /// <summary>
        /// Метод FilterData фильтрует данные по полю и значению от пользователя.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<Patient> FilterData(List<Patient> data)
        {
            // Если пользователь еще не ввел данные, сообщаем об этом.
            if (data is null)
            {
                Console.WriteLine("Данные не обнаружены.");
                return null;
            }
            bool choiceLoop = true;
            // Пользователь должен выбрать одну из 7ми функций для фильтрации.
            // Результат сохраняем в делегат.
            DataFilterDelegate filterRule = DataFilter.IdFilterRule;
            while (choiceLoop)
            {
                choiceLoop = false;
                Console.WriteLine("Выберете вариант из списка:");
                Console.WriteLine("1) Отфильтровать по playerId.");
                Console.WriteLine("2) Отфильтровать по userName.");
                Console.WriteLine("3) Отфильтровать по level.");
                Console.WriteLine("4) Отфильтровать по gameScore.");
                Console.WriteLine("5) Отфильтровать по achievements.");
                Console.WriteLine("6) Отфильтровать по inventory.");
                Console.WriteLine("7) Отфильтровать по guild.");
                ConsoleKey inpKey = Console.ReadKey().Key;
                Console.WriteLine();
                switch (inpKey)
                {
                    case ConsoleKey.D1:
                        filterRule = DataFilter.IdFilterRule;
                        break;
                    case ConsoleKey.D2:
                        filterRule = DataFilter.UserNameFilterRule;
                        break;
                    case ConsoleKey.D3:
                        filterRule = DataFilter.LevelFilterRule;
                        break;
                    case ConsoleKey.D4:
                        filterRule = DataFilter.ScoreFilterRule;
                        break;
                    case ConsoleKey.D5:
                        filterRule = DataFilter.AchievementsFilterRule;
                        break;
                    case ConsoleKey.D6:
                        filterRule = DataFilter.InventoryFilterRule;
                        break;
                    case ConsoleKey.D7:
                        filterRule = DataFilter.GuildFilterRule;
                        break;
                    default:
                        choiceLoop = true;
                        break;
                }
            }
            // Значение для фильтра.
            Console.WriteLine("Введите значение, по которому хотите отфильтровать данные:");
            string filterValue = Console.ReadLine() ?? "";
            // Фильтрация.
            data = DataFilter.FilterPlayers(data, filterRule, filterValue);
            // Сохранение.
            MainUI.SaveData(data);
            return data;
        }

        /// <summary>
        /// Метод SortData сортирует данные по полю и порядку от пользователя.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<Patient> SortData(List<Patient> data)
        {
            // Если пользователь еще не ввел данные, сообщаем об этом.
            if (data is null)
            {
                Console.WriteLine("Данные не обнаружены.");
                return null;
            }
            bool choiceLoop = true;
            // Пользователь должен выбрать одну из 7ми функций для сортировки.
            // Результат сохраняем в делегат.
            Comparison<Patient> sortRule = DataSorter.PlayerIdSort;
            while (choiceLoop)
            {
                choiceLoop = false;
                Console.WriteLine("Выберете вариант из списка:");
                Console.WriteLine("1) Отсортировать по playerId.");
                Console.WriteLine("2) Отсортировать по userName.");
                Console.WriteLine("3) Отсортировать по level.");
                Console.WriteLine("4) Отсортировать по gameScore.");
                Console.WriteLine("5) Отсортировать по achievements.");
                Console.WriteLine("6) Отсортировать по inventory.");
                Console.WriteLine("7) Отсортировать по guild.");
                ConsoleKey inpKey = Console.ReadKey().Key;
                Console.WriteLine();
                switch (inpKey)
                {
                    case ConsoleKey.D1:
                        sortRule = DataSorter.PlayerIdSort;
                        break;
                    case ConsoleKey.D2:
                        sortRule = DataSorter.PlayerNameSort;
                        break;
                    case ConsoleKey.D3:
                        sortRule = DataSorter.PlayerLevelSort;
                        break;
                    case ConsoleKey.D4:
                        sortRule = DataSorter.PlayerGameScoreSort;
                        break;
                    case ConsoleKey.D5:
                        sortRule = DataSorter.PlayerAchievementsSort;
                        break;
                    case ConsoleKey.D6:
                        sortRule = DataSorter.PlayerInventorySort;
                        break;
                    case ConsoleKey.D7:
                        sortRule = DataSorter.PlayerGuildSort;
                        break;
                    default:
                        choiceLoop = true;
                        break;
                }
            }
            // Спрашиваем у пользователя в каком порядке сортировать элементы.
            bool reversed = false;
            choiceLoop = true;
            while (choiceLoop)
            {
                choiceLoop = false;
                Console.WriteLine("Выберите в каком порядке сортировать данные?");
                Console.WriteLine("1) В прямом.");
                Console.WriteLine("2) В обратном.");
                ConsoleKey inpKey = Console.ReadKey().Key;
                Console.WriteLine();
                switch (inpKey)
                {
                    case ConsoleKey.D1:
                        reversed = false;
                        break;
                    case ConsoleKey.D2:
                        reversed = true;
                        break;
                    default:
                        break;
                }
            }

            Comparison<Patient> sorterDelegate = sortRule;
            // Переворачиваем правило, если необходимо.
            if (reversed) sorterDelegate = (x, y) => -sortRule(x, y);
            // Сортируем.
            data.Sort(sorterDelegate);
            // Сохраняем.
            // MainUI.SaveData(data);
            return data;
        }
        */

        private static bool IsPatient(List<Patient> patients, int id, out int ind)
        {
            bool patientFound = false;
            ind = 0;
            for (int i = 0; i < patients.Count; i++)
            {
                if (patients[i].PatientId == id)
                {
                    patientFound = true;
                    ind = i;
                    break;
                }
            }
            return patientFound;
        }

        public static void ChangeObject(List<Patient>? patients, Dictionary<int, Doctor>? doctors)
        {
            if (patients == null || doctors == null)
            {
                Console.WriteLine("Список объектов пуст.");
                return;
            }

            bool patientChoice = false;
            bool choiceLoop = true;
            while (choiceLoop)
            {
                choiceLoop = false;
                Console.WriteLine("Какой объект вы хотите изменить?");
                Console.WriteLine("1) Пациент.");
                Console.WriteLine("2) Доктор.");
                ConsoleKey inpKey = Console.ReadKey().Key;
                Console.WriteLine();
                switch (inpKey)
                {
                    case ConsoleKey.D1:
                        patientChoice = true;
                        break;
                    case ConsoleKey.D2:
                        patientChoice = false;
                        break;
                    default:
                        choiceLoop = true;
                        break;
                }
            }

            if (patientChoice) PatientChange(patients);
        }

        private static void PatientChange(List<Patient> patients)
        {
            int pId = 0;
            int ind = 0;
            Console.WriteLine("Введите id пациента, данные которого хотите отредактировать.");
            while (!int.TryParse(Console.ReadLine(), out pId) || !(IsPatient(patients, pId, out ind)))
            {
                if (!(IsPatient(patients, pId, out ind)))
                {
                    Console.WriteLine("Пациент с этим id не найден.");
                }
                else
                {
                    Console.WriteLine("Введите натуральное число.");
                }
            }

            bool choiceLoop = true;
            ChangerDelegate changeRule = PatientChanger.PatientNameChange;
            while (choiceLoop)
            {
                choiceLoop = false;
                Console.WriteLine("Выберите поле, которое хотите отредактировать.");
                Console.WriteLine("1) _name.");
                Console.WriteLine("2) _age.");
                Console.WriteLine("3) _gender.");
                Console.WriteLine("4) _diagnosis.");
                Console.WriteLine("5) _heartRate.");
                Console.WriteLine("6) _temperature.");
                Console.WriteLine("7) _oxygenSaturation.");
                ConsoleKey inpKey = Console.ReadKey().Key;
                Console.WriteLine();
                switch (inpKey)
                {
                    case ConsoleKey.D1:
                        changeRule = PatientChanger.PatientNameChange;
                        Console.WriteLine($"_name пациента: {patients[ind].Name}");
                        break;
                    case ConsoleKey.D2:
                        changeRule = PatientChanger.PatientAgeChange;
                        Console.WriteLine($"_age пациента: {patients[ind].Age}");
                        break;
                    case ConsoleKey.D3:
                        changeRule = PatientChanger.PatientGenderChange;
                        Console.WriteLine($"_gender пациента: {patients[ind].Gender}");
                        break;
                    case ConsoleKey.D4:
                        changeRule = PatientChanger.PatientDiagnosisChange;
                        Console.WriteLine($"_diagnosis пациента: {patients[ind].Diagnosis}");
                        break;
                    case ConsoleKey.D5:
                        changeRule = PatientChanger.PatientHeartRateChange;
                        Console.WriteLine($"_heartRate пациента: {patients[ind].HeartRate}");
                        break;
                    case ConsoleKey.D6:
                        changeRule = PatientChanger.PatientTemperatureChange;
                        Console.WriteLine($"_temperature пациента: {patients[ind].Temperature}");
                        break;
                    case ConsoleKey.D7:
                        changeRule = PatientChanger.PatientOxygenSaturationChange;
                        Console.WriteLine($"_oxygenSaturation пациента: {patients[ind].OxygenSaturation}");
                        break;
                    default:
                        choiceLoop = true;
                        break;
                }
            }
            Console.WriteLine("Введите значение, на которое вы хотите заменить поле.");
            while (true)
            {
                try
                {
                    string value = Console.ReadLine() ?? "";
                    patients[ind] = changeRule.Invoke(patients[ind], value);
                    Console.WriteLine("Результат:");
                    Console.WriteLine(patients[ind].ToJSON());
                    break;
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine("Строка не может быть пустой. Повторите попытку.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Вы ввели некорректное значение для этого поля. Повторите попытку.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
