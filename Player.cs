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
    public int Coins { get; set; }
    public int Spirit { get; set; } // spirit meter for the monk
    public int SpiritCooldown { get; set; } // cooldown for the spirit usage

    public List<Quest> KnownQuests { get; set; } = new();
    public Dictionary<Spell, int> Spells { get; set; } = null; // Spell and cooldown
    public Dictionary<Item, int> Items { get; set; } = new(); // Item and count
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
        this.Coins = 0;

        if (ClassName == "sorcerer") {
            Spells = new();
            Spells[new HealSpell(0, "Heal Spell", "A powerful spell that will heal the user.", 5, 10)] = 0;
        }

        if (ClassName == "monk")
        {
            this.Spirit = 10; // spirit starting value
            this.SpiritCooldown = 0; // no cooldown at the start
        }
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
        if (Quest.Rewards != null) {
            Console.WriteLine("\x1B[0mRewards:");
            foreach (KeyValuePair<Item, int> kvp in Quest.Rewards) {
                Console.WriteLine($" - {kvp.Value}x \x1B[90m{kvp.Key.Name}\x1B[0m");
            }
        }

        while (true) {
            string input = Console.ReadKey().KeyChar.ToString().ToUpper();
            if (input == "Y") {
                this.KnownQuests.Add(Quest);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nAdded Quest");
                Console.ResetColor();
                break;
            } else if (input == "N") {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nDeclined Quest");
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
            if (this.ClassName == "sorcerer") {
                Console.WriteLine("Choose an action: (1) Attack (2) Defend (3) Use Consumable (4) Flee (5) \x1b[96mOpen Spell book\x1b[0m");
            } else if (this.ClassName == "monk") {
                Console.WriteLine("Choose an action: (1) Attack (2) Defend (3) Use Consumable (4) Flee (5) Use Spirit"); 
            } else {
                Console.WriteLine("Choose an action: (1) Attack (2) Defend (3) Use Consumable (4) Flee");
            }
            string choice = Console.ReadKey().KeyChar.ToString().ToUpper();

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
                    if (!this.UseConsumable()) {
                        Console.Clear();
                        continue;
                    }
                    inCombat = monster.Attack(this);
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
                case "5":
                    if (this.ClassName != "sorcerer") {
                        //Console.WriteLine("Invalid choice.");
                        break;
                    } else {
                        if (!this.UseSpell()) {
                            Console.Clear();
                            continue;
                        }
                        inCombat = monster.Attack(this);
                        break;
                    }
                default:
                    //Console.WriteLine("Invalid choice.");
                    break;
            }
            // Decrease spell cooldowns
            if (this.ClassName == "sorcerer") {
                foreach (KeyValuePair<Spell, int> kvp in this.Spells) {
                    this.Spells[kvp.Key] = Math.Max(0, this.Spells[kvp.Key] - 1);
                }
            }
        }
        Util.pressAnyKey();
    }

    public bool Attack(Monster monster)
    {
        Random rand = new Random();
        int damage;

        if (ActiveWeapon == null && ClassName == "monk") // monk's weaponless attack
        {
            int BareFistBonus = 3;
            damage = rand.Next(0, 5) + this.Strength + BareFistBonus;
            Console.WriteLine($"{this.Name} attacks {monster.Name} for {damage} damage!");
            monster.CurrentHitPoints -= damage;
        }
        
        else if (ActiveWeapon != null)
        {
            damage = rand.Next(ActiveWeapon.MaxDamage) + this.Strength;
            Console.WriteLine($"{this.Name} attacks {monster.Name} for {damage} damage!");
            monster.CurrentHitPoints -= damage;
        }

        //monster.CurrentHitPoints -= damage;
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

            if (ClassName == "monk") // spirit reset for the monk when monster is defeated
            {
                SpiritCooldown = 0;
                Console.WriteLine($"{this.Name} has regained their Spirit");
            }
            // Monster kill reward
            int coinsGained = 0;
            if (monster.Name == "Slime") {
                coinsGained = rand.Next(1, 5);
            } else if (monster.Name == "Spider") {
                coinsGained = rand.Next(3, 8);
                Console.WriteLine("The spider dropped some silk.");
                Util.GivePlayerItems(this, new Dictionary<Item, int>() { { new Item(8, "Spider Silk", "Silk dropped by a spider. It looks quite sturdy, this could be used to craft new weapons."), 1 } });
            }
            Console.WriteLine($"You gained {coinsGained} coins.");
            this.Coins += coinsGained;
            return false;
        }
        return true;
    }

    public void Defend()
    {
        this.IsDefending = true;
        Console.WriteLine($"{this.Name} is bracing for impact!");

    }

    public bool UseConsumable()
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
        Console.WriteLine("\x1B[36mPress enter to close, input any number to use that item\x1B[0m");
        while (true) {
            string input = Console.ReadLine();
            if (input == null || input == "") {
                return false;
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
                        Console.WriteLine($"{this.Name} uses {((Consumable)obj).Name}");
                        // Decrease item count by 1
                        this.Items[((Consumable)obj)] -= 1;
                        // Increase player health
                        this.HitPoints += ((Consumable)obj).Restoration;
                        return true;
                    }
                }
            }
            Console.WriteLine("Invalid input");
        }
    }

    private bool UseSpell() {
        Console.Clear();
        Console.Write($"{this.Name}'s ");
        Console.Write("\x1b[96mSpell book\x1b[0m");
        Console.WriteLine(":");
        foreach (KeyValuePair<Spell, int> kvp in this.Spells) {
            Console.WriteLine($"\x1b[1m\x1b[33m[{kvp.Key.ID}]\x1b[0m {kvp.Key.Name} \x1b[90m(Cooldown: {kvp.Value})\x1b[0m");
        }
        Console.WriteLine("\x1B[36mPress enter to close spell book, enter any number to use that spell.\x1B[0m");
        
        while (true) {
            string input = Console.ReadLine();
            if (input == null || input == "") {
                return false;
            }
            if (!int.TryParse(input, out int Choice)) {
                Console.WriteLine("Invalid input");
                continue;
            }
            foreach (Object obj in this.Spells.Keys) {
                if (obj is Spell) {
                    if (((Spell)obj).ID == Choice) {
                        if (this.Spells[((Spell)obj)] != 0) {
                            Console.WriteLine($"{((Spell)obj).Name} is on cooldown");
                            continue;
                        }
                        Console.Clear();
                        Console.WriteLine($"{this.Name} uses {((Spell)obj).Name}");
                        // Decrease item count by 1
                        this.Spells[((Spell)obj)] = ((Spell)obj).Cooldown;
                        // If it is a heal spell, heal the player
                        if (obj is HealSpell) {
                            this.HitPoints = Math.Min(this.MaxHitPoints, this.HitPoints + ((HealSpell)obj).Heal);
                        }
                        return true;
                    }
                }
            }
            Console.WriteLine("Invalid input");
        }
    }
}
