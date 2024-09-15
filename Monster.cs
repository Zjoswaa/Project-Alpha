public class Monster
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int CurrentHitPoints { get; set; }
    public int MaximumHitPoints { get; set; }
    public int MaximumDamage {  get; set; }

    public Monster(int ID, string Name, int CurrentHitPoints, int MaximumHitPoints, int MaximumDamage)
    {
        this.ID = ID;
        this.Name = Name;
        this.CurrentHitPoints = CurrentHitPoints;
        this.MaximumHitPoints = MaximumHitPoints;
        this.MaximumDamage = MaximumDamage;
    }

    public void Attack(Player player)
    {
        Random rand = new Random();
        int damage = rand.Next(0, this.MaximumDamage);
        Console.WriteLine($"{this.Name} attacks {player.Name} for {damage} damage!");
        Console.WriteLine($"{player.HitPoints}");
    }
}

