public class Weapon : Item {
    public int MaxDamage;

    public Weapon(int ID, string Name, string Description, int MaxDamage) : base(ID, Name, Description) {
        this.MaxDamage = MaxDamage;
    }

    override public string? ToString() {
        return $"\x1B[95m{this.Name}\x1B[0m (Max damage {this.MaxDamage}): {this.Description}";
    }
}
