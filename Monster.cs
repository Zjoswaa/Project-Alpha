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

    public bool Attack(Player player)
    {
        if (this.CurrentHitPoints <= 0) {
            return false;
        }

        int damage = 0;
        Random rand = new Random();
        if (player.IsDefending)
        {
            damage = Math.Max(0, rand.Next(0, this.MaximumDamage) - rand.Next(0, player.ActiveWeapon.Defence));
            player.IsDefending = false;
        }
        else 
        { 
            damage = rand.Next(0, this.MaximumDamage); 
        }
        Console.WriteLine($"{this.Name} attacks {player.Name} for {damage} damage!");
        player.HitPoints -= damage;
        if (player.HitPoints < 0)
        {
            player.HitPoints = 0;
            Console.WriteLine($"{this.Name} has defeated {player.Name}.");
            return false;
        }
        //Console.WriteLine($"{player.Name} has {player.HitPoints} HP left.");
        return true;
    }
}
