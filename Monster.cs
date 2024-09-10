//public class Player
//{
//    public string Name { get; set; }
//    public string ClassName { get; set; }
//    public int HitPoints { get; set; }
//    public int MaxHitPoints { get; }
//    public int Strength { get; set; }
//    public int Agility { get; set; }
//    public int Intelligence { get; set; }
//    public int Charisma { get; set; }

//    public Player(string Name, string ClassName, int HitPoints, int Strength, int Agility, int Intelligence, int Charisma)
//    {
//        this.Name = Name;
//        this.ClassName = ClassName;
//        this.HitPoints = HitPoints;
//        this.MaxHitPoints = HitPoints;
//        this.Strength = Strength;
//        this.Agility = Agility;
//        this.Intelligence = Intelligence;
////      this.Charisma = Charisma;
//    }
//}

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
}