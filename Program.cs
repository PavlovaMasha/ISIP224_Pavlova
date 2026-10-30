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

    public abstract class Enemy
    {
        public string Name { get; }

        public int HP { get; set; }

        public int Attack { get; }

        public int Defense { get; }

        protected Random Random { get; } = new Random();
        protected Enemy(string name, int hp, int attack, int defense)
        {

            Name = name;

            HP = hp;

            Attack = attack;

            Defense = defense;

        }
        public bool IsAlive() => HP > 0;
        public abstract void AttackPlayer(Player player);
    }

    public class Goblin : Enemy
    {
        public int CritChance { get; }
        public Goblin() : base("Гоблин", 20, 6, 2)
        {
            CritChance = 20;

        }
        public override void AttackPlayer(Player player)
        {

            int damage = Attack - player.Armor.Defense;
            if (damage < 1)
                damage = 1;
            if (Random.Next(1, 101) <= CritChance)
            {
                damage *= 2;
                Console.WriteLine("Гоблин наносит критический удар!");
            }
            player.HP -= damage;
            Console.WriteLine($"Гоблин наносит {damage} урона.");
        }
    }

    public class Skeleton : Enemy
    {
        public Skeleton() : base("Скелет", 25, 7, 3) { }
        public override void AttackPlayer(Player player)
        {
            player.HP -= Attack;
            Console.WriteLine($"Скелет игнорирует броню и наносит {Attack} урона.");
        }
    }

    public class Mage : Enemy

    {
        public int FreezeChance { get; }
        public Mage() : base("Маг", 18, 5, 1)
        {
            FreezeChance = 25;
        }

        public override void AttackPlayer(Player player)
        {
            int damage = Attack - player.Armor.Defense;
            if (damage < 1)
                damage = 1;
            player.HP -= damage;
            Console.WriteLine($"Маг наносит {damage} урона.");
            if (Random.Next(1, 101) <= FreezeChance)
            {
                player.Frozen = true;
                Console.WriteLine("Маг накладывает заморозку! Игрок пропустит следующий ход.");
            }
        }
    }



    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }
}
