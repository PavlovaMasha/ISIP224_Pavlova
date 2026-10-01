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
            Console.Write("Ведите текст (минимуму 100 символов): ");
            string text;
            text = Console.ReadLine();
            bool flag = false;
            do {
                text = Console.ReadLine();
                if (text == null || text.Length < 100) { Console.WriteLine("Ошибка. Ваш текст содержит менее 100 символов"); flag = true; }
                else { flag = false; }
            } while (flag);

        }
    }
}
