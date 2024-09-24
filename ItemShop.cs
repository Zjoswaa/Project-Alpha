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
        Consumable potion = new Consumable(5, "Health Potion", "A refreshing potion that restores your health.", 5);
        Consumable bigPotion = new Consumable(6, "Greater Health Potion", "An improved potion that restores your health.", 10);

        if (Stock is null)
        {
            Stock = new Dictionary<Item, int>(){ {potion, 2}, {bigPotion, 2} };
        }

        Console.Clear();
        Console.WriteLine("Welcome, take a look around.");

        bool k = true;
        while (k)
        {
            int itemNumber = 0;
            foreach (KeyValuePair<Item, int> kvp in Stock)
            {
                itemNumber += 1;
                Console.WriteLine($"\n[{itemNumber}] {kvp.Key}: {kvp.Value} left");
            }

            Console.WriteLine("\n\x1b[36mPress enter to exit, type any number to purchase that item.\x1b[0m");

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
                case "":
                case null:
                    k = false;
                    break;
            }
        }
    }

    //Method that gets called to create the town's shop
    public void TownCatalog()
    {
        Weapon greatSword = new(9, "Greatsword", "A heavy, steel blade built to cut through armor with raw power.", 15, 7);
        Weapon quickfireBow = new(10, "Quickfire Bow", "A lightweight bow designed for rapid firing.", 18, 4);
        Weapon noviceWand = new(11, "Novice Wand", "A simple yet sturdy wand, designed for novice spellcasters to harness their first magical energies.", 20, 5);
        Weapon steelDagger = new(12, "Steel Dagger", "A sharp, compact dagger forged from durable steel, ideal for quick strikes and stealthy maneuvers.", 16, 1);

        if (Stock is null)
        {
            Stock = new Dictionary<Item, int>(){ {greatSword, 1}, {quickfireBow, 1}, {noviceWand, 1}, {steelDagger, 1} };
        }

        Console.Clear();
        Console.WriteLine("Welcome, take a look around.");

        bool k = true;
        while (k)
        {
            int itemNumber = 0;
            foreach (KeyValuePair<Item, int> kvp in Stock)
            {
                itemNumber += 1;
                Console.WriteLine($"\n[{itemNumber}] {kvp.Key}: {kvp.Value} left");
            }

            Console.WriteLine("\n\x1b[36mPress enter to exit, type any number to purchase that item.\x1b[0m");

            string userPurchase = Console.ReadLine().ToUpper();

            switch (userPurchase)
            {
                case "1":
                    PurchaseItem(greatSword, 5);
                    break;
                case "2":
                    PurchaseItem(quickfireBow, 5);
                    break;
                case "3":
                    PurchaseItem(noviceWand, 5);
                    break;
                case "4":
                    PurchaseItem(steelDagger, 5);
                    break;
                case "":
                case null:
                    k = false;
                    break;
            }
        }
    }
}
