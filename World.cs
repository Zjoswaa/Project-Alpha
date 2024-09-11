public class World
{
    public static readonly List<Location> Locations = new List<Location>();

    static World()
    {
        PopulateLocations();
    }

    // Shows List of all locations
    public static void DisplayLocationsMenu()
    {
        System.Console.WriteLine("Available Locations:");
        foreach(Location location in Locations)
        {
            System.Console.WriteLine($"{location.ID}. {location.Name}");
        }
    }

    public static void PopulateLocations()
    {
        // Creates all locations
        Location forest = new Location(1, "Forest", "PLACEHOLDER");
        Location townSquare = new Location(2, "Town Square", "PLACEHOLDER");
        Location militarycamp = new Location(3, "Military Camp", "PLACEHOLDER");
        Location alchemisthut = new Location(4, "Alchemist's Hut", "PLACEHOLDER");
        Location torturechambers = new Location(5, "Torture Chambers", "PLACEHOLDER");
        Location farmermedows = new Location(6, "Farmer's Meadows", "PLACEHOLDER");
        Location farmhouse = new Location(7, "Farmhouse", "PLACEHOLDER");
        Location kingsPass = new Location(8, "King's Pass", "PLACEHOLDER");
        Location kings_castle = new Location(9, "Royal Palace", "PLACEHOLDER");

        // Links all locations together
        forest.LocationToNorth = townSquare;
        townSquare.LocationToSouth = forest;
        townSquare.LocationToNorth = alchemisthut;
        alchemisthut.LocationToSouth = townSquare;
        alchemisthut.LocationToEast = kingsPass;
        kingsPass.LocationToWest = alchemisthut;
        kingsPass.LocationToNorth = kings_castle;
        kings_castle.LocationToSouth = kingsPass;
        townSquare.LocationToEast = militarycamp;
        militarycamp.LocationToWest = townSquare;
        militarycamp.LocationToNorth = kingsPass;
        kingsPass.LocationToSouth = militarycamp;
        militarycamp.LocationToEast = torturechambers;
        torturechambers.LocationToWest = militarycamp;
        townSquare.LocationToWest = farmermedows;
        farmermedows.LocationToEast = townSquare;
        farmermedows.LocationToWest = farmhouse;
        farmhouse.LocationToEast = farmermedows;

        // Adds the locations to a list for display
        Locations.Add(forest);
        Locations.Add(townSquare);
        Locations.Add(militarycamp);
        Locations.Add(alchemisthut);
        Locations.Add(kingsPass);
        Locations.Add(kings_castle);
        Locations.Add(torturechambers);
        Locations.Add(farmermedows);
        Locations.Add(farmhouse);
    }

    // Fetches a location by ID
    public static Location LocationByID(int id)
    {
        foreach (Location location in Locations)
        {
            if (location.ID == id)
            {
                return location;
            }
        }

        return null;
    }

    public static void WorldMap()
    {
        string mapLayout = @"
                                                                              +----------------------+
                                                                              |     Royal Palace     |
                                                                              +----------------------+
                                                                                         |
                                                                                         |
                                                                                         |
                                                                                         |
                                                    +----------------------+  +----------------------+
                                                    |   Alchemist's Hut    |--|      King's Pass     |
                                                    +----------------------+  +----------------------+
                                                               |                         |
                                                               |                         |
                                                               |                         |
+----------------------+  +----------------------+  +----------------------+  +----------------------+  +----------------------+
|      Farmhouse       |--|   Farmer's Meadows   |--|     Town Square      |--|     Military Camp    |--|   Torture Chambers   |
+----------------------+  +----------------------+  +----------------------+  +----------------------+  +----------------------+
                                                               |
                                                               |
                                                               |
                                                    +----------------------+
                                                    |        Forest        |
                                                    +----------------------+
";

        Console.WriteLine(mapLayout);
    }
}