using System.Text.Json;
using DataProcessing.PatientProcessing;
using DataProcessing.EventProcessing;
using DataProcessing.Objects;
using DataProcessing.DataProcessing;

namespace MainProgram
{
    /// <summary>
    /// Обеспечивает связь межжду пользователем и библиотекой классов.
    /// </summary>
    public static class MainUI
    {
        /// <summary>
        /// Считывает данные из файла. Работает с пользователем.
        /// </summary>
        /// <param name="patients">Список пациентов</param>
        /// <param name="doctors">Словарь докторов</param>
        public static void GetData(out List<Patient>? patients, out Dictionary<int, Doctor>? doctors)
        {
            Console.WriteLine("Введите путь к файлу для чтения и записи данных.");
            do
            {
                try
                {
                    // Попытка считывания списка объектов из файла.
                    string fPath = Console.ReadLine() ?? "";
                    string jsonTextData = File.ReadAllText(fPath);
                    patients = JsonSerializer.Deserialize<List<Patient>>(jsonTextData);
                    if (patients == null) throw new ArgumentException();

                    // Создание словаря уникальных докторов.
                    doctors = new Dictionary<int, Doctor>();
                    PatientsProcessing.DoctorSeparator(patients, doctors);

                    // Подписка autoSaver на изменение объектов.
                    AutoSaver autoSaver = new AutoSaver(patients, fPath);
                    UpdateSubscriber.UpdateSubscribe(patients, autoSaver);
                    UpdateSubscriber.UpdateSubscribe(doctors.Values, autoSaver);
                    break;
                }
                catch (JsonException ex)
                {
                    Console.WriteLine("Произошла ошибка при считывании объектов.");
                    Console.WriteLine("Повторите попытку.");
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
                    Console.WriteLine(ex);
                }
            } while (true);
        }

        /// <summary>
        /// Выводит данные в консоль или файл. Работает с пользователем.
        /// </summary>
        /// <param name="patients">Список пациентов.</param>
        public static void WriteData(List<Patient>? patients)
        {
            if (patients == null)
            {
                Console.WriteLine("Список объектов пуст.");
                return;
            }
            bool consoleWriteChoice = false;
            bool choiceLoop = true;
            while (choiceLoop)
            {
                choiceLoop = false;
                Console.WriteLine("Куда вы хотите вывести данные?");
                Console.WriteLine("1) Консоль.");
                Console.WriteLine("2) Файл.");
                ConsoleKey inpKey = Console.ReadKey().Key;
                Console.WriteLine();
                switch (inpKey)
                {
                    case ConsoleKey.D1:
                        consoleWriteChoice = true;
                        break;
                    case ConsoleKey.D2:
                        consoleWriteChoice = false;
                        break;
                    default:
                        choiceLoop = true;
                        break;
                }
            }

            // Запись данных в консоль.
            if (consoleWriteChoice)
            {
                Console.WriteLine("Пациенты:");
                DataWriter.Write(patients);
                return;
            }

            // Запись данных в файл.
            Console.WriteLine("Введите путь к файлу, куда вы хотите записать результаты.");
            while (true)
            {
                string fileName = Console.ReadLine() ?? "";
                try
                {
                    DataWriter.Write(patients, fileName);
                    Console.WriteLine("Данные успешно записаны.");
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Произошла непредвиденная ошибка при записи данных в файл. Повторите попытку.");
                }
            }
        }

        /// <summary>
        /// Метод SortData сортирует данные по полю и порядку, введенные пользователем.
        /// </summary>
        /// <param name="patients">Список пациентов</param>
        public static void SortData(List<Patient>? patients)
        {
            if (patients is null)
            {
                Console.WriteLine("Список объектов пуст.");
                return;
            }

            // Пользователь должен выбрать одну из функций для сортировки.
            // Результат сохраняем в делегат.
            bool choiceLoop = true;
            Comparison<Patient> sortRule = DataSorter.PatientIdSort;
            while (choiceLoop)
            {
                choiceLoop = false;
                Console.WriteLine("Выберете вариант из списка:");
                Console.WriteLine("1) Отсортировать по _patientId.");
                Console.WriteLine("2) Отсортировать по _name.");
                Console.WriteLine("3) Отсортировать по _age.");
                Console.WriteLine("4) Отсортировать по _gender.");
                Console.WriteLine("5) Отсортировать по _diagnosis.");
                Console.WriteLine("6) Отсортировать по _heartRate.");
                Console.WriteLine("7) Отсортировать по _temperature.");
                Console.WriteLine("8) Отсортировать по _oxygenSaturation.");
                Console.WriteLine("9) Отсортировать по _doctors.");
                ConsoleKey inpKey = Console.ReadKey().Key;
                Console.WriteLine();
                switch (inpKey)
                {
                    case ConsoleKey.D1:
                        sortRule = DataSorter.PatientIdSort;
                        break;
                    case ConsoleKey.D2:
                        sortRule = DataSorter.PatientNameSort;
                        break;
                    case ConsoleKey.D3:
                        sortRule = DataSorter.PatientAgeSort;
                        break;
                    case ConsoleKey.D4:
                        sortRule = DataSorter.PatientGenderSort;
                        break;
                    case ConsoleKey.D5:
                        sortRule = DataSorter.PatientDiagnosisSort;
                        break;
                    case ConsoleKey.D6:
                        sortRule = DataSorter.PatientHeartRateSort;
                        break;
                    case ConsoleKey.D7:
                        sortRule = DataSorter.PatientTemperatureSort;
                        break;
                    case ConsoleKey.D8:
                        sortRule = DataSorter.PatientOxygenSaturationSort;
                        break;
                    case ConsoleKey.D9:
                        sortRule = DataSorter.PatientDoctorsSort;
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
            patients.Sort(sorterDelegate);
            // Выводим результат на экран.
            DataWriter.Write(patients);
        }

        /// <summary>
        /// Вспомогательный метод. Проверяет, есть ли пациент с нужным id.
        /// Возвращает bool. Возвращает идентификатор этого пациента в массиве через out.
        /// </summary>
        /// <param name="patients">Список пациентов</param>
        /// <param name="id">id искомого пациента</param>
        /// <param name="arrayInd">Идентификатор искомого пациента в списке</param>
        /// <returns>true если пациент найден, иначе false</returns>
        private static bool IsPatient(List<Patient> patients, int id, out int arrayInd)
        {
            bool patientFound = false;
            arrayInd = 0;
            for (int i = 0; i < patients.Count; i++)
            {
                // Если искомый id совпал с пациентом, возвращаем true.
                if (patients[i].PatientId == id)
                {
                    patientFound = true;
                    arrayInd = i;
                    break;
                }
            }
            return patientFound;
        }

        /// <summary>
        /// Метод ChangeObject изменяет выбранный пользователем объект.
        /// </summary>
        /// <param name="patients">Список пациентов</param>
        /// <param name="doctors">Словарь докторов</param>
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
            // Перенаправление на отдельные методы.
            if (patientChoice) PatientChange(patients);
            else DoctorChange(doctors);
        }

        /// <summary>
        /// Вспомогательный метод. Изменяет выбранного пациента. Работает с пользователем.
        /// </summary>
        /// <param name="patients">Список пациентов</param>
        private static void PatientChange(List<Patient> patients)
        {
            // ind - id пациента в массиве.
            // pId - значение поля patientId.
            int ind;
            // Пока пациент не найден, запрашиваем pId у пользователя.
            Console.WriteLine("Введите id пациента, данные которого хотите отредактировать.");
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out int pId))
                {
                    Console.WriteLine("Введите натуральное число.");
                    continue;
                }
                if (!(IsPatient(patients, pId, out ind)))
                {
                    Console.WriteLine("Пациент с этим id не найден.");
                    continue;
                }
                break;
            }

            Console.WriteLine("Пациент найден:");
            Console.WriteLine(patients[ind].ToJSON());

            // Пользователь выбирает делегат для изменения нужного поля.
            bool choiceLoop = true;
            ChangerDelegate changeRule = PatientChanger.PatientNameChange;
            while (choiceLoop)
            {
                choiceLoop = false;
                Console.WriteLine("Выберите поле, которое хотите отредактировать.");
                Console.WriteLine("1) name.");
                Console.WriteLine("2) age.");
                Console.WriteLine("3) gender.");
                Console.WriteLine("4) diagnosis.");
                Console.WriteLine("5) heartRate.");
                Console.WriteLine("6) temperature.");
                Console.WriteLine("7) oxygenSaturation.");
                ConsoleKey inpKey = Console.ReadKey().Key;
                Console.WriteLine();
                switch (inpKey)
                {
                    case ConsoleKey.D1:
                        changeRule = PatientChanger.PatientNameChange;
                        Console.WriteLine($"name пациента: {patients[ind].Name}");
                        break;
                    case ConsoleKey.D2:
                        changeRule = PatientChanger.PatientAgeChange;
                        Console.WriteLine($"age пациента: {patients[ind].Age}");
                        break;
                    case ConsoleKey.D3:
                        changeRule = PatientChanger.PatientGenderChange;
                        Console.WriteLine($"gender пациента: {patients[ind].Gender}");
                        break;
                    case ConsoleKey.D4:
                        changeRule = PatientChanger.PatientDiagnosisChange;
                        Console.WriteLine($"diagnosis пациента: {patients[ind].Diagnosis}");
                        break;
                    case ConsoleKey.D5:
                        changeRule = PatientChanger.PatientHeartRateChange;
                        Console.WriteLine($"heartRate пациента: {patients[ind].HeartRate}");
                        break;
                    case ConsoleKey.D6:
                        changeRule = PatientChanger.PatientTemperatureChange;
                        Console.WriteLine($"temperature пациента: {patients[ind].Temperature}");
                        break;
                    case ConsoleKey.D7:
                        changeRule = PatientChanger.PatientOxygenSaturationChange;
                        Console.WriteLine($"oxygenSaturation пациента: {patients[ind].OxygenSaturation}");
                        break;
                    default:
                        choiceLoop = true;
                        break;
                }
            }

            Console.WriteLine("Введите новое значение поля.");
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
                    Console.WriteLine("Произошла непредвиденная ошибка. Повторите попытку.");
                }
            }
        }

        /// <summary>
        /// Вспомогательный метод. Изменяет выбранного доктора. Работает с пользователем.
        /// </summary>
        /// <param name="doctors">Словарь докторов</param>
        private static void DoctorChange(Dictionary<int, Doctor> doctors)
        {
            int dId;
            Doctor? resDoc;
            Console.WriteLine("Введите id доктора, данные которого хотите отредактировать.");
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out dId))
                {
                    Console.WriteLine("Введите натуральное число.");
                    continue;
                }
                if (!(doctors.TryGetValue(dId, out resDoc)))
                {
                    Console.WriteLine("Доктор с этим id не найден.");
                    continue;
                }
                break;
            }

            Console.WriteLine("Доктор найден:");
            Console.WriteLine(resDoc.ToJSON());

            // По условию пользователь может изменять только имя доктора.
            Console.WriteLine("Введите новое имя доктора.");
            while (true)
            {
                try
                {
                    string value = Console.ReadLine() ?? "";
                    resDoc.Name = value;
                    Console.WriteLine("Результат:");
                    Console.WriteLine(resDoc.ToJSON());
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Произошла непредвиденная ошибка. Повторите попытку.");
                }
            }
        }
    }
}
