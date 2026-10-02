using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP224_Pavlova
{
    public enum BookGenre
    {
        Fiction = 1,
        Science,
        History,
        Fantasy,
        Detective
    }
    class Book
    {
        public string BookID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public BookGenre Genre { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public Book(string bookID, string title, string author, BookGenre genre, int year, decimal price)
        {
            BookID = bookID;
            Title = title;
            Author = author;
            Genre = genre;
            Year = year;
            Price = price;
        }

        public void PrintInfo()
        {
            Console.WriteLine(
                $"ID: {BookID}, " +
                $"Название: {Title}, " +
                $"Автор: {Author}, " +
                $"Жанр: {Genre}, " +
                $"Год: {Year}, " +
                $"Цена: {Price:F2} руб."); //Fixed-point, количество знаков после запятой
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
