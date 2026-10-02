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
        static int nextID = 6;
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

        static void ShowBooks(List<Book> books)
        {
            if (books.Count == 0)
            {
                Console.WriteLine("Список книг пуст.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Список книг:");

            foreach (Book book in books)
            {
                book.PrintInfo();
            }
        }

        static void AddBook(List<Book> books)
        {
            string bookID = nextID.ToString();
            nextID++;

            Console.Write("Введите название книги: ");
            string title = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(title))
            {
                Console.Write("Название не должно быть пустым. Повторите ввод: ");
                title = Console.ReadLine();
            }

            Console.Write("Введите автора: ");
            string author = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(author))
            {
                Console.Write("Автор не должен быть пустым. Повторите ввод: ");
                author = Console.ReadLine();
            }

            BookGenre genre;
            while (true)
            {
                Console.WriteLine("Выберите жанр книги:");
                Console.WriteLine("1 — Fiction (Художественная)");
                Console.WriteLine("2 — Science (Научная)");
                Console.WriteLine("3 — History (Историческая)");
                Console.WriteLine("4 — Fantasy (Фэнтези)");
                Console.WriteLine("5 — Detective (Детектив)");
                Console.Write("Ваш выбор: ");

                string genreText = Console.ReadLine();

                if (Enum.TryParse(genreText, out BookGenre enterGenre) &&
                    Enum.IsDefined(typeof(BookGenre), enterGenre))
                {
                    genre = enterGenre;
                    break;
                }

                Console.WriteLine("Ошибка. Выберите число от 1 до 5.");
            }

            Console.Write("Введите год издания: ");
            int year;
            while (!int.TryParse(Console.ReadLine(), out year) ||
                   year <= 0 || year > DateTime.Now.Year)
            {
                Console.Write($"Ошибка. Введите год от 1 до {DateTime.Now.Year}: ");
            }

            Console.Write("Введите цену: ");
            decimal price;
            while (!decimal.TryParse(Console.ReadLine(), out price) ||
                   price <= 0)
            {
                Console.Write("Ошибка. Введите положительную цену: ");
            }

            Book book = new Book(bookID, title, author, genre, year, price);
            books.Add(book);

            Console.WriteLine("Книга добавлена.");
        }

        static void RemoveBook(List<Book> books)
        {
            Console.Write("Введите ID книги для удаления: ");
            string bookID = Console.ReadLine();

            Book book = FindByID(books, bookID);

            if (book == null)
            {
                Console.WriteLine("Книга с таким ID не найдена.");
                return;
            }

            books.Remove(book);
            Console.WriteLine("Книга удалена.");
        }

        static void SearchBook(List<Book> books)
        {
            Console.WriteLine();
            Console.WriteLine("1 — Поиск по названию");
            Console.WriteLine("2 — Поиск по автору");
            Console.WriteLine("3 — Поиск по жанру");
            Console.Write("Выберите способ поиска: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Введите название книги для поиска: ");
                string title = Console.ReadLine();

                var found = books.Where(b => b.Title.ToLower().Contains(title.ToLower())).ToList();

                if (found.Count == 0)
                {
                    Console.WriteLine("Книги не найдены.");
                }
                else
                {
                    Console.WriteLine("Найдено книг: " + found.Count);
                    foreach (Book book in found)
                    {
                        book.PrintInfo();
                    }
                }
            }
            else if (choice == "2")
            {
                Console.Write("Введите автора для поиска: ");
                string author = Console.ReadLine();

                var found = books.Where(b => b.Author.ToLower().Contains(author.ToLower())).ToList();

                if (found.Count == 0)
                {
                    Console.WriteLine("Книги не найдены.");
                }
                else
                {
                    Console.WriteLine("Найдено книг: " + found.Count);
                    foreach (Book book in found)
                    {
                        book.PrintInfo();
                    }
                }
            }
            else if (choice == "3")
            {
                BookGenre genre;
                while (true)
                {
                    Console.WriteLine("Выберите жанр для поиска:");
                    Console.WriteLine("1 — Fiction (Художественная)");
                    Console.WriteLine("2 — Science (Научная)");
                    Console.WriteLine("3 — History (Историческая)");
                    Console.WriteLine("4 — Fantasy (Фэнтези)");
                    Console.WriteLine("5 — Detective (Детектив)");
                    Console.Write("Ваш выбор: ");

                    string genreText = Console.ReadLine();

                    if (Enum.TryParse(genreText, out BookGenre enterGenre) &&
                        Enum.IsDefined(typeof(BookGenre), enterGenre))
                    {
                        genre = enterGenre;
                        break;
                    }

                    Console.WriteLine("Ошибка. Выберите число от 1 до 5.");
                }

                var found = books.Where(b => b.Genre == genre).ToList();

                if (found.Count == 0)
                {
                    Console.WriteLine("Книги такого жанра не найдены.");
                }
                else
                {
                    Console.WriteLine("Найдено книг: " + found.Count);
                    foreach (Book book in found)
                    {
                        book.PrintInfo();
                    }
                }
            }
            else
            {
                Console.WriteLine("Неверный способ поиска.");
            }
        }

        static void SortByTitle(List<Book> books)
        {
            var sorted = books.OrderBy(b => b.Title).ToList();

            Console.WriteLine();
            Console.WriteLine("Книги отсортированы по названию:");
            foreach (Book book in sorted)
            {
                book.PrintInfo();
            }
        }

        static void SortByYear(List<Book> books)
        {
            var sorted = books.OrderBy(b => b.Year).ToList();

            Console.WriteLine();
            Console.WriteLine("Книги отсортированы по году:");
            foreach (Book book in sorted)
            {
                book.PrintInfo();
            }
        }

        static void ShowMinMaxPrice(List<Book> books)
        {
            if (books.Count == 0)
            {
                Console.WriteLine("Список книг пуст.");
                return;
            }

            var mostExpensive = books.OrderByDescending(b => b.Price).ToList();
            var cheapest = books.OrderBy(b => b.Price).ToList();

            Console.WriteLine();
            Console.WriteLine("Самая дорогая книга:");
            mostExpensive[0].PrintInfo();

            Console.WriteLine();
            Console.WriteLine("Самая дешёвая книга:");
            cheapest[0].PrintInfo();
        }

        static void GroupByAuthor(List<Book> books)
        {
            Console.WriteLine("Количество книг по авторам:");

            foreach (var group in books.GroupBy(b => b.Author))
            {
                Console.WriteLine($"{group.Key}: {group.Count()} книг(и)");
            }
        }

        static Book FindByID(List<Book> books, string bookID)
        {
            foreach (Book book in books)
            {
                if (book.BookID == bookID)
                {
                    return book;
                }
            }

            return null;
        }

    }
}
