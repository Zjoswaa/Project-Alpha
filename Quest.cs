public class Quest {
    public int ID;
    public string Name;
    public string Description;
    public bool Completed;
    
    public Quest(int ID, string Name, string Description) {
        this.ID = ID;
        this.Name = Name;
        this.Description = Description;
        this.Completed = false;
    }

    public Quest(int ID, string Name, string Description, bool Completed) {
        this.ID = ID;
        this.Name = Name;
        this.Description = Description;
        this.Completed = Completed;
    }

    override public string? ToString() {
        if (this.Completed) {
            return $"\x1B[92m{this.Name} \x1B[90m{this.Description}\x1B[0m";
        }
        return $"\x1B[91m{this.Name} \x1B[90m{this.Description}\x1B[0m";
    }

    override public bool Equals(object? obj) {
        if (obj == null || !(obj is Quest)) {
            return false;
        }
        return this.ID == ((Quest)obj).ID;
    }
}
