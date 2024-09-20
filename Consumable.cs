public class Consumable : Item {
    public int Restoration;

    public Consumable(int ID, string Name, string Description, int Restoration) : base(ID, Name, Description, true) {
        this.Restoration = Restoration;
    }

    override public string? ToString() {
        return $"{this.Name} \x1B[32m(Max heal: {this.Restoration})\u001b[0m \x1B[90m{this.Description}\x1B[0m";
    }

    override public bool Equals(object? obj) {
        if (obj == null || !(obj is Consumable)) {
            return false;
        }
        return this.ID == ((Consumable)obj).ID;
    }
}
