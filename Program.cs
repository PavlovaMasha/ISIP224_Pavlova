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
        static void Main()
        {
            List<Book> books = new List<Book>();
            books.Add(new Book("1", "Война и мир", "Лев Толстой", BookGenre.Fiction, 1869, 850.00m));
            books.Add(new Book("2", "Преступление и наказание", "Фёдор Достоевский", BookGenre.Detective, 1866, 720.50m));
            books.Add(new Book("3", "Гарри Поттер", "Дж. К. Роулинг", BookGenre.Fantasy, 1997, 950.00m));
            books.Add(new Book("4", "Краткая история времени", "Стивен Хокинг", BookGenre.Science, 1988, 680.00m));
            books.Add(new Book("5", "История России", "Николай Карамзин", BookGenre.History, 1818, 1200.00m));

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Учёт книг в библиотеке");
                Console.WriteLine("1 — Показать все книги");
                Console.WriteLine("2 — Добавить книгу");
                Console.WriteLine("3 — Удалить книгу по ID");
                Console.WriteLine("4 — Найти книгу");
                Console.WriteLine("5 — Сортировать по названию");
                Console.WriteLine("6 — Сортировать по году");
                Console.WriteLine("7 — Самая дорогая и дешёвая книга");
                Console.WriteLine("8 — Количество книг по авторам");
                Console.WriteLine("0 — Выйти");
                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    ShowBooks(books);
                }
                else if (choice == "2")
                {
                    AddBook(books);
                }
                else if (choice == "3")
                {
                    RemoveBook(books);
                }
                else if (choice == "4")
                {
                    SearchBook(books);
                }
                else if (choice == "5")
                {
                    SortByTitle(books);
                }
                else if (choice == "6")
                {
                    SortByYear(books);
                }
                else if (choice == "7")
                {
                    ShowMinMaxPrice(books);
                }
                else if (choice == "8")
                {
                    GroupByAuthor(books);
                }
                else if (choice == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Такой команды нет.");
                }

            }
        }
    }
}
