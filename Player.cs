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
    public bool IsDefending { get; set; } = false;

    public List<Quest> KnownQuests { get; set; } = new();
    public Dictionary<Item, int> Items { get; set; } = new();
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
        Console.WriteLine("                                    ___                  _         _       _     _          _ ");
        Console.WriteLine("                                   / _ \\ _   _  ___  ___| |_      / \\   __| | __| | ___  __| |");
        Console.WriteLine("                                  | | | | | | |/ _ \\/ __| __|    / _ \\ / _` |/ _` |/ _ \\/ _` |");
        Console.WriteLine("                                  | |_| | |_| |  __/\\__ \\ |_    / ___ \\ (_| | (_| |  __/ (_| |");
        Console.WriteLine("                                   \\__\\_\\\\__,_|\\___||___/\\__|  /_/   \\_\\__,_|\\__,_|\\___|\\__,_|");
        Console.WriteLine("                                                                                              ");
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

    public void Fight(Monster monster, Quest Quest)
    {
        Console.Clear();
        bool inCombat = true;

        while (inCombat && this.HitPoints > 0 && monster.CurrentHitPoints > 0)
        {
            Console.WriteLine($"Player \x1B[32mHP\x1B[0m: {this.HitPoints}");
            Console.WriteLine($"{monster.Name} \x1B[32mHP\x1B[0m: {monster.CurrentHitPoints}");
            Console.WriteLine("Choose an action: (1) Attack (2) Defend (3) Use Consumable (4) Flee");
            string choice = Console.ReadLine();

            Console.Clear();
            switch (choice)
            {
                case "1":
                    inCombat = this.Attack(monster);
                    inCombat = monster.Attack(this);
                    break;
                case "2":
                    this.Defend();
                    inCombat = monster.Attack(this);
                    break;
                case "3":
                    this.UseConsumable();
                    break;
                case "4":
                    if (Quest == null || Quest.QuestType == "SIDE")
                    {
                        //Console.WriteLine($"You fled from the {monster.Name}.");
                        inCombat = false;
                        break;
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
        Util.pressAnyKey();
    }

    public bool Attack(Monster monster)
    {
        Random rand = new Random();
        int damage = rand.Next(ActiveWeapon.MaxDamage) + this.Strength;
        Console.WriteLine($"{this.Name} attacks {monster.Name} for {damage} damage!");

        monster.CurrentHitPoints -= damage;
        //if (monster.CurrentHitPoints > 0)
        //{
        //    Console.WriteLine($"{monster.Name} has {monster.CurrentHitPoints} HP left.");
        //}

        if (this.HitPoints <= 0)
        {
            Console.WriteLine("You were defeated!");
            return false;
        }
        else if (monster.CurrentHitPoints <= 0)
        {
            Console.WriteLine($"You defeated {monster.Name}!");
            return false;
        }
        return true;
    }

    public void Defend()
    {
        this.IsDefending = true;
        Console.WriteLine($"{this.Name} is bracing for impact!");

    }

    public void UseConsumable()
    {
        Console.Clear();
        Console.Write($"{this.Name}'s ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Consumable Items");
        Console.ResetColor();
        Console.WriteLine(":");
        for (int i = 0; i < this.Items.Count; i++)
        {
            if (this.Items.ElementAt(i).Key.IsConsumable) {
                Console.WriteLine($"[{this.Items.ElementAt(i).Key.ID}] {this.Items.ElementAt(i).Key.Name} {this.Items.ElementAt(i).Value}x");
            }
        }
        Console.WriteLine("\x1B[36mPress enter to exit, input any number to use that item\x1B[0m");
        bool ChoiceMade = false;
        while (!ChoiceMade) {
            string input = Console.ReadLine();
            if (input == null || input == "") {
                ChoiceMade = true;
                break;
            }
            if (!int.TryParse(input, out int Choice)) {
                Console.WriteLine("Invalid input");
                continue;
            }
            foreach (Object obj in this.Items.Keys) {
                if (obj is Consumable) {
                    if (((Consumable)obj).ID == Choice) {
                        if (this.Items[((Consumable)obj)] == 0) {
                            Console.WriteLine($"You don't have any {((Consumable)obj).Name}.");
                            continue;
                        }
                        // Decrease item count by 1
                        this.Items[((Consumable)obj)] -= 1;
                        // Increase player health
                        this.HitPoints += ((Consumable)obj).Restoration;
                        ChoiceMade = true;
                        break;
                    } else {
                        Console.WriteLine("Invalid input");
                        continue;
                    }
                }
            }
        }
    }
}
