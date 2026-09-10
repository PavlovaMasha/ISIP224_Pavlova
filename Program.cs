using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ISIP224_Pavlova
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите количество операций, которые будут записаны (от 2 до 40): ");
            int n = Convert.ToInt32(Console.ReadLine());
            Dictionary<string, int> operations = new Dictionary<string, int>();
            for (int i = 0; i < n; i++)
            {
                Console.Write("Введите операцию по шаблону: Название услуги или товара, Количество денег");
                string text = Console.ReadLine();
                string[] words = text.Split(new[] { ';' });
                operations.Add(words[0].Trim(), Convert.ToInt32(words[1].Trim()));
            }
            Console.WriteLine("Меню");
            Console.WriteLine("1. Вывод данных");
            Console.WriteLine("2. Статистика (среднее, максимальное, минимальное, сумма)");
            Console.WriteLine("3. Сортировка по цене");
            Console.WriteLine("4. Конвертация валюты");
            Console.WriteLine("5. Поиск по названию");
            Console.WriteLine("0. Выход");
            Console.WriteLine("");
            Console.Write("Введите номер пункта меню:");
            int number = Convert.ToInt32(Console.ReadLine());
            switch (number)
            {
                case 0: return;
                case 1:
                    foreach (var item in operations)
                    {
                        Console.WriteLine($"{item.Key}: {item.Value}");
                    }
                    break;
                case 2: Statistika(operations); break;
                case 3: Sortirovka(operations); break;
                case 4: Convertation(operations); break;
                case 5: Found(operations); break;
                default: Console.WriteLine("Такого пункта не существует"); return;
            }

        }

        static void Statistika(Dictionary<string, int> operations)
        {
            if (operations.Count == 0)
            {
                Console.WriteLine("Нет данных для статистики.");
                return;
            }

            var values = new List<int>();
            foreach (var elem in operations)
            {
                values.Add(elem.Value);
            }
            double average = values.Average();
            int max = values.Max();
            int min = values.Min();

            Console.WriteLine($"Среднее: {average}");
            Console.WriteLine($"Максимум: {max}");
            Console.WriteLine($"Минимум: {min}");
        }

        static void Sortirovka(Dictionary<string, int> operations)
        {
            //var sorted = operations.OrderBy(x => x.Value);
            //foreach (var item in sorted)
            //{
            //    Console.WriteLine($"{item.Key}: {item.Value}");
            //}
            // переписать
        }

        static void Convertation(Dictionary<string, int> operations)
        {
            Console.Write("Введите курс конвертации (например, 90): ");
            int rate = Convert.ToInt32(Console.ReadLine());
            //пока неизвестно
        }

        static void Found(Dictionary<string, int> operations)
        {
            Console.Write("Введите название для поиска: ");
            string search = Console.ReadLine().Trim();
            foreach (var elem in operations)
            {
                if (elem.Key.Contains(search)) { Console.WriteLine(elem); }
                ;
            }
        }
    }
}