using HW_CPS_HSEZoo.Interfaces;
using HW_CPS_HSEZoo.Models;
using HW_CPS_HSEZoo.Models.Inventory.Animals;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace HW_CPS_HSEZoo
{
    public static class UIZoo
    {
        public static void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Меню:");
                Console.WriteLine("1. Добавить животное в зоопарк.");
                Console.WriteLine("2. Вывести сколько еды нужно всем животным.");
                Console.WriteLine("3. Вывести список 'контактных' животных.");
                Console.WriteLine("4. Выйти из программы.\n");
                Console.WriteLine("чтобы выбрать действие, нажмите соответсвующую клавишу");
                var key = Console.ReadKey().Key;
                Console.WriteLine();
                switch (key)
                {
                    case (ConsoleKey.NumPad1):
                    case (ConsoleKey.D1):
                        Console.Clear();
                        UIZoo.AddAnimal();
                        break;
                    case (ConsoleKey.NumPad2):
                    case (ConsoleKey.D2):
                        Console.Clear();
                        UIZoo.WriteFoodList();
                        break;
                    case (ConsoleKey.NumPad3):
                    case (ConsoleKey.D3):
                        Console.Clear();
                        UIZoo.WriteContactAnimalList();
                        break;
                    case (ConsoleKey.NumPad4):
                    case (ConsoleKey.D4):
                        UIZoo.Exit();
                        return;
                    default:
                        break;
                }
            }
        }


        public static void AddAnimal()
        {
            var services = AppConfig.Services;
            var hseZoo = services.GetRequiredService<HseZoo>();
            var invFactory = services.GetRequiredService<InventoryFactory>();
            var vet = services.GetRequiredService<VetClinic>();

            string animFoodType = GetUserAnimFoodType();
            if (animFoodType == "") return;
            string aClass = GetUserAnimalClass(animFoodType);
            if (aClass == "") return;
            Animal animal = invFactory.CreateAnimal(aClass);

            animal.Food = GetUserNum(
                "Введите положительное число - кг еды, необходимых животному.", 0);
            if (animFoodType == "Herbo") { ((Herbo)animal).Kindness = 
                    GetUserNum("Введите число от 0 до 10 - доброту животного.", 0, 10); }

            if (vet.AnalyzeHealth(animal))
            {
                Console.WriteLine("Животное успешно добавлено!");
                hseZoo.AddToInventory(animal);
            }
            else 
            {
                Console.WriteLine("Животное было недостаточно здоровым.");
            }
            Console.WriteLine("\nНажмите любую клавишу чтобы вернуться в меню.");
            Console.ReadKey();
        }

        private static string GetUserAnimFoodType()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберете класс животного:");
                Console.WriteLine("1. Хищник.");
                Console.WriteLine("2. Травоядное.");
                Console.WriteLine("3. Выйти в меню.\n");
                Console.WriteLine("чтобы выбрать действие, нажмите соответсвующую клавишу");
                var key = Console.ReadKey().Key;
                Console.WriteLine();
                switch (key)
                {
                    case (ConsoleKey.NumPad1):
                    case (ConsoleKey.D1):
                        return "Predator";
                    case (ConsoleKey.NumPad2):
                    case (ConsoleKey.D2):
                        Console.Clear();
                        return "Herbo";
                    case (ConsoleKey.NumPad3):
                    case (ConsoleKey.D3):
                        return "";
                    default:
                        break;
                }
            }
        }

        private static string GetUserAnimalClass(string foodType) 
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберете животное:");
                if (foodType == "Herbo")
                {
                    Console.WriteLine("1. Мартышка.");
                    Console.WriteLine("2. Кролик.");
                }
                else 
                {
                    Console.WriteLine("1. Тигр.");
                    Console.WriteLine("2. Волк.");
                }
                Console.WriteLine("3. Выйти в меню.\n");
                Console.WriteLine("чтобы выбрать действие, нажмите соответсвующую клавишу");
                var key = Console.ReadKey().Key;
                Console.WriteLine();
                switch (key)
                {
                    case (ConsoleKey.NumPad1):
                    case (ConsoleKey.D1):
                        return foodType == "Predator" ? "Tiger" : "Monkey";
                    case (ConsoleKey.NumPad2):
                    case (ConsoleKey.D2):
                        Console.Clear();
                        return foodType == "Predator" ? "Wolf" : "Rabbit";
                    case (ConsoleKey.NumPad3):
                    case (ConsoleKey.D3):
                        return "";
                    default:
                        break;
                }
            }
            return "";
        }

        private static int GetUserNum(string message, int minn = int.MinValue, int maxn = int.MaxValue) 
        {
            int ret = 0;
            do
            {
                Console.Clear();
                Console.WriteLine(message);
            } while (!int.TryParse(Console.ReadLine(), out ret) ||
                        !(minn <= ret && ret <= maxn));
            return ret;
        }

        private static void WriteInventoryData<T>(List<T>? items, string itemType = "инвентаря")
        {
            StringBuilder sb = new StringBuilder();
            if (items == null || items.Count == 0) { sb.AppendLine("Список пуст."); return; }
            foreach (IInventory? item in items)
            {
                if (item == null) { continue; }
                sb.Append($"Id: {item.Number}; Вид {itemType}: ");
                sb.Append(item.GetType().Name);
                if (item is IAlive) { sb.Append($"; Количество еды: {((IAlive)item).Food}"); }
                if (item is IMood) { sb.Append($"; Доброта: {((IMood)item).Kindness}"); }
                sb.AppendLine();
            }
            Console.Write(sb.ToString());
        }

        public static void WriteFoodList()
        {
            var services = AppConfig.Services;
            HseZoo hseZoo = services.GetRequiredService<HseZoo>();
            WriteInventoryData(hseZoo.GetInventoryData<IAlive>(), "животного");

            Console.WriteLine("\nНажмите любую клавишу чтобы вернуться в меню.");
            Console.ReadKey();
        }


        public static void WriteContactAnimalList()
        {
            var services = AppConfig.Services;
            HseZoo hseZoo = services.GetRequiredService<HseZoo>();
            // hseZoo.WriteContactAnimalsData();
            var contactAnims = new List<IMood>(hseZoo.GetInventoryData<IMood>().Where(
                (IMood anim) => { return anim.Kindness >= 5; }));
            WriteInventoryData(contactAnims, "животного");

            Console.WriteLine("\nНажмите любую клавишу чтобы вернуться в меню.");
            Console.ReadKey();
        }


        public static void Exit()
        {
            Console.WriteLine("Выход из программы...");
        }
    }
}
