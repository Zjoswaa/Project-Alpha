using System.Drawing;

public class Game {
    private Player Player = null;

    List<Quest> quests = new() {
        new Quest(1, "Go to the city.", "The strange old man told you find the nearby city. Find the way using your map."),
        new Quest(2, "Test", "Completed", true),
        new Quest(3, "Test", "Not completed", false)
    };

    public Game() {
        this.welcome();
        this.createPlayer();
        this.intro();
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

        Player.AddQuest(quests[0]);
        Player.KnownQuests.Add(quests[1]);
        Player.KnownQuests.Add(quests[2]);
        this.pressAnyKey();

        while(true) {
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
        Console.WriteLine("1: Warrior\t80\t7\t2\t1\t2");
        Console.WriteLine("2: Archer\t40\t3\t9\t2\t2");
        Console.WriteLine("3: Sorcerer\t20\t1\t3\t10\t4");
        Console.WriteLine("4: Rogue\t40\t3\t7\t1\t5");
        bool choiceMade = false;
        while (!choiceMade) {
            switch (Console.ReadLine()) {
                case "1":
                    this.Player = new Player(name, "warrior", 80, 7, 2, 1, 2);
                    Player.Items.Add(new Weapon(0, "Rusty Sword", "An old iron sword, it looks rusted.", 10));
                    choiceMade = true;
                    break;
                case "2":
                    this.Player = new Player(name, "archer", 40, 3, 9, 2, 2);
                    Player.Items.Add(new Weapon(1, "Weak Bow", "An old bow, there are cracks showing in the wood.", 12));
                    choiceMade = true;
                    break;
                case "3":
                    this.Player = new Player(name, "sorcerer", 20, 1, 3, 10, 4);
                    Player.Items.Add(new Weapon(2, "Crooked Wand", "A wooden stick, there is a leaf growing out of it.", 15));
                    choiceMade = true;
                    break;
                case "4":
                    this.Player = new Player(name, "rogue", 40, 3, 7, 1, 5);
                    Player.Items.Add(new Weapon(3, "Brittle Dagger", "A small homemade dagger, it looks quite brittle.", 12));
                    choiceMade = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    continue;
            }
        }
        this.Player.ItemCounts.Add(1);
        this.Player.ActiveWeapon = (Weapon)Player.Items[0];
        Console.Write($"Created {this.Player.ClassName} ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(name);
        Console.ResetColor();
        Console.WriteLine(", Good luck!");
        this.pressAnyKey();
    }

    private void ShowActionMenu() {
        Console.Clear();
        Console.WriteLine("I: Open inventory");
        Console.WriteLine("M: Show map");
        Console.WriteLine("Q: Show quests");
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
                    // TODO: Show map
                    choiceMade = true;
                    break;
                case "Q":
                    this.ShowQuests();
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
        foreach (Quest quest in this.Player.KnownQuests) {
            Console.WriteLine($"- {quest}");
        }
        this.pressAnyKey();
    }

    private void ShowInventory() {
        Console.Clear();
        Console.Write($"{Player.Name}'s ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Inventory");
        Console.ResetColor();
        Console.WriteLine(":");
        for (int i = 0; i < this.Player.Items.Count; i++) {
            Console.WriteLine($"- {this.Player.Items[i]} {this.Player.ItemCounts[i]}x");
        }
        Console.WriteLine();
        if (this.Player.ActiveWeapon != null) {
            Console.WriteLine($"Current equipped weapon: \x1B[95m{this.Player.ActiveWeapon.Name}\x1B[0m");
        } else {
            Console.WriteLine($"Current equipped weapon: \x1B[90mNone\x1B[0m");
        }
        this.pressAnyKey();
    }
}
