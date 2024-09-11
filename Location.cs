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

    // Dynamic compass that changes based on possible routes
    public string Compass()
    {
        string direction = "From here you can go:\n\n";
        string north = "";
        string east = "";
        string south = "";
        string west = "";

        if (LocationToNorth != null)
        {
            north = $"[N]";
        }
        if (LocationToEast != null)
        {
            east = $"[E]";
        }
        if (LocationToSouth != null)
        {
            south = $"[S]";
        }
        if (LocationToWest != null)
        {
            west = $"[W]";
        }

        return $"{direction}\n     {north}\n  {west}   {east}\n     {south}";
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
    public static void PlayerMovement(Location? currentLocation)
    {
        while (true)
        {
            if (currentLocation != null)
            {
                Console.WriteLine($"\nYou are currently at {currentLocation.Name}.");
                Console.WriteLine($"Description: {currentLocation.Description}");
                Console.WriteLine(currentLocation.Compass());
            }

            Console.WriteLine("Enter a direction to move (N/E/S/W) or M to check the map:");

            string userInput = Console.ReadLine().ToUpper();

            if (userInput == "M")
            {
                World.WorldMap();
            }

            else
            {
                Location? newLocation = currentLocation?.GetLocationAt(userInput);

                if (newLocation != null)
                {
                    currentLocation = newLocation;
                    break;
                }

                else
                {
                    Console.WriteLine("The terrain blocks off this path. You can't take this route.");
                }
            }
        }
    }
}