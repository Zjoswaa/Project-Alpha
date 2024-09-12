﻿using System.Drawing;

public class Game {
    private Player Player = null;
    private World World;

    private List<Quest> quests = new() {
        new Quest(0, "Go to the city.", "The strange old man told you find the nearby city. Find the way using your map.", "MAIN", new Dictionary<Item, int>() { { items[4], 5 } }),
        new Quest(1, "Test", "Completed", "SIDE", null, true), // TODO: Remove
        new Quest(2, "Test", "Not completed", "SIDE", null, false), // TODO: Remove
    };

    private static List<Item> items = new() {
        new Weapon(0, "Rusty Sword", "An old iron sword, it looks rusted.", 10),
        new Weapon(1, "Weak Bow", "An old bow, there are cracks showing in the wood.", 12),
        new Weapon(2, "Crooked Wand", "A wooden stick, there is a leaf growing out of it.", 15),
        new Weapon(3, "Brittle Dagger", "A small homemade dagger, it looks quite brittle.", 12),
        new Item(4, "Gold Coin", "A widely used currency in the world of Duskmire")
    };

    public Game() {
        this.welcome();
        this.createPlayer();
        this.intro();
        this.start();
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
        this.pressAnyKey();
        Console.WriteLine("The early morning mist clung to the trees, and the distant sounds of the forest surrounded you, birds chirping, leaves rustling.");
        Console.WriteLine("The burnt remains of your campfire gave off a faint, smoky scent, but your mind was clouded with confusion.");
        Console.WriteLine("You couldn't recall how you'd ended up here, or why the campfire had burned down to ashes.");
        Console.WriteLine("You notice a masked man approach you.");
        this.pressAnyKey();
        Console.WriteLine("Your heart raced as the masked figure drew closer.");
        Console.WriteLine("He moved with a deliberate, almost unnatural calmness, his dark clothes blending with the shadows of the trees.");
        Console.Write("The mask itself was ");
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write("eerie—green");
        Console.ResetColor();
        Console.WriteLine(" and featureless, except for a single vertical scar running down from the left eye to the chin.");
        this.pressAnyKey();
        Console.WriteLine("\"I see you're awake\" he said, his voice muffled and cold.");
        Console.WriteLine("You swallowed hard, your throat dry. \"Who are you? What happened here?\"");
        this.pressAnyKey();
        Console.WriteLine("He remained still for a moment, then took a step forward. \"That depends. Do you remember anything about last night?\"");
        Console.WriteLine("You racked your brain, trying to piece together the fragments of memory. But it was like the details slipped away, like smoke.");
        this.pressAnyKey();
        Console.WriteLine("\"I...I don't remember,\" you admitted, tensing. \"Who are you? What do you want?\"");
        Console.Write("The masked man let out a soft, rattling laugh. \"My name is ");
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Write("Arachthos");
        Console.ResetColor();
        Console.WriteLine("\", he said, \"but this is not about me, this is about you.\"");
        this.pressAnyKey();
        Console.WriteLine("\"Head to the nearby city if you want to learn what happened.\" Arachthos said.");
        Console.WriteLine("After that he walked away, vanishing into the deep forest just like he appeared.");
        this.pressAnyKey();
        Console.Clear();


    }

    private void start() {
        Player.AddQuest(quests[0]);
        this.pressAnyKey();
        //this.Player.AskAddQuest(quests[1]);
        //this.pressAnyKey();
        //this.Player.AskAddQuest(quests[2]);
        //this.pressAnyKey();

        while (true) {
            this.CheckQuestsCompletion();
            ShowActionMenu();
        }
    }

    private void pressAnyKey() {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
        Console.ResetColor();
    }

    private void pressAnyKey(string Message) {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write($"{Message}");
        Console.ReadKey();
        Console.Clear();
        Console.ResetColor();
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
        bool choiceMade = false;
        while (!choiceMade) {
            switch (Console.ReadLine()) {
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
                default:
                    Console.WriteLine("Invalid choice (1-4)");
                    continue;
            }
        }

        this.Player.ActiveWeapon = (Weapon)Player.Items.ElementAt(0).Key;

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
            default: // Wont happen
                break;
        }
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(name);
        Console.ResetColor();
        Console.WriteLine(", Good luck!");
        this.pressAnyKey("Press any key to start...");
    }

    private void ShowActionMenu() {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("HP: ");
        Console.ResetColor();
        Console.WriteLine($"{this.Player.HitPoints}/{this.Player.MaxHitPoints}");
        Console.WriteLine("I: Open inventory");
        Console.WriteLine("M: Show map");
        Console.WriteLine("Q: Manage quests");
        if (Player.ClassName == "sorcerer") {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("S: Spell book");
            Console.ResetColor();
        }

        bool choiceMade = false;
        while (!choiceMade) {
            switch (Console.ReadLine().ToUpper()) {
                case "I":
                    this.ShowInventory();
                    choiceMade = true;
                    break;
                case "M":
                    Console.Clear();
                    Console.WriteLine($"You are currently at: {this.Player.CurrentLocation.Name}");
                    World.WorldMap();
                    this.pressAnyKey();
                    choiceMade = true;
                    break;
                case "Q":
                    this.ManageQuests();
                    choiceMade = true;
                    break;
                case "S":
                    if (Player.ClassName == "sorcerer") {
                        // TODO: Show spell book
                        choiceMade = true;
                        break;
                    } else {
                        Console.WriteLine("Invalid input");
                        continue;
                    }
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
                Console.WriteLine($"\x1B[1m\x1B[33m[{quest.ID}]\x1B[0m {quest}");
                foreach (KeyValuePair<Item, int> kvp in quest.Rewards) {
                    Console.WriteLine($"\x1B[0m - {kvp.Value}x \x1B[90m{kvp.Key.Name}\x1B[0m");
                }
            }
        }
        // Then print side quest
        foreach (Quest quest in this.Player.KnownQuests) {
            if (quest.QuestType == "SIDE") {
                foreach (KeyValuePair<Item, int> kvp in quest.Rewards) {
                    Console.WriteLine($"\x1B[0m - {kvp.Value}x \x1B[90m{kvp.Key.Name}\x1B[0m");
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
                this.Player.KnownQuests.Remove(toRemove);
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
            Console.WriteLine($"- {this.Player.Items.ElementAt(i).Key} {this.Player.Items.ElementAt(i).Value}x");
        }
        Console.WriteLine();
        if (this.Player.ActiveWeapon != null) {
            Console.WriteLine($"Current equipped weapon: \x1B[95m{this.Player.ActiveWeapon.Name}\x1B[0m");
        } else {
            Console.WriteLine($"Current equipped weapon: \x1B[90mNone\x1B[0m");
        }
        this.pressAnyKey("Press any key to exit inventory...");
    }

    private void CheckQuestsCompletion() {
        if (this.Player.CurrentLocation == World.LocationByID(1)) {
            this.Player.KnownQuests[this.Player.KnownQuests.IndexOf(quests[0])].Completed = true;
            // TODO: Add rewards to player inventory
        }
    }
}
