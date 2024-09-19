class ItemShop
{
    public Dictionary<Item, int> Stock;
    private Player Player;

    //Instantiate ItemShop with a player field to access currency
    public ItemShop(Player player)
    {
        Player = player;
    }

    // Method used to make comparisons regarding the player's inventory and the shop's stock
    // Responsible for removing item from stock and adding it to the player's inventory, for a price
    public void PurchaseItem(Item item, int price)
    {
        Console.Clear();
        Console.WriteLine($"This product costs {price} gold coins. Purchase this item? (y/n)");
        string purchaseInput = Console.ReadLine().ToLower();
        while (purchaseInput != "y" && purchaseInput != "n")
        {
            Console.WriteLine("Invalid input.");
            Console.WriteLine($"This product costs {price} gold coins. Purchase this item? (y/n)");
            purchaseInput = Console.ReadLine().ToLower();
        }
        if (purchaseInput == "n")
        {
            Console.Clear();
            return;
        }

        if (Player.Coins >= price)
        {
            if (Stock[item] != 0)
            {
                // Checks if item already exists in player inventory
                // If it does, increases value by 1 rather than adding the same item separately
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

    //Method that gets called to create the alchemist's shop
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
                Console.WriteLine($"\n{itemNumber}. {kvp.Key}: {kvp.Value} left");
            }

            Console.WriteLine("\nType the number corresponding with the item or type 'X' to exit:");

            string userPurchase = Console.ReadLine().ToUpper();

            //Will have to add more cases if you want to add more items to the shop
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

    //Method that gets called to create the town's shop
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
                Console.WriteLine($"\n{itemNumber}. {kvp.Key}: {kvp.Value} left");
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