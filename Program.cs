using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP224_Pavlova
{
    public enum ProdCategory
    {
        Food = 1,
        Drinks,
        Clothing,
        Electronics,
        Other
    }

    //public class Tovar
    //{
    //    public static int Tovar_ID;
    //    public string FullName;
    //    public bool IsInStock;
    //    public int Price;
    //    public int Quantity;
    //    public int Category;
    //    public Tovar(string name, bool isinstock, int price, int quantity, int category)
    //    {
    //        this.FullName = name;
    //        this.IsInStock = isinstock;
    //        this.Price = price;
    //        this.Quantity = quantity;
    //        this.Category = category;
    //    }

    //    //Добавить товар

    //    //Удалить товар

    //    //Заказать поставку товара

    //    //Продать товар

    //    //Поиск товаров(по коду, названию и категории). Необходимо выводить полную информацию о товаре.
    //}

    class Product
    {
        public string ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int QuSklad { get; set; }
        public ProdCategory Category { get; set; }


        public Product(string productID, string name, decimal price, int quantity, int quSklad, ProdCategory category)
        {
            ProductID = productID;
            Name = name;
            Price = price;
            Quantity = quantity;
            QuSklad = quSklad;
            Category = category;
        }

        public void PrintInfo()
        {
            Console.WriteLine(
                $"ID: {ProductID}, " +
                $"Название: {Name}, " +
                $"Цена: {Price:F2} руб., " +
                $"Количество: {Quantity}, " +
                $"Склад: {QuSklad}, " +
                $"Категория: {Category}");
        }
    }
    class Program
    {
        static void Main()
        {
            List<Product> products = new List<Product>();

            // Добавление начальных товаров
            products.Add(new Product("1_1", "Хлеб", 60.50m, 20, 50, ProdCategory.Food));
            products.Add(new Product("1_2", "Молоко", 90.00m, 15, 10, ProdCategory.Food));

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1 — Показать все товары");
                Console.WriteLine("2 — Добавить товар");
                Console.WriteLine("3 — Удалить товар");
                Console.WriteLine("0 — Выйти");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    ShowProducts(products);
                }
                else if (choice == "2")
                {
                    AddProduct(products);
                }
                else if (choice == "3")
                {
                    RemoveProduct(products);
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

        static void ShowProducts(List<Product> products)
        {
            if (products.Count == 0)
            {
                Console.WriteLine("Список товаров пуст.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Список товаров:");

            foreach (Product product in products)
            {
                product.PrintInfo();
            }
        }

        static void AddProduct(List<Product> products)
        {
            Console.Write("Введите ID товара: 1_");
            string productID = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(productID))
            {
                Console.Write("ID товара не введен. Повторите ввод: 1_");
                productID = Console.ReadLine();
            }
            productID = "1_" + productID;

            Console.Write("Введите название товара: ");
            string name = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(name))
            {
                Console.Write("Название не должно быть пустым. Повторите ввод: ");
                name = Console.ReadLine();
            }

            Console.Write("Введите цену: ");
            decimal price;
            while (!decimal.TryParse(Console.ReadLine(), out price) ||
                   price <= 0)
            {
                Console.Write("Ошибка. Введите корректную цену: ");
            }

            Console.Write("Введите количество: ");
            int quantity;
            while (!int.TryParse(Console.ReadLine(), out quantity) ||
                       quantity < 0)
            {
                Console.Write("Ошибка. Введите целое неотрицательное число: ");
            }

            Console.Write("Введите остаток на Складе: ");
            int quSklad;
            while (!int.TryParse(Console.ReadLine(), out quSklad) ||
                       quSklad < 0)
            {
                Console.Write("Ошибка. Введите целое неотрицательное число: ");
            }


            ProdCategory category;
            while (true)
            {
                Console.WriteLine("Выберите категорию товара:");
                Console.WriteLine("1 — Food");
                Console.WriteLine("2 — Drinks");
                Console.WriteLine("3 — Clothing");
                Console.WriteLine("4 — Electronics");
                Console.WriteLine("5 — Other");
                Console.Write("Ваш выбор: ");

                string categoryText = Console.ReadLine();

                if (Enum.TryParse(categoryText, out ProdCategory enterCategory) &&
                    Enum.IsDefined(typeof(ProdCategory), enterCategory))
                {
                    category = enterCategory;
                    break;
                }

                Console.WriteLine("Ошибка. Выберите число от 1 до 5.");
            }

            Product product = new Product(
                productID,
                name,
                price,
                quantity,
                quSklad,
                category
                );

            products.Add(product);

            Console.WriteLine("Товар добавлен.");
        }

        static void RemoveProduct(List<Product> products)
        {
            Console.Write("Введите ID товара для удаления: ");
            string productID = Console.ReadLine();

            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].ProductID == productID)
                {
                    products.RemoveAt(i);
                    Console.WriteLine("Товар удалён.");
                    return;
                }
            }

            Console.WriteLine("Товар с таким ID не найден.");
        }
    }
}
