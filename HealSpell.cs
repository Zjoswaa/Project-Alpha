public class HealSpell : Spell {
    public int Heal;

    public HealSpell(int ID, string Name, string Description, int Cooldown, int Heal) : base(ID, Name, Description, Cooldown) {
        this.Heal = Heal;
    }

    override public string? ToString() {
        return $"\x1B[1m\x1B[33m{this.Name}\x1B[0m \x1B[90m{this.Description}\x1B[0m \x1B[32m(Heal: {this.Heal})\x1B[0m";
    }

    override public bool Equals(object? obj) {
        if (obj == null || !(obj is HealSpell)) {
            return false;
        }
        return this.ID == ((HealSpell)obj).ID;
    }
}
