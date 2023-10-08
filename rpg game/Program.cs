using System;

namespace ShadowOfThePastRPG
{
    class Program
    {
        static Random random = new Random();

        static string playerName;
        static string playerClass;
        static int playerStrength;
        static int playerIntelligence;
        static int playerAgility;
        static string playerArtifact;
        static int playerLevel = 1;
        static int playerExperience = 0;
        static int playerHealth;
        static int playerMaxHealth;
        static int playerAttack;

        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в нашу игру 'Тени прошлого'!");
            Console.Write("Введите имя своего персонажа: ");
            playerName = Console.ReadLine();

            Console.WriteLine("Выберите класс персонажа:");
            Console.WriteLine("1) Мечник");
            Console.WriteLine("2) Маг");
            Console.WriteLine("3) Лучник");
            int classChoice = int.Parse(Console.ReadLine());

            InitializePlayer(classChoice);

            Console.WriteLine($"Вы выбрали класс: {playerClass}");
            Console.WriteLine($"Ваши характеристики: Сила - {playerStrength}, Интеллект - {playerIntelligence}, Ловкость - {playerAgility}");
            Console.WriteLine($"Вы начинаете игру с артефактом: {playerArtifact}");
            Console.WriteLine("Вы готовы начать прохождение!");
            Console.WriteLine("Удачи!");

            Console.WriteLine("\nВы находитесь в мрачной пещере. Перед вами развилка.");
            Console.WriteLine("1) Пойти налево");
            Console.WriteLine("2) Пойти направо");
            int pathChoice = int.Parse(Console.ReadLine());

            if (pathChoice == 1)
            {
                Console.WriteLine("\nВы направились налево и наткнулись на монстра!");
                BattleMonster();
            }
            else if (pathChoice == 2)
            {
                Console.WriteLine("\nВы направились направо и наткнулись на сундук с сокровищами!");
                OpenChest();
            }

            while (playerHealth > 0)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1) Сразиться с монстром");
                Console.WriteLine("2) Посмотреть статус персонажа");
                Console.WriteLine("3) Выйти из игры");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        BattleMonster();
                        break;
                    case 2:
                        ShowPlayerStatus();
                        break;
                    case 3:
                        Console.WriteLine("Вы покинули игру. До свидания!");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }

            Console.WriteLine("Вы проиграли. Игра окончена.");
        }

        static void InitializePlayer(int classChoice)
        {
            switch (classChoice)
            {
                case 1:
                    playerClass = "Мечник";
                    playerStrength = 10;
                    playerIntelligence = 5;
                    playerAgility = 7;
                    playerArtifact = "Зуб Драконойда";
                    break;
                case 2:
                    playerClass = "Маг";
                    playerStrength = 5;
                    playerIntelligence = 10;
                    playerAgility = 7;
                    playerArtifact = "Посох леса";
                    break;
                case 3:
                    playerClass = "Лучник";
                    playerStrength = 7;
                    playerIntelligence = 7;
                    playerAgility = 10;
                    playerArtifact = "Клинки Ронаса";
                    break;
                default:
                    Console.WriteLine("Неверный выбор класса. Игра завершена.");
                    Environment.Exit(0);
                    break;
            }

            playerMaxHealth = playerStrength * 10;
            playerHealth = playerMaxHealth;
            playerAttack = playerAgility * 2;
        }

        static void BattleMonster()
        {
            int monsterHealth = random.Next(playerLevel * 10, playerLevel * 15);
            int monsterAttack = random.Next(playerLevel * 2, playerLevel * 5);

            Console.WriteLine($"Вы встретили монстра с {monsterHealth} здоровья!");

            while (playerHealth > 0 && monsterHealth > 0)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1) Атаковать");
                Console.WriteLine("2) Попытаться убежать");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        int playerDamage = random.Next(playerAttack - 5, playerAttack + 5);
                        int monsterDamage = random.Next(monsterAttack - 5, monsterAttack + 5);

                        Console.WriteLine($"Вы нанесли {playerDamage} урона монстру.");
                        Console.WriteLine($"Монстр нанес вам {monsterDamage} урона.");

                        playerHealth -= monsterDamage;
                        monsterHealth -= playerDamage;

                        if (monsterHealth <= 0)
                        {
                            Console.WriteLine("Вы победили монстра!");
                            playerExperience += 20;
                            CheckLevelUp();
                        }

                        break;

                    case 2:
                        Console.WriteLine("Вы попытались убежать.");
                        if (random.Next(0, 2) == 0)
                        {
                            Console.WriteLine("У вас удалось убежать!");
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Монстр помешал вам убежать и атаковал вас.");
                            int escapeDamage = random.Next(monsterAttack - 5, monsterAttack + 5);
                            playerHealth -= escapeDamage;
                        }
                        break;

                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }

            if (playerHealth <= 0)
            {
                Console.WriteLine("Вы проиграли в бою с монстром. Игра окончена.");
                Environment.Exit(0);
            }
        }

        static void ShowPlayerStatus()
        {
            Console.WriteLine("\n--- Статус персонажа ---");
            Console.WriteLine($"Имя: {playerName}");
            Console.WriteLine($"Класс: {playerClass}");
            Console.WriteLine($"Уровень: {playerLevel}");
            Console.WriteLine($"Здоровье: {playerHealth}/{playerMaxHealth}");
            Console.WriteLine($"Сила: {playerStrength}");
            Console.WriteLine($"Интеллект: {playerIntelligence}");
            Console.WriteLine($"Ловкость: {playerAgility}");
            Console.WriteLine($"Опыт: {playerExperience}");
            Console.WriteLine("-----------------------");
        }

        static void CheckLevelUp()
        {
            if (playerExperience >= playerLevel * 100)
            {
                playerLevel++;
                playerMaxHealth += 10;
                playerHealth = playerMaxHealth;
                playerStrength += 2;
                playerIntelligence += 2;
                playerAgility += 2;
                playerAttack = playerAgility * 2;
                Console.WriteLine($"Поздравляем! Вы достигли уровня {playerLevel}!");
            }
        }

        static void OpenChest()
        {
            Console.WriteLine("Вы открываете сундук и находите артефакт!");
            Console.WriteLine("1) Зуб Драконойда");
            Console.WriteLine("2) Посох леса");
            Console.WriteLine("3) Клинки Ронаса");
            int artifactChoice = int.Parse(Console.ReadLine());

            switch (artifactChoice)
            {
                case 1:
                    playerArtifact = "Зуб Драконойда";
                    break;
                case 2:
                    playerArtifact = "Посох леса";
                    break;
                case 3:
                    playerArtifact = "Клинки Ронаса";
                    break;
                default:
                    Console.WriteLine("Неверный выбор артефакта.");
                    break;
            }

            Console.WriteLine($"Вы теперь обладаете артефактом: {playerArtifact}");
        }
    }
}
