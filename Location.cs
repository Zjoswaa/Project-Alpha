public class Location
{
    public int ID;
    public string Name;
    public string Description;
    public Location? LocationToNorth;
    public Location? LocationToEast;
    public Location? LocationToSouth;
    public Location? LocationToWest;
    public bool IsUnlocked;

    public Location(int ID, string Name, string Description, bool IsUnlocked)
    {
        this.ID = ID;
        this.Name = Name;
        this.Description = Description;
        this.IsUnlocked = IsUnlocked;
    }

    // Dynamic compass that changes based on possible routes
    public string Compass()
    {
        string direction = "From here you can go:\n";
        string north = LocationToNorth != null ? "[N]" : "   ";
        string east = LocationToEast != null ? "[E]" : "   ";
        string south = LocationToSouth != null ? "[S]" : "   ";
        string west = LocationToWest != null ? "[W]" : "   ";

        string northLine = $"     {north}     ";
        string middleLine = $"  {west}   {east}  ";
        string southLine = $"     {south}     ";
        
        if (LocationToNorth == null && LocationToSouth == null)
        {
            middleLine = $"{west}     {east}";
        }

        return $"{direction}\n{northLine}\n{middleLine}\n{southLine}";
    }
    

    // Based on user input, returns the location in that direction
    public Location? GetLocationAt(string location)
    {
        if (location == "N") return LocationToNorth;
        if (location == "E") return LocationToEast;
        if (location == "S") return LocationToSouth;
        if (location == "W") return LocationToWest;
        return null;
    }

    // Use this method to give the player the option to move and check the map.
    // Always use "World.LocationByID(1)" as the parameter when starting the game (unless testing)
    public static void PlayerMovement(Location? currentLocation, Player Player)
    {
        while (true)
        {
            if (currentLocation != null)
            {
                Console.Clear();
                Console.WriteLine($"Current location: \x1B[34m{currentLocation.Name}\x1B[0m");
                Console.WriteLine($"Description: \x1B[90m{currentLocation.Description}\x1B[0m");
                Console.WriteLine(currentLocation.Compass());
            }

            Console.WriteLine();
            Console.WriteLine("Enter a direction to move (N/E/S/W), press enter to exit:");

            string userInput = Console.ReadLine().ToUpper();

            if (userInput == null || userInput == "") {
                return;
            }
            
            if (userInput == "N" || userInput == "E" || userInput == "S" || userInput == "W") {
                Location? newLocation = currentLocation?.GetLocationAt(userInput);
                if (newLocation is not null && !newLocation.IsUnlocked) {
                    Console.Clear();
                    Console.WriteLine("This location is not yet available.");
                    Util.pressAnyKey();
                    return;
                }
                if (newLocation != null) {
                    Player.CurrentLocation = newLocation;
                    //currentLocation = newLocation;
                    break;
                } else {
                    Console.WriteLine("The terrain blocks off this path. You can't take this route.");
                }
            } else {
                Console.WriteLine("Invalid input (N/E/S/W), press enter to exit:");
            }
        }
    }

    // Function needed to compare equality to an Item object
    override public bool Equals(object? obj) {
        if (obj == null || !(obj is Location)) {
            return false;
        }
        return this.ID == ((Location)obj).ID;
    }
}
