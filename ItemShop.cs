class ItemShop
{
    public Dictionary<Item, int> Stock;
    private Player Player;

    public ItemShop(Player player)
    {
        Player = player;
    }

    public void PurchaseItem(Item item, int price)
    {
        if (Player.Coins >= price && Stock.ContainsKey(item))
        {
            if (Stock[item] != 0)
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
                Console.Clear();
                Console.WriteLine($"{item} has been added to your inventory.");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("This product is out of stock!");
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("You don't have enough funds.");
        }
    }

    public void AlchemistCatalog()
    {
        Consumable potion = new Consumable(5, "Health Potion", "A refreshing potion that restores your health", 5);
        Consumable bigPotion = new Consumable(6, "Greater Health Potion", "An improved potion that restores your health", 10);

        if (Stock is null)
        {
            Stock = new Dictionary<Item, int>(){ {potion, 2}, {bigPotion, 2} };
        }

        Console.Clear();
        Console.WriteLine("Welcome, take a look around.\n");

        bool k = true;
        while (k)
        {
            int itemNumber = 0;
            foreach (KeyValuePair<Item, int> kvp in Stock)
            {
                itemNumber += 1;
                Console.WriteLine($"\n{itemNumber}. {kvp.Key}: {kvp.Value}");
            }

            Console.WriteLine("\nType the number corresponding with the item or type 'X' to exit:");

            string userPurchase = Console.ReadLine().ToUpper();

            switch (userPurchase)
            {
                case "1":
                    PurchaseItem(potion, 3);
                    break;
                case "2":
                    PurchaseItem(bigPotion, 5);
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

        if (Stock is null)
        {
            Stock = new Dictionary<Item, int>(){ {potion, 2}, {bigPotion, 2} };
        }

        Console.Clear();
        Console.WriteLine("Welcome, take a look around.\n");

        bool k = true;
        while (k)
        {
            int itemNumber = 0;
            foreach (KeyValuePair<Item, int> kvp in Stock)
            {
                itemNumber += 1;
                Console.WriteLine($"\n{itemNumber}. {kvp.Key}: {kvp.Value}");
            }

            Console.WriteLine("\nType the number corresponding with the item or type 'X' to exit:");

            string userPurchase = Console.ReadLine().ToUpper();

            switch (userPurchase)
            {
                case "1":
                    PurchaseItem(potion, 3);
                    break;
                case "2":
                    PurchaseItem(bigPotion, 5);
                    break;
                case "X":
                    k = false;
                    break;
            }
        }
    }
}