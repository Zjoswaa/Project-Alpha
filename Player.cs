public class Player {
    public string Name { get; set; }
    public string ClassName { get; set; }
    public int HitPoints { get; set; }
    public int MaxHitPoints { get; }
    public int Strength { get; set; }
    public int Agility { get; set;}
    public int Intelligence { get; set;}
    public int Charisma { get; set; }

    public List<Quest> KnownQuests { get; set; } = new();
    public List<Item> Items { get; set; } = new();
    public Weapon ActiveWeapon { get; set; }
    
    public Player(string Name, string ClassName, int HitPoints, int Strength, int Agility, int Intelligence, int Charisma) {
        this.Name = Name;
        this.ClassName = ClassName;
        this.HitPoints = HitPoints;
        this.MaxHitPoints = HitPoints;
        this.Strength = Strength;
        this.Agility = Agility;
        this.Intelligence = Intelligence;
        this.Charisma = Charisma;
    }

    public void AddQuest(Quest Quest) {
        this.KnownQuests.Add(Quest);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Added Quest");
        Console.ResetColor();
        Console.Write("Name: ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(Quest.Name);
        Console.ResetColor();
        Console.Write("Description: ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(Quest.Description);
        Console.ResetColor();
    }
}
