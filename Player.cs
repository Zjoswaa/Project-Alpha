public class Player {
    public string Name { get; set; }
    public string ClassName { get; set; }
    public int HitPoints { get; set; }
    public int MaxHitPoints { get; }
    public int Strength { get; set; }
    public int Agility { get; set;}
    public int Intelligence { get; set;}
    public int Charisma { get; set; }

    public Quest CurrentQuest { get; set; }
    public List<Item> Items { get; set; } = new();
    
    public Player(string Name, string ClassName, int HitPoints, int Strength, int Agility, int Intelligence, int Charisma) {
        this.Name = Name;
        this.ClassName = ClassName;
        this.HitPoints = HitPoints;
        this.MaxHitPoints = HitPoints;
        this.Strength = Strength;
        this.Agility = Agility;
        this.Intelligence = Intelligence;
        this.Charisma = Charisma;

        this.CurrentQuest = null;
    }

    public void SetQuest(Quest Quest) {
        this.CurrentQuest = Quest;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Started Quest");
        Console.ResetColor();
        Console.Write("Name: ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(this.CurrentQuest.Name);
        Console.ResetColor();
        Console.Write("Description: ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(this.CurrentQuest.Description);
        Console.ResetColor();
    }
}
