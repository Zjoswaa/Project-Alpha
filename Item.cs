public class Item {
    public int ID;
    public string Name;
    public string Description;

    public Item(int ID, string Name, string Description) {
        this.ID = ID;
        this.Name = Name;
        this.Description = Description;
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
