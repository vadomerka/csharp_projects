namespace Program
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Ввод длины массива.
            int len = 0;
            Console.WriteLine("Введите длину массива");
            while (!Int32.TryParse(Console.ReadLine() ?? "", out len) || len < 0) { 
                Console.WriteLine("Введите целое положительное число");
            }
            // Ввод массива строк.
            Console.WriteLine("Введите строки массива:");
            string[] arr = new string[len];
            int shortNum = 0;
            int shortI = 0;
            for (int i = 0; i < len; i++)
            {
                arr[i] = Console.ReadLine() ?? "";
                // Подсчет количества коротких строк.
                if (arr[i].Length <= 3) { 
                    shortNum++;
                }
            }
            // Добавляем все короткие слова в новый массив.
            string[] shortArr = new string[shortNum];
            for (int i = 0; i < len; i++)
            {
                if (arr[i].Length <= 3)
                {
                    shortArr[shortI] = arr[i];
                    shortI++;
                }
            }
            // Вывод нового массива.
            Console.WriteLine("Результат:");
            for (int i = 0; i < shortNum; i++)
            {
                Console.WriteLine(shortArr[i]);
            }
        }
    }
}