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
    public class Tovar
    {
        public static int Tovar_ID;
        public string FullName;
        public bool IsInStock;
        public int Price;
        public int Quantity;
        public int Category;
        public Person(string name, bool isinstock, int price, int quantity, int category)
        {
            this.FullName = name;
            this.IsInStock = isinstock;
            this.Price = price;
            this.Quantity = quantity;
            this.Category = category;
        }

        //Добавить товар

        //Удалить товар

        //Заказать поставку товара

        //Продать товар

        //Поиск товаров(по коду, названию и категории). Необходимо выводить полную информацию о товаре.
    }
}
