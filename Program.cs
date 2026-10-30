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

    public class Player
    {
        public int HP { get; set; }
        public int MaxHP { get; }
        public Weapon Weapon { get; set; }
        public Armor Armor { get; set; }
        public bool Frozen { get; set; }
        public Player()
        {
            MaxHP = 50;
            HP = MaxHP;
            Weapon = new Weapon("Ржавый меч", 5);
            Armor = new Armor("Тряпичная роба", 1);
            Frozen = false;
        }
        public void ShowStats()
        {
            Console.WriteLine($"Игрок | HP: {HP}/{MaxHP} | Оружие: {Weapon.Name} ({Weapon.Damage}) | Броня: {Armor.Name} ({Armor.Defense})");
        }
        public void HealFull()
        {
            HP = MaxHP;
            Console.WriteLine("Игрок полностью восстановил здоровье!");
        }
    }


    public class Game
    {
        private Random Random { get; } = new Random();
        private Player Player { get; } = new Player();
        private int Turn { get; set; } = 1;
        private bool PlayerDefendedLastTurn { get; set; }
        public void Start()
        {
            Console.WriteLine("=== Текстовый рогалик ===");
            Console.WriteLine("Нажимайте Enter, чтобы продолжить.");
            while (Player.HP > 0)
            {

                Console.WriteLine($"===== Ход { Turn} ===== ");
                Player.ShowStats();
                if (Turn % 10 == 0)
                {
                    StartBossFight();
                }
                else if (Random.Next(1, 101) <= 50)
                {
                    OpenChest();
                }
                else
                {
                    StartFight(CreateRandomEnemy());
                }
                if (Player.HP <= 0)
                {
                    Console.WriteLine("Игрок погиб.Игра окончена.");
                    break;
                }
                Turn++;
                Console.WriteLine("Нажмите Enter для следующего хода...");
                Console.ReadLine();
            }
        }

        private Enemy CreateRandomEnemy()
        {
            int number = Random.Next(1, 4);
            if (number == 1)
                return new Goblin();
            if (number == 2)
                return new Skeleton();
            return new Mage();
        }

        private Enemy CreateRandomBoss()
        {
            int number = Random.Next(1, 5);
            if (number == 1)
                return new VVG();
            if (number == 2)
                return new Kovalsky();
            if (number == 3)
                return new ArchmageCpp();
            return new PestovCMinusMinus();
        }

        private void OpenChest()
        {
            Console.WriteLine("Вы нашли сундук!");

            int number = Random.Next(1, 4);
            if (number == 1)
            {
                HealingPotion potion = new HealingPotion();
                potion.Describe();
                Player.HealFull();
            }

            else if (number == 2)
            {

                Weapon newItem = CreateRandomWeapon();
                Console.WriteLine("Новый предмет: ");
                newItem.Describe();
                Console.WriteLine("Текущее оружие:");
                Player.Weapon.Describe();
                AskToReplaceWeapon(newItem);
            }

            else
            {
                Armor newItem = CreateRandomArmor();
                Console.WriteLine("Новый предмет: ");
                newItem.Describe();
                Console.WriteLine("Текущий доспех:");
                Player.Armor.Describe();
                AskToReplaceArmor(newItem);
            }
        }

        private Weapon CreateRandomWeapon()
        {
            int number = Random.Next(1, 4);
            if (number == 1)

                return new Weapon("Короткий меч", 6);
            if (number == 2)

                return new Weapon("Боевой топор", 8);
            return new Weapon("Магический посох", 10);
        }

        private Armor CreateRandomArmor()
        {
            int number = Random.Next(1, 4);
            if (number == 1)
                return new Armor("Кожаная броня", 2);
            if (number == 2)
                return new Armor("Кольчуга", 4);
            return new Armor("Стальные доспехи", 6);
        }

        private void AskToReplaceWeapon(Weapon newItem)
        {
            Console.Write("Взять новое оружие? (1 - да, 2 - нет): ");
            string answer = Console.ReadLine();
            if (answer == "1")
            {
                Player.Weapon = newItem;
                Console.WriteLine("Вы взяли новое оружие.");
            }

            else
            {
                Console.WriteLine("Вы выбросили новое оружие.");
            }

        }

        private void AskToReplaceArmor(Armor newItem)
        {
            Console.Write("Взять новый доспех? (1 - да, 2 - нет): ");
            string answer = Console.ReadLine();
            if (answer == "1")
            {
                Player.Armor = newItem;
                Console.WriteLine("Вы взяли новый доспех.");
            }
            else
            {
                Console.WriteLine("Вы выбросили новый доспех.");
            }
        }

        private void StartFight(Enemy enemy)
        {
            Console.WriteLine($"На вас напал { enemy.Name}!");
            Console.WriteLine($"HP: {enemy.HP}, Атака: {enemy.Attack}, Защита: {enemy.Defense}");
            while (Player.HP > 0 && enemy.IsAlive())
            {

                if (Player.Frozen)
                {
                    Console.WriteLine("Игрок заморожен и пропускает ход.");
                    Player.Frozen = false;
                }
                else
                {
                    PlayerTurn(enemy);
                }
                if (!enemy.IsAlive())
                {
                    Console.WriteLine($"{ enemy.Name} побеждён!");
                    break;
                }
                if (Player.HP <= 0)
                    break;
                bool defended = PlayerDefendedLastTurn;
                PlayerDefendedLastTurn = false;
                EnemyTurn(enemy, defended);
            }
        }


        private void StartBossFight()
        {
            Enemy boss = CreateRandomBoss();
            Console.WriteLine("Вы чувствуете мощную ауру...");
            StartFight(boss);
        }

        private void PlayerTurn(Enemy enemy)
        {
            Console.WriteLine("Ваш ход: ");
            Console.WriteLine("1 - Атака");
            Console.WriteLine("2 - Защита");
            Console.Write("Выберите действие: ");
            string choice = Console.ReadLine();
            if (choice == "2")
            {

                PlayerDefendedLastTurn = true;
                Console.WriteLine("Вы готовитесь защищаться.");
                return;

            }
            int damage = Player.Weapon.Damage - enemy.Defense;
            if (damage < 1)
                damage = 1;
            enemy.HP -= damage;
            Console.WriteLine($"Вы атакуете {enemy.Name} и наносите {damage} урона.");
            Console.WriteLine($"У врага осталось HP: {enemy.HP}");

        }

        private void EnemyTurn(Enemy enemy, bool playerDefended)
        {

            Console.WriteLine("Ход врага.");
            if (playerDefended)
            {
                if (Random.Next(1, 101) <= 40)
                {
                    Console.WriteLine("Вы полностью уклонились от атаки!");
                    return;
                }
                int beforeHP = Player.HP;
                enemy.AttackPlayer(Player);
                int takenDamage = beforeHP - Player.HP;
                int blockPercent = Random.Next(70, 101);
                int blockedDamage = takenDamage * blockPercent / 100;
                int finalDamage = takenDamage - blockedDamage;
                Player.HP += takenDamage - finalDamage;
                Console.WriteLine($"Вы заблокировали {blockPercent}% урона. Получено урона: {finalDamage}.");
            }

            else
            {
                enemy.AttackPlayer(Player);
            }
            Console.WriteLine($"Ваше HP: {Player.HP}");
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
