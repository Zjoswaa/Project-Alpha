public class Quest {
    public int ID;
    public string Name;
    public string Description;
    public string QuestType; // MAIN or SIDE
    public Dictionary<Item, int> Rewards;
    public bool Completed;
    
    public Quest(int ID, string Name, string Description, string QuestType, Dictionary<Item, int> Rewards) {
        this.ID = ID;
        this.Name = Name;
        this.Description = Description;
        this.QuestType = QuestType;
        this.Rewards = Rewards;
        this.Completed = false;
    }

    public Quest(int ID, string Name, string Description, string QuestType, Dictionary<Item, int> Rewards, bool Completed) {
        this.ID = ID;
        this.Name = Name;
        this.Description = Description;
        this.QuestType = QuestType;
        this.Rewards = Rewards;
        this.Completed = Completed;
    }

    override public string? ToString() {
        if (this.QuestType == "MAIN") {
            return $"\x1B[1m\x1B[93m{this.Name}\x1B[0m \x1B[90m{this.Description}\x1B[0m";
        }
        return $"\x1B[1m{this.Name}\x1B[0m \x1B[90m{this.Description}\x1B[0m";
        //if (this.Completed) {
        //    return $"\x1B[92m{this.Name} \x1B[90m{this.Description}\x1B[0m";
        //}
        //return $"\x1B[91m{this.Name} \x1B[90m{this.Description}\x1B[0m";
    }

    override public bool Equals(object? obj) {
        if (obj == null || !(obj is Quest)) {
            return false;
        }
        return this.ID == ((Quest)obj).ID;
    }
}
