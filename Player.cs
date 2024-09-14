public class Player {
    public string Name { get; set; }
    public string ClassName { get; set; }
    public int HitPoints { get; set; }
    public int MaxHitPoints { get; }
    public int Strength { get; set; }
    public int Agility { get; set; }
    public int Intelligence { get; set; }
    public int Charisma { get; set; }
    public Location CurrentLocation { get; set; } = World.LocationByID(1);

    public List<Quest> KnownQuests { get; set; } = new();
    public Dictionary<Item, int> Items { get; set; } = new();
    //public List<int> ItemCounts { get; set; } = new();
    public Weapon ActiveWeapon { get; set; }

    public Player(string Name, string ClassName, int HitPoints, int Strength, int Agility, int Intelligence, int Charisma) {
        this.Name = Name;
        this.ClassName = ClassName;
        this.HitPoints = HitPoints;
        this.MaxHitPoints = HitPoints;
        this.Strength = Strength;
        this.Agility = Agility;
        this.Intelligence = Intelligence;
        this.Charisma = Charisma;
    }

    public void AddQuest(Quest Quest) {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("================================================================================================================================");
        Console.WriteLine("   ___                  _         _       _     _          _ ");
        Console.WriteLine("  / _ \\ _   _  ___  ___| |_      / \\   __| | __| | ___  __| |");
        Console.WriteLine(" | | | | | | |/ _ \\/ __| __|    / _ \\ / _` |/ _` |/ _ \\/ _` |");
        Console.WriteLine(" | |_| | |_| |  __/\\__ \\ |_    / ___ \\ (_| | (_| |  __/ (_| |");
        Console.WriteLine("  \\__\\_\\\\__,_|\\___||___/\\__|  /_/   \\_\\__,_|\\__,_|\\___|\\__,_|");
        Console.WriteLine("                                                             ");
        Console.WriteLine("================================================================================================================================");
        this.KnownQuests.Add(Quest);
        Console.WriteLine();
        Console.ResetColor();
        Console.Write("Name: ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(Quest.Name);
        Console.ResetColor();
        Console.Write("Description: ");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(Quest.Description);
        Console.WriteLine("\x1B[0mRewards:");
        foreach (KeyValuePair<Item, int> kvp in Quest.Rewards) {
            Console.WriteLine($" - {kvp.Value}x \x1B[90m{kvp.Key.Name}\x1B[0m");
        }
    }

    public void AskAddQuest(Quest Quest) {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Do you want to accept this quest (y/n):");
        Console.ResetColor();
        Console.Write("Name: ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(Quest.Name);
        Console.ResetColor();
        Console.Write("Description: ");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(Quest.Description);
        Console.WriteLine("\x1B[0mRewards:");
        foreach (KeyValuePair<Item, int> kvp in Quest.Rewards) {
            Console.WriteLine($" - {kvp.Value}x \x1B[90m{kvp.Key.Name}\x1B[0m");
        }

        while (true) {
            string input = Console.ReadLine().ToUpper();
            if (input == "Y") {
                this.KnownQuests.Add(Quest);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Added Quest");
                Console.ResetColor();
                break;
            } else if (input == "N") {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Declined Quest");
                Console.ResetColor();
                break;
            } else {
                continue;
            }
        }
    }

    public void Fight(Monster monster, Player player, Quest Quest)
    {
        bool inCombat = true;

        while (inCombat && this.HitPoints > 0 && monster.CurrentHitPoints > 0)
        {
            Console.WriteLine("Choose an action: (1) Attack (2) Defend (3) Use Potion (4) Flee");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    this.Attack(monster, player);
                    break;
                case "2":
                    //player.Defend();
                    break;
                case "3":
                    //player.UsePotion();
                    break;
                case "4":
                    if (Quest.QuestType == "SIDE")
                    {
                        Console.WriteLine($"You fled from the {monster.Name}. The quest is canceled.");
                        inCombat = false;
                    }
                    else
                    {
                        Console.WriteLine("You cannot flee from a main quest!");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    public void Attack(Monster monster, Player player)
    {
        bool inCombat = true;

        Random rand = new Random();
        int damage = rand.Next(ActiveWeapon.MaxDamage) + this.Strength;
        Console.WriteLine($"{this.Name} attacks {monster.Name} for {damage} damage!");

        monster.CurrentHitPoints -= damage;
        if (monster.CurrentHitPoints > 0)
        {
            monster.Attack(player);
            Console.WriteLine($"{monster.Name} has {monster.CurrentHitPoints} HP left.");
        }

        if (this.HitPoints <= 0)
        {
            Console.WriteLine("You were defeated!");
            inCombat = false;
        }
        else if (monster.CurrentHitPoints <= 0)
        {
            Console.WriteLine($"You defeated {monster.Name}!");
            inCombat = false;
        }
    }
}
