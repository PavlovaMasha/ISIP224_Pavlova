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
            Console.Write("Ведите текст (минимум 100 символов): ");
            string text;
            bool flag = false;
            do {
                text = Console.ReadLine();
                if (text == null || text.Length < 100) { Console.WriteLine("Ошибка. Ваш текст содержит менее 100 символов"); flag = true; }
                else { flag = false; }
            } while (flag);
            Console.WriteLine("Подсчёт количества слов в тексте: " + Count_of_words(text));
            Console.WriteLine("Поиск самого короткого слова: " + Min_word(text));
            Console.WriteLine("Подсчёт количества предложений: " + Count_of_sentences(text));
            Console.WriteLine("Подсчёт количества гласных букв в тексте: " + Count_of_glas_bykv(text));
            Console.WriteLine("Подсчёт количества согласных букв в тексте: " + Count_of_soglas_bykv(text));
            Console.WriteLine("Самое длинное слово в тексте: " + Max_word(text));
            Console.WriteLine("Cтатистика по частоте встречаемости каждой буквы: " + Count_of_every_bykv(text));

        }
    }
}
