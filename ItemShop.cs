class ItemShop
{
    public Dictionary<Item, int> Stock;
    private Player Player;

    public ItemShop(Player player)
    {
        Stock = new Dictionary<Item, int>{};
        Player = player;
    }

    public void PurchaseItem(Item item, int price)
    {
        List<Item> itemsToRemove = new(){};
        foreach (KeyValuePair<Item, int> kvp in Stock)
        {
            if (kvp.Value == 0)
            {
                itemsToRemove.Add(kvp.Key);
            }
        }

        foreach(Item items in itemsToRemove)
        {
            Stock.Remove(items);
        }

        if (Player.Coins >= price)
        {
            if (Player.Items.ContainsKey(item))
            {
                Player.Items[item] += 1;
            }
            else
            {
                Player.Items[item] = 1;
            }
            Stock[item] -= 1;
            Player.Coins -= price;
            Console.WriteLine($"{item} has been added to your inventory.");
        }
        else
        {
            Console.WriteLine("You don't have enough funds.");
        }
    }

    public void AlchemistCatalog()
    {
        Consumable potion = new Consumable(5, "Health Potion", "A refreshing potion that restores your health", 5);
        Consumable bigPotion = new Consumable(6, "Greater Health Potion", "An improved potion that restores your health", 10);

        Stock = new Dictionary<Item, int>(){ {potion, 2}, {bigPotion, 2} };

        Console.WriteLine("Welcome, take a look around.\n");

        int itemNumber = 0;
        foreach (KeyValuePair<Item, int> kvp in Stock)
        {
            itemNumber += 1;
            Console.WriteLine($"\n{itemNumber}. {kvp.Key}: {kvp.Value}");
        }
        bool k = true;
        while (k)
        {
            Console.WriteLine("\nType the number corresponding with the item or type 'X' to exit:");

            string userPurchase = Console.ReadLine().ToUpper();

            switch (userPurchase)
            {
                case "1":
                    PurchaseItem(potion, 3);
                    break;
                case "2":
                    PurchaseItem(potion, 5);
                    break;
                case "X":
                    k = false;
                    break;
            }
        }
    }

    public void TownCatalog()
    {
        Consumable potion = new Consumable(5, "Health Potion", "A refreshing potion that restores your health", 5);
        Consumable bigPotion = new Consumable(6, "Greater Health Potion", "An improved potion that restores your health", 10);

        Stock = new Dictionary<Item, int>(){ {potion, 2}, {bigPotion, 2} };

        Console.WriteLine("Welcome, take a look around.\n");

        int itemNumber = 0;
        foreach (KeyValuePair<Item, int> kvp in Stock)
        {
            itemNumber += 1;
            Console.WriteLine($"\n{itemNumber}. {kvp.Key}: {kvp.Value}");
        }

        while (true)
        {
            Console.WriteLine("\nType the number corresponding with the item or type 'X' to exit:");

            string userPurchase = Console.ReadLine();

            switch (userPurchase)
            {
                case "1":
                    PurchaseItem(potion, 3);
                    break;
                case "2":
                    PurchaseItem(potion, 5);
                    break;
            }
        }
    }
}