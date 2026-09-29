using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP224_Pavlova
{
    public enum Category
    {
        Electronics = 1,
        Food,
        Clothing
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
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Product(int productID, string name, decimal price, int quantity)
        {
            ProductID = productID;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public void PrintInfo()
        {
            Console.WriteLine(
                $"ID: {ProductID}, " +
                $"Название: {Name}, " +
                $"Цена: {Price:F2} руб., " +
                $"Количество: {Quantity}");
        }
    }
    class Program
    {
        static void Main()
        {
            List<Product> products = new List<Product>();

            // Добавление начальных товаров
            products.Add(new Product(1, "Хлеб", 60.50m, 20));
            products.Add(new Product(2, "Молоко", 90.00m, 15));

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
            Console.Write("Введите ID товара: ");
            int productID = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите название товара: ");
            string name = Console.ReadLine();

            Console.Write("Введите цену: ");
            decimal price = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Введите количество: ");
            int quantity = Convert.ToInt32(Console.ReadLine());

            Product product = new Product(
                productID,
                name,
                price,
                quantity);

            products.Add(product);

            Console.WriteLine("Товар добавлен.");
        }

        static void RemoveProduct(List<Product> products)
        {
            Console.Write("Введите ID товара для удаления: ");
            int productID = Convert.ToInt32(Console.ReadLine());

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
