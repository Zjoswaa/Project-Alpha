public class Game {
    private Player Player = null;
    private World World = new();

    private List<Quest> quests = new() {
        new Quest(0, "Go to Duskmire.", "The strange old man told you find the nearby city called Duskmire. Find the way using your map.", "MAIN", new Dictionary<Item, int>() { { items[4], 5 } }),
        new Quest(1, "Gearing up!", "Collect 3 spider silks by defeating spiders in Farmers Meadow's, and collect 3 Bones by defeating skeletons at the Farmhouse", "MAIN", new Dictionary<Item, int>() { { items[4], 5 } }, false),
    };

    private static List<Item> items = new() {
        new Weapon(0, "Rusty Sword", "An old iron sword, it looks rusted.", 10, 5),
        new Weapon(1, "Weak Bow", "An old bow, there are cracks showing in the wood.", 12, 2),
        new Weapon(2, "Crooked Wand", "A wooden stick, there is a leaf growing out of it.", 15, 3),
        new Weapon(3, "Brittle Dagger", "A small homemade dagger, it looks quite brittle.", 12, 1),
        new Item(4, "Coin", "A currency widely used in and around the city of Duskmire."),
        new Consumable(5, "Health Potion", "A refreshing potion that restores your health.", 5),
        new Consumable(6, "Greater Health Potion", "An improved potion that restores your health.", 10),
        new Weapon(7, "Staff", "A monk staff that has been passed down for multiple generations. It holds spiritual energy.", 12, 5),
        new Item(8, "Spider Silk", "Silk dropped by a spider. It looks quite sturdy, this could be used to craft new weapons."),
        new Weapon(9, "Greatsword", "A heavy, steel blade built to cut through armor with raw power.", 15, 7),
        new Weapon(10, "Quickfire Bow", "A lightweight bow designed for rapid firing.", 18, 4),
        new Weapon(11, "Novice Wand", "A simple yet sturdy wand, designed for novice spellcasters to harness their first magical energies.", 20, 5),
        new Weapon(12, "Steel Dagger", "A sharp, compact dagger forged from durable steel, ideal for quick strikes and stealthy maneuvers.", 16, 1),
    };

    private static ItemShop TownShop;
    private static ItemShop AlchemistShop;

    public Game() {
        this.welcome();
        this.createPlayer();
        //this.intro();
        this.start();
    }

    private void Restart() {
        this.Player = null;
        this.World = new();
        this.welcome();
        this.createPlayer();
        this.intro();
        this.start();
    }

    private void GameOverCheck() {
        if (this.Player.HitPoints <= 0) {
            Console.Clear();
            Console.WriteLine("\x1B[31m================================================================================================================================");
            Console.WriteLine("                                        ____                          ___                 ");
            Console.WriteLine("                                       / ___| __ _ _ __ ___   ___    / _ \\__   _____ _ __ ");
            Console.WriteLine("                                      | |  _ / _` | '_ ` _ \\ / _ \\  | | | \\ \\ / / _ \\ '__|");
            Console.WriteLine("                                      | |_| | (_| | | | | | |  __/  | |_| |\\ V /  __/ |   ");
            Console.WriteLine("                                       \\____|\\__,_|_| |_| |_|\\___|   \\___/  \\_/ \\___|_|   ");
            Console.WriteLine("                                                                                          ");
            Console.WriteLine("================================================================================================================================\x1B[0m");
            Console.WriteLine();
            Console.WriteLine("Restart? (Y/N)");
            while (true) {
                string input = Console.ReadLine().ToUpper();
                if (input == "Y") {
                    break;
                } else if (input == "N") {
                    Console.Clear();
                    Console.WriteLine("\x1B[36m================================================================================================================================");
                    Console.WriteLine("                    _____ _                 _            __                     _             _             ");
                    Console.WriteLine("                   |_   _| |__   __ _ _ __ | | _____    / _| ___  _ __    _ __ | | __ _ _   _(_)_ __   __ _ ");
                    Console.WriteLine("                     | | | '_ \\ / _` | '_ \\| |/ / __|  | |_ / _ \\| '__|  | '_ \\| |/ _` | | | | | '_ \\ / _` |");
                    Console.WriteLine("                     | | | | | | (_| | | | |   <\\__ \\  |  _| (_) | |     | |_) | | (_| | |_| | | | | | (_| |");
                    Console.WriteLine("                     |_| |_| |_|\\__,_|_| |_|_|\\_\\___/  |_|  \\___/|_|     | .__/|_|\\__,_|\\__, |_|_| |_|\\__, |");
                    Console.WriteLine("                                                                         |_|            |___/         |___/ ");
                    Console.WriteLine("================================================================================================================================\x1B[0m");
                    System.Environment.Exit(0);
                } else {
                    continue;
                }
            }
            this.Restart();
        }
    }

    private void welcome() {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("================================================================================================================================");
        Console.WriteLine("                 __        ___     _                                  __   _   _           __        __   _     ");
        Console.WriteLine("                 \\ \\      / / |__ (_)___ _ __   ___ _ __ ___    ___  / _| | |_| |__   ___  \\ \\      / /__| |__  ");
        Console.WriteLine("                  \\ \\ /\\ / /| '_ \\| / __| '_ \\ / _ \\ '__/ __|  / _ \\| |_  | __| '_ \\ / _ \\  \\ \\ /\\ / / _ \\ '_ \\ ");
        Console.WriteLine("                   \\ V  V / | | | | \\__ \\ |_) |  __/ |  \\__ \\ | (_) |  _| | |_| | | |  __/   \\ V  V /  __/ |_) |");
        Console.WriteLine("                    \\_/\\_/  |_| |_|_|___/ .__/ \\___|_|  |___/  \\___/|_|    \\__|_| |_|\\___|    \\_/\\_/ \\___|_.__/ ");
        Console.WriteLine("                                        |_|                                                                     ");
        Console.WriteLine("================================================================================================================================");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Press any key to start...");
        Console.ReadKey();
        Console.ResetColor();
    }

    private void intro() {
        Console.Clear();
        Console.Write("You awake in a ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("wooded forest");
        Console.ResetColor();
        Console.Write(" next to your burnt up ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("campfire");
        Console.ResetColor();
        Console.WriteLine(". You don't remember falling asleep.");
        Console.WriteLine("\"Last night must have been rough\" you tell yourself.");
        Util.pressAnyKey();
        Console.WriteLine("The early morning mist clung to the trees, and the distant sounds of the forest surrounded you, birds chirping, leaves rustling.");
        Console.WriteLine("The burnt remains of your campfire gave off a faint, smoky scent, but your mind was clouded with confusion.");
        Console.WriteLine("You couldn't recall how you'd ended up here, or why the campfire had burned down to ashes.");
        Console.WriteLine("You notice a masked man approach you.");
        Util.pressAnyKey();
        Console.WriteLine("Your heart raced as the masked figure drew closer.");
        Console.WriteLine("He moved with a deliberate, almost unnatural calmness, his dark clothes blending with the shadows of the trees.");
        Console.Write("The mask itself was ");
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write("eerie—green");
        Console.ResetColor();
        Console.WriteLine(" and featureless, except for a single vertical scar running down from the left eye to the chin.");
        Util.pressAnyKey();
        Console.WriteLine("\"I see you're awake\" he said, his voice muffled and cold.");
        Console.WriteLine("You swallowed hard, your throat dry. \"Who are you? What happened here?\"");
        Util.pressAnyKey();
        Console.WriteLine("He remained still for a moment, then took a step forward. \"That depends. Do you remember anything about last night?\"");
        Console.WriteLine("You racked your brain, trying to piece together the fragments of memory. But it was like the details slipped away, like smoke.");
        Util.pressAnyKey();
        Console.WriteLine("\"I...I don't remember,\" you admitted, tensing. \"Who are you? What do you want?\"");
        Console.Write("The masked man let out a soft, rattling laugh. \"My name is ");
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Write("Arachthos");
        Console.ResetColor();
        Console.WriteLine("\", he said, \"but this is not about me, this is about you.\"");
        Util.pressAnyKey();
        Console.WriteLine("\"Head to the nearby city of Duskmire if you want to learn what happened.\" Arachthos said.");
        Console.WriteLine("After that he walked away, vanishing into the deep forest just like he appeared.");
        Util.pressAnyKey();
        Console.Clear();
    }


    private void start() {
        Player.AddQuest(quests[0]);
        Util.pressAnyKey();
        

        while (true) {
            this.CheckQuestsCompletion();
            ShowActionMenu();
        }
    }

    private void createPlayer() {
        Console.Clear();
        Console.Write("What is your name: ");
        string name = Console.ReadLine();
        if (name == "" || name == null) {
            name = "Player";
        }
        Console.Clear();
        Console.Write("Select a class for player ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(name);
        Console.WriteLine();
        Console.ResetColor();
        Console.WriteLine("\t\tHP\tSTR\tAGI\tINT\tCHA");
        Console.WriteLine("1: \x1B[91mWarrior\x1B[0m\t80\t7\t2\t1\t2");
        Console.WriteLine("2: \x1B[92mArcher\x1B[0m\t40\t3\t9\t2\t2");
        Console.WriteLine("3: \x1B[96mSorcerer\x1B[0m\t20\t1\t3\t10\t4");
        Console.WriteLine("4: \x1B[34mRogue\x1B[0m\t40\t3\t7\t1\t5");
        Console.WriteLine("5: \x1B[35mMonk\t\x1B[0m\t40\t8\t8\t4\t4");

        bool choiceMade = false;
        while (!choiceMade) {
            switch (Console.ReadKey().KeyChar.ToString().ToUpper()) {
                case "1":
                    this.Player = new Player(name, "warrior", 80, 7, 2, 1, 2);
                    Player.Items[items[0]] = 1;
                    choiceMade = true;
                    break;
                case "2":
                    this.Player = new Player(name, "archer", 40, 3, 9, 2, 2);
                    Player.Items[items[1]] = 1;
                    choiceMade = true;
                    break;
                case "3":
                    this.Player = new Player(name, "sorcerer", 20, 1, 3, 10, 4);
                    Player.Items[items[2]] = 1;
                    choiceMade = true;
                    break;
                case "4":
                    this.Player = new Player(name, "rogue", 40, 3, 7, 1, 5);
                    Player.Items[items[3]] = 1;
                    choiceMade = true;
                    break;
                case "5":
                    this.Player = new Player(name, "monk", 40, 8, 8, 4, 4);
                     // Starter weapon is given to the monk, just not equipped yet.
                    Player.Items[items[7]] = 1;
                    choiceMade = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice (1-5)");
                    continue;
            }
        }

        // Monk starts with bare fists, staff can still be equipped
        if (this.Player.ClassName == "monk") {
            this.Player.ActiveWeapon = null;
        } else {
            List<Weapon> weapons = new(){};
            foreach (KeyValuePair<Item, int> kvp in Player.Items)
            {
                if (kvp.Key is Weapon weapon)
                {
                    weapons.Add(weapon);
                }
            }

            Weapon bestStat = new(99, "", "", 0, 0);
            foreach(Weapon weapon in weapons)
            {
                if (weapon.MaxDamage > bestStat.MaxDamage)
                {
                    bestStat = weapon;
                }
            }
            this.Player.ActiveWeapon = bestStat;
        }

        Console.Clear();
        switch (this.Player.ClassName) {
            case "warrior":
                Console.Write($"Created \x1B[91m{char.ToUpper(this.Player.ClassName[0]) + this.Player.ClassName.Substring(1)}\x1B[0m ");
                break;
            case "archer":
                Console.Write($"Created \x1B[92m{char.ToUpper(this.Player.ClassName[0]) + this.Player.ClassName.Substring(1)}\x1B[0m ");
                break;
            case "sorcerer":
                Console.Write($"Created \x1B[96m{char.ToUpper(this.Player.ClassName[0]) + this.Player.ClassName.Substring(1)}\x1B[0m ");
                break;
            case "rogue":
                Console.Write($"Created \x1B[34m{char.ToUpper(this.Player.ClassName[0]) + this.Player.ClassName.Substring(1)}\x1B[0m ");
                break;
            case "monk":
                Console.Write($"Created \x1B[35m{char.ToUpper(this.Player.ClassName[0]) + this.Player.ClassName.Substring(1)}\x1B[0m "); //
                break;
            default: // Wont happen
                break;
        }
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(name);
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("Good luck, have fun!");
        Util.pressAnyKey("Press any key to start...");
    }

    private void ShowActionMenu() {
        Console.Clear();
        Console.Write("\x1B[32mHP:\x1B[0m ");
        Console.WriteLine($"{this.Player.HitPoints}/{this.Player.MaxHitPoints}");
        Console.Write("\x1B[34mLocation:\x1B[0m ");
        Console.WriteLine(this.Player.CurrentLocation.Name);
        Console.WriteLine($"\x1b[93mCoins: \x1b[0m{this.Player.Coins}");
        Console.WriteLine();
        if (this.Player.CurrentLocation.ID == 4)
        {
            Console.WriteLine("\x1b[1m\x1b[33m[E]\x1b[0m Enter Alchemist's Shop");
        }
        if (this.Player.CurrentLocation.ID == 2)
        {
            Console.WriteLine("\x1b[1m\x1b[33m[E]\x1b[0m Enter Clerk's Shop");
        }
        Console.WriteLine("\x1b[1m\x1b[33m[I]\x1b[0m Open inventory");
        Console.WriteLine("\x1b[1m\x1b[33m[M]\x1b[0m Show map");
        Console.WriteLine("\x1b[1m\x1b[33m[Q]\x1b[0m Manage quests");
        Console.WriteLine("\x1b[1m\x1b[33m[W]\x1b[0m Walk in a direction");
        if (this.Player.CurrentLocation.ID == 1) {
            Console.WriteLine("\x1b[1m\x1b[33m[F]\x1b[0m Fight Slime");
        } else if (this.Player.CurrentLocation.ID == 6) {
            Console.WriteLine("\x1b[1m\x1b[33m[F]\x1b[0m Fight Spider");
        }
        if (Player.ClassName == "sorcerer") {
            Console.WriteLine("\x1b[1m\x1b[33m[S]\x1b[0m \x1b[96mSpell book\x1b[0m");
        }

        bool choiceMade = false;
        while (!choiceMade) {
            switch (Console.ReadKey().KeyChar.ToString().ToUpper()) {
                case "I":
                    this.ShowInventory();
                    choiceMade = true;
                    break;
                case "M":
                    Console.Clear();
                    //Console.WriteLine($"Current location: \x1B[34m{this.Player.CurrentLocation.Name}\x1B[0m");
                    World.WorldMap(this.Player.CurrentLocation);
                    Util.pressAnyKey("Press any key to close the map...");
                    choiceMade = true;
                    break;
                case "Q":
                    this.ManageQuests();
                    choiceMade = true;
                    break;
                case "S":
                    if (Player.ClassName == "sorcerer") {
                        // TODO: Show spell book
                        this.ShowSpellBook();
                        Util.pressAnyKey("Press any key to exit spell book...");
                        choiceMade = true;
                        break;
                    } else {
                        Console.WriteLine("Invalid input");
                        continue;
                    }
                case "W":
                    Location.PlayerMovement(this.Player.CurrentLocation, this.Player);
                    choiceMade = true;
                    break;
                case "F":
                    if (this.Player.CurrentLocation.ID == 1) { // Forest
                        choiceMade = true;
                        this.Player.Fight(new Monster(0, "Slime", 20, 20, 5), null);
                        this.GameOverCheck();
                    } else if (this.Player.CurrentLocation.ID == 6) { // Farmer's Meadows
                        choiceMade = true;
                        this.Player.Fight(new Monster(0, "Spider", 40, 40, 8), null);
                        this.GameOverCheck();
                    } else {
                        Console.WriteLine("Invalid input");
                        continue;
                    }
                    break;
                case "E":
                    if (this.Player.CurrentLocation.ID == 4)
                    {
                        if (AlchemistShop is null)
                        {
                            AlchemistShop = new(Player);
                        }
                        AlchemistShop.AlchemistCatalog();
                        choiceMade = true;
                    }
                    else if (this.Player.CurrentLocation.ID == 2)
                    {
                        if (TownShop is null)
                        {
                            TownShop = new(Player);
                        }
                        TownShop.TownCatalog();
                        choiceMade = true;
                    }
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    continue;
            }
        }
    }

    private void ShowQuests() {
        Console.Clear();
        // First print main quests
        foreach (Quest quest in this.Player.KnownQuests) {
            if (quest.QuestType == "MAIN") {
                if (quest.Completed) {
                    Console.WriteLine($"\x1B[1m\x1B[92m[{quest.ID}]\x1B[0m {quest}");
                } else {
                    Console.WriteLine($"\x1B[1m\x1B[91m[{quest.ID}]\x1B[0m {quest}");
                }
                if (quest.Rewards != null) {
                    foreach (KeyValuePair<Item, int> kvp in quest.Rewards) {
                        Console.WriteLine($"\x1B[0m - {kvp.Value}x \x1B[90m{kvp.Key.Name}\x1B[0m");
                    }
                }
            }
        }
        // Then print side quest
        foreach (Quest quest in this.Player.KnownQuests) {
            if (quest.QuestType == "SIDE") {
                if (quest.Completed) {
                    Console.WriteLine($"\x1B[1m\x1B[92m[{quest.ID}]\x1B[0m {quest}");
                } else {
                    Console.WriteLine($"\x1B[1m\x1B[91m[{quest.ID}]\x1B[0m {quest}");
                }
                if (quest.Rewards != null) {
                    foreach (KeyValuePair<Item, int> kvp in quest.Rewards) {
                        Console.WriteLine($"\x1B[0m - {kvp.Value}x \x1B[90m{kvp.Key.Name}\x1B[0m");
                    }
                }
            }
        }
        Console.WriteLine();
        Console.WriteLine("\x1B[36mPress enter to exit quest menu, input any number to delete that quest. \x1B[1m\x1B[33mMain quests\x1B[0m\x1b[36m cannot be deleted.\x1B[0m");
    }

    private void ManageQuests() {
        this.ShowQuests();
        while (true) {
            string input = Console.ReadLine();
            if (input == null || input == "") {
                break;
            }
            else {
                if (!int.TryParse(input, out int inputNum)) {
                    Console.WriteLine("Invalid input");
                    continue;
                }
                Quest toRemove = null;
                foreach (Quest quest in this.Player.KnownQuests) {
                    if (quest.ID == inputNum) {
                        if (quest.QuestType == "MAIN") {
                            continue;
                        } else if (quest.QuestType == "SIDE") {
                            toRemove = quest;
                        }
                    }
                }
                if (toRemove != null) {
                    this.Player.KnownQuests.Remove(toRemove);
                }
            }
            this.ShowQuests();
        }
    }

    private void ShowInventory() {
        Console.Clear();
        Console.Write($"{Player.Name}'s ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Inventory");
        Console.ResetColor();
        Console.WriteLine(":");
        for (int i = 0; i < this.Player.Items.Count; i++) {
            Console.WriteLine($"[{this.Player.Items.ElementAt(i).Key.ID}] {this.Player.Items.ElementAt(i).Key} {this.Player.Items.ElementAt(i).Value}x");
        }
        Console.WriteLine();
        Console.WriteLine($"\x1b[93mCoins: \x1b[0m{this.Player.Coins}");
        if (this.Player.ActiveWeapon != null) {
            Console.WriteLine($"Current equipped weapon: {this.Player.ActiveWeapon.Name}");
        } else {
            Console.WriteLine($"Current equipped weapon: None");
        }

        Console.WriteLine();
        if (this.Player.ClassName == "monk" && this.Player.ActiveWeapon != null) {
            
            Console.WriteLine("\x1B[36mPress enter to exit quest menu, input any number to switch that weapon to active slot. Press X to Unequip current weapon.\x1b[0m");
        } else {
            Console.WriteLine("\x1B[36mPress enter to exit quest menu, input any number to switch that weapon to active slot.\x1b[0m");
        }
        while (true) {
            string input = Console.ReadLine();
            if (input == null || input == "") {
                break;     
            } else {
                // Unequip possibility for monk.
                if (input.ToUpper() == "X" && this.Player.ClassName == "monk" && this.Player.ActiveWeapon != null) {
                    this.Player.ActiveWeapon = null;
                    break;
                }
                if (!int.TryParse(input, out int inputNum)) {
                    Console.WriteLine("Invalid input");
                    continue;
                }
                bool itemFound = false;
                foreach (Item item in this.Player.Items.Keys) {
                    if (item.ID == inputNum) {
                        itemFound = true;
                    }
                }
                if (!itemFound) {
                    Console.WriteLine("Invalid input");
                    continue;
                }
                if (Util.GetItemByID(inputNum, items) is not Weapon) {
                    Console.WriteLine("This is not a weapon");
                    continue;
                }
                this.Player.ActiveWeapon = (Weapon)Util.GetItemByID(inputNum, items);
                break;
            }
        }
    }

    private void CheckQuestsCompletion() {
        // Check for the "Go to Duskmire" quest completion
        if (this.Player.CurrentLocation == World.LocationByID(2) && !this.Player.KnownQuests[this.Player.KnownQuests.IndexOf(quests[0])].Completed) {
            this.NotifyQuestCompletion(this.Player.KnownQuests[this.Player.KnownQuests.IndexOf(quests[0])]);
            this.Player.KnownQuests[this.Player.KnownQuests.IndexOf(quests[0])].Completed = true;
            this.Player.Coins += 5;
        }

        // Check for the "Web of Intrigue" quest completion
        Quest webOfIntrigueQuest = this.Player.KnownQuests.Find(q => q.ID == 3);
        if (webOfIntrigueQuest != null && !webOfIntrigueQuest.Completed) {
            int spiderSilkCount = this.Player.Items.ContainsKey(items[8]) ? this.Player.Items[items[8]] : 0;
            if (spiderSilkCount >= 3) {
                this.NotifyQuestCompletion(webOfIntrigueQuest);
                webOfIntrigueQuest.Completed = true;
                this.Player.Coins += 5;
                this.Player.Items[items[8]] -= 3; // Remove the spider silks used for the quest
            }
        }
    }


    private void ShowSpellBook() {
        Console.Clear();
        foreach (KeyValuePair<Spell, int> kvp in this.Player.Spells) {
            Console.WriteLine($"{kvp.Key}");
        }
    }

    private void NotifyQuestCompletion(Quest Quest) {
        Console.Clear();
        Console.WriteLine("\x1b[36m================================================================================================================================");
        Console.WriteLine("                           ___                  _       ____                      _      _           _ ");
        Console.WriteLine("                          / _ \\ _   _  ___  ___| |_    / ___|___  _ __ ___  _ __ | | ___| |_ ___  __| |");
        Console.WriteLine("                         | | | | | | |/ _ \\/ __| __|  | |   / _ \\| '_ ` _ \\| '_ \\| |/ _ \\ __/ _ \\/ _` |");
        Console.WriteLine("                         | |_| | |_| |  __/\\__ \\ |_   | |__| (_) | | | | | | |_) | |  __/ ||  __/ (_| |");
        Console.WriteLine("                          \\__\\_\\\\__,_|\\___||___/\\__|   \\____\\___/|_| |_| |_| .__/|_|\\___|\\__\\___|\\__,_|");
        Console.WriteLine("                                                                           |_|                         ");
        Console.WriteLine("================================================================================================================================\x1B[0m");
        Console.WriteLine();
        Console.Write("Name: ");
        Console.WriteLine($"\x1b[32m{Quest.Name}\x1b[0m");
        Console.Write("Description: ");
        Console.WriteLine($"\x1b[90m{Quest.Description}\x1b[0m");
        Console.WriteLine("\x1B[0mRewards:");
        foreach (KeyValuePair<Item, int> kvp in Quest.Rewards) {
            Console.WriteLine($" - {kvp.Value}x \x1B[90m{kvp.Key.Name}\x1B[0m");
        }
        Util.pressAnyKey();
    }
}
