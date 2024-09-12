public class Player {
    public string Name { get; set; }
    public string ClassName { get; set; }
    public int HitPoints { get; set; }
    public int MaxHitPoints { get; }
    public int Strength { get; set; }
    public int Agility { get; set;}
    public int Intelligence { get; set;}
    public int Charisma { get; set; }
    public Location CurrentLocation { get; set; } = World.LocationByID(2);

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
}
