static class Util {
    public static void pressAnyKey() {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
        Console.ResetColor();
    }

    public static void pressAnyKey(string Message) {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write($"{Message}");
        Console.ReadKey();
        Console.Clear();
        Console.ResetColor();
    }

    public static Item GetItemByID(int ID, List<Item> items) {
        foreach (Item item in items) {
            if (item.ID == ID) {
                return item;
            }
        }
        return null;
    }

    public static void GivePlayerItems(Player Player, Dictionary<Item, int> Items) {
        foreach (KeyValuePair<Item, int> kvp in Items) {
            // If the player already has the item
            if (Player.Items.ContainsKey(kvp.Key)) {
                Player.Items[kvp.Key] += kvp.Value;
            }
            // Else add it to the inventory
            else {
                Player.Items[kvp.Key] = kvp.Value;
            }
        }
    }
}
