using System.Numerics;

public class Game {
    private Player Player = null;
    private World World = new();

    private List<Quest> quests = new() {
        new Quest(0, "Go to Duskmire.", "The strange old man told you find the nearby city called Duskmire. Find the way using your map.", "MAIN", new Dictionary<Item, int>() { { new Item(4, "Coin", "A currency widely used in and around the city of Duskmire."), 5 } }),
        new Quest(1, "Slaying monsters.", "Collect 5 spider silks by defeating spiders in Farmer's Meadows, and collect 5 Bones by defeating skeletons at the Farmhouse.", "MAIN", new Dictionary<Item, int>() { { new Item(4, "Coin", "A currency widely used in and around the city of Duskmire."), 5 } }),
        new Quest(2, "Gearing up!", "Using your silk and bones, upgrade your weapon at the Duskmire smithery.", "MAIN", new Dictionary<Item, int>() { { new Item(4, "Coin", "A currency widely used in and around the city of Duskmire."), 5 } }),
        new Quest(3, "A test of strength", "Talk to the military commander in the camp.", "MAIN", new Dictionary<Item, int>() { { new Item(14, "Key", "An old key, could this be used to unlock something?"), 1 } }),
        new Quest(4, "Enigma", "Go to the prison and crack the code.", "MAIN", new Dictionary<Item, int>() { { new Item(4, "Coin", "A currency widely used in and around the city of Duskmire."), 10 } }),
        new Quest(5, "Answers", "Go to the King's Pass.", "MAIN", new Dictionary<Item, int>() { { new Item(4, "Coin", "A currency widely used in and around the city of Duskmire."), 10 } }),
        new Quest(6, "Enter the palace", "Go to the Royal Palace", "MAIN", new Dictionary<Item, int>() { { new Item(4, "Coin", "A currency widely used in and around the city of Duskmire."), 10 } }),
    };

    private static List<Item> items = new() {
        new Weapon(0, "Rusty Sword", "An old iron sword, it looks rusted.", 10000, 5),
        new Weapon(1, "Weak Bow", "An old bow, there are cracks showing in the wood.", 12, 2),
        new Weapon(2, "Crooked Wand", "A wooden stick, there is a leaf growing out of it.", 15, 3),
        new Weapon(3, "Brittle Dagger", "A small homemade dagger, it looks quite brittle.", 12, 1),
        new Item(4, "Coin", "A currency widely used in and around the city of Duskmire."),
        new Consumable(5, "Health Potion", "A refreshing potion that restores your health.", 10),
        new Consumable(6, "Greater Health Potion", "An improved potion that restores your health.", 20),
        new Weapon(7, "Staff", "A monk staff that has been passed down for multiple generations. It holds spiritual energy.", 12, 5),
        new Item(8, "Spider Silk", "Silk dropped by a spider. It looks quite sturdy, this could be used to craft new weapons."),
        new Weapon(9, "Great sword", "A heavy, steel blade built to cut through armor with raw power.", 15, 7),
        new Weapon(10, "Quickfire Bow", "A lightweight bow designed for rapid firing.", 18, 4),
        new Weapon(11, "Novice Wand", "A simple yet sturdy wand, designed for novice spellcasters to harness their first magical energies.", 20, 5),
        new Weapon(12, "Steel Dagger", "A sharp, compact dagger forged from durable steel, ideal for quick strikes and stealthy maneuvers.", 16, 1),
        new Item(13, "Skeleton Bone", "A bone dropped by a skeleton. It could be used to craft stronger weapons."),
        new Item(14, "Key", "An old key, could this be used to unlock something?")
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
        Console.WriteLine("1: \x1B[91mWarrior\x1B[0m\t80\t7\t2\t3\t2");
        Console.WriteLine("2: \x1B[92mArcher\x1B[0m\t40\t3\t9\t5\t2");
        Console.WriteLine("3: \x1B[96mSorcerer\x1B[0m\t20\t1\t3\t10\t4");
        Console.WriteLine("4: \x1B[34mRogue\x1B[0m\t40\t3\t7\t6\t5");
        Console.WriteLine("5: \x1B[35mMonk\t\x1B[0m\t40\t8\t8\t4\t4");

        bool choiceMade = false;
        while (!choiceMade) {
            switch (Console.ReadKey().KeyChar.ToString().ToUpper()) {
                case "1":
                    this.Player = new Player(name, "warrior", 1000, 7, 2, 10, 2);
                    Player.Items[items[0]] = 1;
                    choiceMade = true;
                    break;
                case "2":
                    this.Player = new Player(name, "archer", 40, 3, 9, 5, 2);
                    Player.Items[items[1]] = 1;
                    choiceMade = true;
                    break;
                case "3":
                    this.Player = new Player(name, "sorcerer", 20, 1, 3, 10, 4);
                    Player.Items[items[2]] = 1;
                    choiceMade = true;
                    break;
                case "4":
                    this.Player = new Player(name, "rogue", 40, 3, 7, 6, 5);
                    Player.Items[items[3]] = 1;
                    choiceMade = true;
                    break;
                case "5":
                    this.Player = new Player(name, "monk", 400, 8, 8, 4, 4);
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
            this.Player.ActiveWeapon = (Weapon)this.Player.Items.ElementAt(0).Key;
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
                Console.Write($"Created \x1B[35m{char.ToUpper(this.Player.ClassName[0]) + this.Player.ClassName.Substring(1)}\x1B[0m ");
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
            Console.WriteLine("\x1b[1m\x1b[33m[E]\x1b[0m Enter Smith's Shop");
        }
        Console.WriteLine("\x1b[1m\x1b[33m[I]\x1b[0m Open inventory");
        Console.WriteLine("\x1b[1m\x1b[33m[M]\x1b[0m Show map");
        Console.WriteLine("\x1b[1m\x1b[33m[Q]\x1b[0m Manage quests");
        Console.WriteLine("\x1b[1m\x1b[33m[W]\x1b[0m Walk in a direction");
        if (this.Player.CurrentLocation.ID == 1) {
            Console.WriteLine("\x1b[1m\x1b[33m[F]\x1b[0m Fight Slime");
        } else if (this.Player.CurrentLocation.ID == 6) {
            Console.WriteLine("\x1b[1m\x1b[33m[F]\x1b[0m Fight Spider");
        } else if (this.Player.CurrentLocation.ID == 7) {
            Console.WriteLine("\x1b[1m\x1b[33m[F]\x1b[0m Fight Skeleton");
        } else if (this.Player.CurrentLocation.ID == 3) {
            Console.WriteLine("\x1b[1m\x1b[33m[F]\x1b[0m Talk to military commander");
        } else if (this.Player.CurrentLocation.ID == 5) {
            Console.WriteLine("\x1b[1m\x1b[33m[F]\x1b[0m Solve the puzzle");
        } else if (this.Player.CurrentLocation.ID == 8 && !this.Player.GuardPassed) {
            Console.WriteLine("\x1b[1m\x1b[33m[F]\x1b[0m Talk to the royal guard");
        } else if (this.Player.CurrentLocation.ID == 9 && !this.Player.BossBeaten) {
            Console.WriteLine("\x1b[1m\x1b[33m[F]\x1b[0m Enter the palace");
        }

        if (Player.ClassName == "sorcerer") {
            Console.WriteLine("\x1b[1m\x1b[33m[S]\x1b[0m \x1b[96mSpell book\x1b[0m");
        }
        if (Player.ClassName == "monk")
        {
            Console.WriteLine("\x1b[1m\x1b[33m[S]\x1b[0m \x1b[96mRest\x1b[0m");
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
                    } 
                    else if (Player.ClassName == "monk")
                    {
                        this.Rest();
                        Util.pressAnyKey();
                        choiceMade = true;
                        break;
                    }
                    else {
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
                        this.Player.Fight(new Monster(1, "Spider", 30, 30, 10), null);
                        this.GameOverCheck();
                    } else if (this.Player.CurrentLocation.ID == 7) { // Farmhouse
                        choiceMade = true;
                        this.Player.Fight(new Monster(2, "Skeleton", 40, 40, 8), null);
                        this.GameOverCheck();
                    } else if (this.Player.CurrentLocation.ID == 3) { // Military camp
                        choiceMade = true;
                        this.MilitaryCommanderDialogue();
                    } else if (this.Player.CurrentLocation.ID == 5) { // Prison
                        choiceMade = true;
                        this.HigherLowerGame(1, 30 - this.Player.Intelligence, this.Player.Intelligence);
                    } else if (this.Player.CurrentLocation.ID == 8 && !this.Player.GuardPassed) {
                        choiceMade = true;
                        this.RoyalGuardDialogue();
                    } else if (this.Player.CurrentLocation.ID == 9 && !this.Player.BossBeaten) {
                        choiceMade = true;
                        this.FinalBossFight();
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

            // Zone unlock
            this.UnlockLocation(World.Locations[7]); // Farmer's Meadows
            this.UnlockLocation(World.Locations[8]); // Farmhouse

            // Add next quest
            this.Player.AddQuest(this.quests[1]);
            Util.pressAnyKey();
        }

        // Check for the "Slaying monsters." quest completion
        Quest slayingMonstersQuest = this.Player.KnownQuests.Find(q => q.ID == 1);
        if (slayingMonstersQuest != null && !slayingMonstersQuest.Completed) {
            int spiderSilkCount = this.Player.Items.ContainsKey(items[8]) ? this.Player.Items[items[8]] : 0;
            int bonesCount = this.Player.Items.ContainsKey(items[13]) ? this.Player.Items[items[13]] : 0;
            if (spiderSilkCount >= 5 && bonesCount >= 5) {
                this.NotifyQuestCompletion(slayingMonstersQuest);
                slayingMonstersQuest.Completed = true;
                this.Player.Coins += 5;

                // Add next quest
                this.Player.AddQuest(quests[2]);
                Util.pressAnyKey();
            }
        }

        // Check for the "Gearing up!" quest completion
        Quest gearingUpQuest = this.Player.KnownQuests.Find(q => q.ID == 2);
        if (gearingUpQuest != null && !gearingUpQuest.Completed) {
            if (this.Player.Items.ContainsKey(items[9]) || this.Player.Items.ContainsKey(items[10]) || this.Player.Items.ContainsKey(items[11]) || this.Player.Items.ContainsKey(items[12]) || this.Player.FistsUpgraded) {
                this.NotifyQuestCompletion(gearingUpQuest);
                gearingUpQuest.Completed = true;
                this.Player.Coins += 5;

                // Zone unlock
                this.UnlockLocation(World.Locations[2]); // Military camp

                // Add next quest
                this.Player.AddQuest(quests[3]);
                Util.pressAnyKey();
            }
        }

        // Check for the "Testing your strength" quest completion
        Quest TestingYourStrengthQuest = this.Player.KnownQuests.Find(q => q.ID == 3);
        if (TestingYourStrengthQuest != null && !TestingYourStrengthQuest.Completed && this.Player.MilitaryCommanderBeaten) {
            TestingYourStrengthQuest.Completed = true;

            // Zone unlock
            this.UnlockLocation(World.Locations[6]); // Prison

            // Add next quest
            this.Player.AddQuest(quests[4]);
            Util.pressAnyKey();
        }

        // Check for the "Enigma" quest completion
        Quest EnigmaQuest = this.Player.KnownQuests.Find(q => q.ID == 4);
        if (EnigmaQuest != null && !EnigmaQuest.Completed && this.Player.HigherLowerGameBeaten) {
            EnigmaQuest.Completed = true;
            this.Player.Coins += 10;

            // Zone unlock
            this.UnlockLocation(World.Locations[4]); // King's Pass

            // Add next quest
            this.Player.AddQuest(quests[5]);
            Util.pressAnyKey();
        }

        // Check for the "Answers" quest completion
        Quest AnswersQuest = this.Player.KnownQuests.Find(q => q.ID == 5);
        if (AnswersQuest != null && !AnswersQuest.Completed && this.Player.GuardPassed) {
            AnswersQuest.Completed = true;
            this.Player.Coins += 10;

            // Zone unlock
            this.UnlockLocation(World.Locations[5]); // Royal Palace

            // TODO add "enter the royal palace" quest
            this.Player.AddQuest(quests[6]);
            Util.pressAnyKey();
        }

        // Check for the "Enter the Royal palace" quest completion
        Quest RoyalPalaceQuest = this.Player.KnownQuests.Find(q => q.ID == 6);
        if (RoyalPalaceQuest != null && !RoyalPalaceQuest.Completed && this.Player.CurrentLocation.ID == 9) {
            RoyalPalaceQuest.Completed = true;
            this.Player.Coins += 10;
        }
    }


    private void ShowSpellBook() {
        Console.Clear();
        foreach (KeyValuePair<Spell, int> kvp in this.Player.Spells) {
            Console.WriteLine($"{kvp.Key}");
        }
    }

    private void Rest()
    {
        Player.Spirit = 10;
        Player.SpiritCooldown = 0;
        Console.Clear();
        Console.WriteLine($"{Player.Name} used Rest and regained their Spirit.");
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

    private void MilitaryCommanderDialogue() {
        Console.Clear();
        Console.WriteLine("You approach the military commander.");
        Util.pressAnyKey();
    
        if (!this.Player.MilitaryCommanderBeaten) {
            Console.WriteLine("\x1b[91m\x1b[1mCommander\x1b[0m: So, you're the one trying to move forward. We don't let just anyone through these gates. Strength, skill, and determination, those are the qualities we need. Prove you have them, and I'll let you pass. He says.");
            Console.WriteLine();
            Console.WriteLine("*He cracks his knuckles and steps forward, sizing you up.*");
            Util.pressAnyKey();
            Console.WriteLine("\x1b[91m\x1b[1mCommander\x1b[0m: Let's see if you're as tough as they say. Ready your weapon, and don't hold back. Show me what you've got!");
        } else {
            Console.WriteLine("\x1b[91m\x1b[1mCommander\x1b[0m: You have already beaten me in battle before. But I am always up for a fight, show me what you've got! He says");
        }
        Util.pressAnyKey();

        this.Player.Fight(new Monster(3, "Military Commander", 50, 50, 10), this.quests[3]);
        this.GameOverCheck();

        Console.Clear();
        Console.WriteLine("*Coughing and out of breath*");
        Console.WriteLine("\x1b[91m\x1b[1mCommander\x1b[0m: Impressive... I didn't think you'd have it in you. Few can best me in a fight.");

        if (!this.Player.MilitaryCommanderBeaten)
        {
            Util.pressAnyKey();
            Console.WriteLine("*He stands up slowly, wiping blood from his lip.*");
            Console.WriteLine(
                "\x1b[91m\x1b[1mCommander\x1b[0m: You’ve earned the right to pass. Consider this a mark of respect. The road ahead is yours. Don’t waste the opportunity.");
        }


        Util.pressAnyKey();

        this.Player.MilitaryCommanderBeaten = true;
    }

    private void RoyalGuardText() {
        Console.WriteLine("You reply:");
        Console.WriteLine($"[1] \"I see you have a keen eye for authority, but perhaps a little incentive could change your mind? \x1b[93m(-{this.Player.BribePrice} gold)\x1b[0m\"");
        Console.WriteLine("[2] \"I don't have time for your rules! Move aside, or face the consequences!\"");
        Console.WriteLine($"[3] *Try to talk your way past the guard* \x1b[93m({this.Player.Charisma} Charisma)\x1b[0m");
    }

   private void RoyalGuardDialogue() {
    Console.Clear();
    Console.WriteLine("You enter the king's pass.");
    Console.WriteLine("You notice an armored guard standing in front of a big gate.");
    Util.pressAnyKey();
    Console.WriteLine("\x1b[33mGuard\x1b[0m: Stay right there! You're not allowed in. Only those with royal permission may pass these gates.");
    Console.WriteLine();
    this.RoyalGuardText();

    bool choiceMade = false;
    Random rand = new();
    while (!choiceMade) {
        string choice = Console.ReadKey().KeyChar.ToString().ToUpper();
        Console.WriteLine();
        switch (choice) {
            case "1":
                if (this.Player.Coins < this.Player.BribePrice) {
                    Console.WriteLine("You don't have enough coins.");
                    Util.pressAnyKey();
                    this.RoyalGuardText();
                    continue;
                }
                Console.WriteLine("\x1b[33mGuard\x1b[0m: Incentive, you say? What do you have in mind?");
                Console.WriteLine($"You give the guard {this.Player.BribePrice} gold.");
                if (rand.Next(0, 11) > 6) {
                    // Success
                    this.Player.GuardPassed = true;
                    Console.WriteLine("\x1b[33mGuard\x1b[0m: You think I can be bought so easily? I'll take your coins, but remember, trust is a fickle thing. Hand them over, and I'll let you through this once.");
                    choiceMade = true;
                    break;
                } else {
                    // Failure
                    Console.WriteLine("\x1b[33mGuard\x1b[0m: Nice try, but you think I'm just a common thug? You’ve insulted my duty by trying to buy your way in. I'll let you try again, but make it worth my while. What else do you have?");
                    this.Player.BribePrice += 10;
                    Util.pressAnyKey();
                    this.RoyalGuardText();
                    continue;
                }
            case "2":
                Console.Clear();
                Console.WriteLine("The guard snorts, raising his weapon");
                Console.WriteLine("\x1b[33mGuard\x1b[0m: Bold words. Let's see if your bravado matches your skill. En garde!");
                Util.pressAnyKey();
                this.Player.Fight(new Monster(4, "Royal Guard", 60, 60, 12), this.quests[5]);
                this.GameOverCheck();
                this.Player.GuardPassed = true;
                choiceMade = true;
                break;
            case "3":
                choiceMade = true;
                break;
            default:
                Console.WriteLine("Invalid input");
                break;
        }
        this.RoyalGuardText();
    }
    Util.pressAnyKey();
}


    private void UnlockLocation(Location Location) {
        Location.IsUnlocked = true;
        Console.Clear();
        Console.WriteLine("\x1b[33m================================================================================================================================");
        Console.WriteLine("                                   _                      _   _       _            _            _ ");
        Console.WriteLine("                                  / \\   _ __ ___  __ _   | | | |_ __ | | ___   ___| | _____  __| |");
        Console.WriteLine("                                 / _ \\ | '__/ _ \\/ _` |  | | | | '_ \\| |/ _ \\ / __| |/ / _ \\/ _` |");
        Console.WriteLine("                                / ___ \\| | |  __/ (_| |  | |_| | | | | | (_) | (__|   <  __/ (_| |");
        Console.WriteLine("                               /_/   \\_\\_|  \\___|\\__,_|   \\___/|_| |_|_|\\___/ \\___|_|\\_\\___|\\__,_|");
        Console.WriteLine("                                                                                                  ");
        Console.WriteLine("================================================================================================================================\x1B[0m");
        Console.WriteLine();
        Console.Write("Name: ");
        Console.WriteLine($"\x1b[32m{Location.Name}\x1b[0m");
        Console.Write("Description: ");
        Console.WriteLine($"\x1b[90m{Location.Description}\x1b[0m");
        Util.pressAnyKey();
    }

    private void HigherLowerGame(int MinNumber, int MaxNumber, int GuessCount) {
        Console.Clear();
        Random random = new Random();
        int secretNumber = random.Next(MinNumber, MaxNumber + 1);

        int turns = 0;
        bool guessedCorrectly = false;
        int playerGuess;

        while (turns < GuessCount) {
            Console.WriteLine($"\nTurn {turns + 1}/{GuessCount}");
            Console.Write($"Guess a number between {MinNumber} and {MaxNumber}: ");
            
            if (!int.TryParse(Console.ReadLine(), out int output)) {
                continue;
            } else {
                playerGuess = output;
            }

            if (playerGuess == secretNumber) {
                guessedCorrectly = true;
                break;
            } else if (playerGuess < secretNumber && turns != GuessCount - 1) {
                Console.WriteLine("\x1b[32mHigher!\x1b[0m");
            } else if (turns != GuessCount - 1) {
                Console.WriteLine("\x1b[31mLower!\x1b[0m");
            }

            turns++;
        }

        if (guessedCorrectly) {
            this.Player.HigherLowerGameBeaten = true;
            Console.WriteLine($"\nCongratulations! You guessed the number {secretNumber} in {turns + 1} turns.");
            Util.pressAnyKey();
        } else {
            Console.WriteLine($"\nA trap triggers and hits you for 5 damage.");
            this.Player.HitPoints = Math.Max(1, this.Player.HitPoints - 5);
            Util.pressAnyKey();
        }
    }

    private void FinalBossFight() {
        Console.WriteLine("In the dim light of the throne room, you see a form standing. His form appearing as an old man wearing a mask.");
        Console.ReadKey();
        Console.WriteLine();
        Console.WriteLine("\x1b[1m\x1b[31mArachthos:\x1b[0m \"Ah, so you've finally made it here. I expected nothing less from someone who has clawed their way through my little games.\"");
        Console.ReadKey();
        Console.WriteLine();
        Console.WriteLine($"\x1b[1m\x1b[93m{this.Player.Name}:\x1b[0m \"What are you talking about? Who are you, really?\"");
        Console.ReadKey();
        Console.WriteLine();
        Console.WriteLine("\x1b[1m\x1b[31mArachthos:\x1b[0m \"You still don't see it, do you? I am Arachthos, the architect of the chaos that has plagued this land. You’ve played right into my hands, thinking you were on a noble quest.\"");
        Console.ReadKey();
        Console.WriteLine();
        Console.WriteLine($"\x1b[1m\x1b[93m{this.Player.Name}:\x1b[0m \"What do you mean? What's your goal?\"");
        Console.ReadKey();
        Console.WriteLine();
        Console.WriteLine("\x1b[1m\x1b[31mArachthos:\x1b[0m \"This kingdom is ripe for change, and I intend to be the one to bring it. You were merely a pawn in my grand design, a tool to test the strength of those who dare to challenge me.\"");
        Console.ReadKey();
        Console.WriteLine();
        Console.WriteLine($"\x1b[1m\x1b[93m{this.Player.Name}:\x1b[0m \"You're the villain behind all this! You've used innocent lives for your own gain!\"");
        Console.ReadKey();
        Console.WriteLine();
        Console.WriteLine("Arachthos straightens, his old form beginning to shimmer ominously");
        Console.WriteLine("\x1b[1m\x1b[31mArachthos:\x1b[0m \"Innocence is a luxury this world can no longer afford. You’ve shown strength, yes, but you lack vision. You should thank me for your trials; they've made you stronger, haven't they?\"");
        Console.ReadKey();
        Console.WriteLine();
        Console.WriteLine($"\x1b[1m\x1b[93m{this.Player.Name}:\x1b[0m \"I'll never join you! I'll put an end to your plans!\"");
        Console.ReadKey();
        Console.WriteLine();
        Console.WriteLine("Arachthos' voice blooms, the illusion of age fades as he transforms into a towering skeleton-spider hybrid");
        Console.WriteLine("\x1b[1m\x1b[31mArachthos:\x1b[0m \"Foolish child! You think you can defy your destiny? This kingdom will fall, and I will rise from its ashes!\"");
        Console.ReadKey();
        Console.WriteLine();
        Console.WriteLine($"\x1b[1m\x1b[93m{this.Player.Name}:\x1b[0m \"What are you?!\"");
        Console.ReadKey();
        Console.WriteLine();
        Console.WriteLine("Arachthos snarls, revealing sharp fangs and glistening legs");
        Console.WriteLine("\x1b[1m\x1b[31mArachthos:\x1b[0m \"I am the darkness incarnate, and soon you will feel the true power of my creation! Prepare yourself!\"");
        Console.ReadKey();
        Console.WriteLine();
        Console.WriteLine($"\x1b[1m\x1b[93m{this.Player.Name}:\x1b[0m \"I'll stop you here and now!\"");
        Console.ReadKey();
        Console.WriteLine();
        Console.WriteLine("Arachthos' skeletal form twists into a battle stance, limbs poised to strike");
        Console.WriteLine("\x1b[1m\x1b[31mArachthos:\x1b[0m \"Then come! Let us see if you are worthy of your own legend. Show me the strength you've gained from my trials, and let us finally settle the score!\"");
    }
}
