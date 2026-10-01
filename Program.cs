using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace ISIP224_Pavlova
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите количество товаров/услуг, которые будут записаны (от 2 до 100): ");
            int n = Convert.ToInt32(Console.ReadLine());
            Dictionary<string, int> operations = new Dictionary<string, int>();
            for (int i = 0; i < n; i++)
            {
                while (true)
                {
            
                    Console.WriteLine($"{i+1}. Введите операцию по шаблону: Название услуги или товара; Количество денег");
                    string text = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(text))
                    {   
                        Console.WriteLine("Ошибка ввода: ввод не должен быть пустым.");
                        continue;
                    }

                    string[] words = text.Split(';');

                    if (words.Length != 2)
                    {
                        Console.WriteLine("Ошибка ввода: используйте правильный формат: Название товара; Число.");
                        continue;
                    }

                    string nameText = words[0].Trim();
                    string moneyText = words[1].Trim();
                    if (nameText == "")
                    {
                        Console.WriteLine("Ошибка: название товара не указано.");
                        continue;
                    }

                    int money;

                    if (!int.TryParse(moneyText, out money))
                    {
                        Console.WriteLine("Ошибка: количество денег должно быть целым числом.");
                        continue;
                    }

                    if (money < 0)
                    {
                        Console.WriteLine("Ошибка: количество денег не может быть отрицательным.");
                        continue;
                    }

                    if (operations.ContainsKey(nameText))
                    {
                        Console.WriteLine("Ошибка: такое название товара уже есть в списке. Введите другое название. ");
                        continue;
                    }
                    
                    operations.Add(words[0].Trim(), Convert.ToInt32(words[1].Trim()));
                                        
                    break;
                }
            }
            int nmb = 1;
            while (nmb != 0) 
            { 
                Console.WriteLine("");
                Console.WriteLine("Меню");
                Console.WriteLine("1. Вывод данных");
                Console.WriteLine("2. Статистика (среднее, максимальное, минимальное, сумма)");
                Console.WriteLine("3. Сортировка по цене");
                Console.WriteLine("4. Конвертация валюты");
                Console.WriteLine("5. Поиск по названию");
                Console.WriteLine("0. Выход");
                Console.WriteLine("");
                Console.Write("Введите номер пункта меню: ");
                int number = Convert.ToInt32(Console.ReadLine());
                nmb = number;

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
                    default: Console.WriteLine("Такого пункта не существует"); break;
                }
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
            var list = operations.ToList();

            int n = list.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (list[j].Value > list[j + 1].Value)
                    {
                        var temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }
            Console.WriteLine("Отсортировано по возрастанию цены:");
            foreach (var item in list)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }

        static void Convertation(Dictionary<string, int> operations)
        {
            int rate;
            do
            {
                Console.Write("Введите курс конвертации (например, 90): ");
            }
            while (!int.TryParse(Console.ReadLine(), out rate));

            Console.WriteLine("Конвертированные значения:");
            foreach (var item in operations)
            {
                int converted = item.Value * rate;
                Console.WriteLine($"{item.Key}: {converted}");
            }
        }

        static void Found(Dictionary<string, int> operations)
        {
            Console.Write("Введите название для поиска: ");
            string search = Console.ReadLine().Trim().ToLower();
            bool flag = false;
            foreach (var elem in operations)
            {
                if (elem.Key.ToLower().Contains(search)) { Console.WriteLine(elem); flag = true; }
                ;
            }
            if (!flag) { Console.WriteLine("Товар не найден"); }
        }
    }
}