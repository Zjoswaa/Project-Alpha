public class Weapon : Item {
    public int MaxDamage;

    public Weapon(int ID, string Name, string Description, int MaxDamage) : base(ID, Name, Description) {
        this.MaxDamage = MaxDamage;
    }

    override public string? ToString() {
        return $"\x1B[95m{this.Name}\x1B[0m \x1B[91m(Max damage {this.MaxDamage})\u001b[0m \x1B[90m{this.Description}\x1B[0m";
    }
}
