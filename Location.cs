public class Location
{
    public int ID;
    public string Name;
    public string Description;
    public Location? LocationToNorth;
    public Location? LocationToEast;
    public Location? LocationToSouth;
    public Location? LocationToWest;

    public Location(int ID, string Name, string Description)
    {
        this.ID = ID;
        this.Name = Name;
        this.Description = Description;
    }

    public string Compass()
    {
        string s = "From here you can go:\n";
        if (LocationToNorth != null)
        {
            s += "    P\n    A\n";
        }
        if (LocationToWest != null)
        {
            s += "  VFT";
        }
        else
        {
            s += "    |";
        }
        if (LocationToEast != null)
        {
            s += "GBS";
        }
        s += "\n";
        if (LocationToSouth != null)
        {
            s += "    H\n    ";
        }
        return s;
    }

    public Location? GetLocationAt(string location)
    {
        if (location == "N") return LocationToNorth;
        if (location == "E") return LocationToEast;
        if (location == "S") return LocationToSouth;
        if (location == "W") return LocationToWest;
        return null;
    }
}