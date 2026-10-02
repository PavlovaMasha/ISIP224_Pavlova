using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP224_Pavlova
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> texts = new List<string>();
            bool work = true;
            while (work)
            {
                Console.Write("Введите текст (минимум 100 символов): ");
                string text;
                bool flag = false;
                do
                {
                    text = Console.ReadLine();

                    if (text == null || text.Length < 100)
                    {
                        Console.WriteLine("Ошибка. Ваш текст содержит менее 100 символов.");
                        Console.Write("Введите текст ещё раз: ");
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                while (flag);
                texts.Add(text);
                Console.WriteLine();
                Console.WriteLine("Подсчёт количества слов в тексте: " + Count_of_words(text));
                Console.WriteLine("Поиск самого короткого слова: " + Min_word(text));
                Console.WriteLine("Подсчёт количества предложений: " + Count_of_sentences(text));
                Console.WriteLine("Подсчёт количества гласных букв в тексте: " + Count_of_glas_bykv(text));
                Console.WriteLine("Подсчёт количества согласных букв в тексте: " + Count_of_soglas_bykv(text));
                Console.WriteLine("Самое длинное слово в тексте: " + Max_word(text));
                Console.WriteLine("Статистика по частоте встречаемости каждой буквы:");
                Count_of_every_bykv(text);
                Console.WriteLine();
                Console.WriteLine("Меню");
                Console.WriteLine("1 - Ввести новый текст");
                Console.WriteLine("2 - Вывести статистику прошлых текстов");
                Console.WriteLine("0 - Завершить программу");
                Console.Write("Ваш выбор: ");

                string choice = Console.ReadLine();
                if (choice == "2")
                {
                    Console.WriteLine();
                    for (int i = 0; i < texts.Count; i++)
                    {
                        Console.WriteLine("Текст №" + (i + 1));
                        Console.WriteLine("Подсчёт количества слов в тексте: " + Count_of_words(texts[i]));
                        Console.WriteLine("Поиск самого короткого слова: " + Min_word(texts[i]));
                        Console.WriteLine("Подсчёт количества предложений: " + Count_of_sentences(texts[i]));
                        Console.WriteLine("Подсчёт количества гласных букв в тексте: " + Count_of_glas_bykv(texts[i]));
                        Console.WriteLine("Подсчёт количества согласных букв в тексте: " + Count_of_soglas_bykv(texts[i]));
                        Console.WriteLine("Самое длинное слово в тексте: " + Max_word(texts[i]));
                        Console.WriteLine("Статистика по частоте встречаемости каждой буквы:");
                        Count_of_every_bykv(texts[i]);
                        Console.WriteLine();
                    }
                }
                else if (choice == "0")
                {
                    work = false;
                }
            }
            Console.WriteLine("Программа завершена.");
        }

        static string[] GetWords(string text)
        {
            char[] separators =
            {
                ' ', '\t', '\n', '\r',
                '.', ',', '!', '?', ';', ':',
                '-', '(', ')', '[', ']',
                '"', '«', '»'
            };
            return text.Split(separators, StringSplitOptions.RemoveEmptyEntries); //игнорит пустые строки
        }

        static int Count_of_words(string text)
        {
            string[] words = GetWords(text);
            return words.Length;
        }

        static string Min_word(string text)
        {
            string[] words = GetWords(text);
            string minWord = words[0];
            for (int i = 1; i < words.Length; i++)
            {
                if (words[i].Length < minWord.Length)
                {
                    minWord = words[i];
                }
            }
            return minWord;
        }

        static int Count_of_sentences(string text)
        {
            int count = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '.' || text[i] == '!' || text[i] == '?')
                {
                    count++;
                }
            }
            return count;
        }

        static int Count_of_glas_bykv(string text)
        {
            string glas_b = "аеёиоуыэюяАЕЁИОУЫЭЮЯ";
            int count = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (glas_b.Contains(text[i]))
                {
                    count++;
                }
            }
            return count;
        }

        static int Count_of_soglas_bykv(string text)
        {
            string soglas_b = "бвгджзйклмнпрстфхцчшщБВГДЖЗЙКЛМНПРСТФХЦЧШЩ";
            int count = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (soglas_b.Contains(text[i]))
                {
                    count++;
                }
            }
            return count;
        }

        static string Max_word(string text)
        {
            string[] words = GetWords(text);
            string maxWord = words[0];
            for (int i = 1; i < words.Length; i++)
            {
                if (words[i].Length > maxWord.Length)
                {
                    maxWord = words[i];
                }
            }
            return maxWord;
        }

        static void Count_of_every_bykv(string text)
        {
            string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            text = text.ToLower();
            for (int i = 0; i < alphabet.Length; i++)
            {
                int count = 0;
                for (int j = 0; j < text.Length; j++)
                {
                    if (text[j] == alphabet[i])
                    {
                        count++;
                    }
                }
                if (count > 0)
                {
                    Console.WriteLine("Количество букв " + alphabet[i] + ": " + count);
                }
            }

        }

    }
}
