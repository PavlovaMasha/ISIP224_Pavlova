using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP224_Pavlova
{

    public abstract class Item
    {
        public string Name { get; }
        protected Item(string name) => Name = name;

        public void Describe();
    }

    public class HealingPotion : Item
    {
        public HealingPotion() : base("Лечебное зелье") { }

        public override void Describe()
        {
            Console.WriteLine($"{Name}: полностью восстанавливает здоровье.");
        }
    }

    public class Weapon : Item
    {
        public int Damage { get; }

        public Weapon(string name, int damage) : base(name)
        {
            Damage = damage;
        }

        public override void Describe()
        {
            Console.WriteLine($"{Name}: урон {Damage}");
        }
    }

    public class Armor : Item
    {
        public int Defense { get; }

        public Armor(string name, int defense) : base(name)
        {
            Defense = defense;
        }

        public override void Describe()
        {
            Console.WriteLine($"{Name}: защита {Defense}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
