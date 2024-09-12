public class Weapon : Item {
    public int MaxDamage;

    public Weapon(int ID, string Name, string Description, int MaxDamage) : base(ID, Name, Description) {
        this.MaxDamage = MaxDamage;
    }

    override public string? ToString() {
        return $"{this.Name} \x1B[91m(Max damage: {this.MaxDamage})\u001b[0m \x1B[90m{this.Description}\x1B[0m";
    }

    override public bool Equals(object? obj) {
        if (obj == null || !(obj is Weapon)) {
            return false;
        }
        return this.ID == ((Weapon)obj).ID;
    }
}
