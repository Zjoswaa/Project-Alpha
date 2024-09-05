public class Player {
    public string name { get; set; }
    public int hitPoints { get; set; }
    public int strength { get; set; }
    public int agility { get; set;}
    public int intelligence { get; set;}
    public int charisma { get; set; }
    
    public Player(string name, int strength, int agility, int intelligence, int charisma) {
        this.hitPoints = 100;
        this.name = name;
        this.strength = strength;
        this.agility = agility;
        this.intelligence = intelligence;
        this.charisma = charisma;
    }
}
