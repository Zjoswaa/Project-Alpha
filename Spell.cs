public class Spell {
    public int ID;
    public string Name;
    public string Description;
    public int Cooldown;

    public Spell(int ID, string Name, string Description, int Cooldown) {
        this.ID = ID;
        this.Name = Name;
        this.Description = Description;
        this.Cooldown = Cooldown;
    }

    override public string? ToString() {
        return $"{this.Name} \x1B[90m{this.Description}\x1B[0m";
    }

    // Function needed to compare equality to an Item object
    override public bool Equals(object? obj) {
        if (obj == null || !(obj is Item)) {
            return false;
        }
        return this.ID == ((Item)obj).ID;
    }

    // Function needed to index an Item object as a dictionary key
    public override int GetHashCode() {
        return ID.GetHashCode();
    }
}
