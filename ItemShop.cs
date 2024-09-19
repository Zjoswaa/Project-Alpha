class ItemShop
{
    public Dictionary<Item, int> Stock;
    private Player Player;

    public ItemShop(Player player)
    {
        Stock = new Dictionary<Item, int>{};
        Player = player;
    }

    public void StoreDisplay()
    {
        Console.WriteLine("Welcome, take a look around.\n");

        foreach (KeyValuePair<Item, int> kvp in Stock)
        {
            Console.WriteLine($"\n{kvp.Key}: {kvp.Value}");
        }
    }

    public void Purchase(Item item, int price)
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
            Player.Coins -= price;
            Console.WriteLine($"{item} has been added to your inventory.");
        }
        else
        {
            Console.WriteLine("You don't have enough funds.");
        }
    }

    public void FillAlchemistCatalog()
    {
        Consumable potion = new Consumable(5, "Health Potion", "A refreshing potion that restores your health", 5);
        Consumable bigPotion = new Consumable(6, "Greater Health Potion", "An improved potion that restores your health", 10);

        Stock = new Dictionary<Item, int>(){ {potion, 2}, {bigPotion, 2} };
    }

    public void FillTownCatalog()
    {
        Consumable potion = new Consumable(5, "Health Potion", "A refreshing potion that restores your health", 5);
        Consumable bigPotion = new Consumable(6, "Greater Health Potion", "An improved potion that restores your health", 10);

        Stock = new Dictionary<Item, int>(){ {potion, 2}, {bigPotion, 2} };
    }

    public void ShopUI()
    {
        StoreDisplay();
        
    }
}