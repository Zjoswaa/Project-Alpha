public class World
{
    public static readonly List<Location> Locations = new List<Location>();
    public const int HOME = 1;
    public const int TOWN_SQUARE = 2;
    public const int MILITARY_CAMP = 3;
    public const int ALCHEMIST_HUT = 4;
    public const int TORTURE_CHAMBERS = 5;
    public const int FARMER_MEADOWS = 6;
    public const int FARMHOUSE = 7;
    public const int BRIDGE = 8;
    public const int KINGS_CASTLE= 9;

    static World()
    {
        PopulateLocations();
    }
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
        Location home = new Location(HOME, "Start");
        Location townSquare = new Location(TOWN_SQUARE, "Empty location");
        Location militarycamp = new Location(MILITARY_CAMP, "Empty location");
        Location alchemisthut = new Location(ALCHEMIST_HUT, "Empty location");
        Location torturechambers = new Location(TORTURE_CHAMBERS, "Empty location");
        Location farmermedows = new Location(FARMER_MEADOWS, "Empty location");
        Location farmhouse = new Location(FARMHOUSE, "Empty location");
        Location bridge = new Location(BRIDGE, "Empty location");
        Location kings_castle = new Location(KINGS_CASTLE, "Empty location");


        home.LocationToNorth = townSquare;
        townSquare.LocationToSouth = home;
        townSquare.LocationToNorth = alchemisthut;
        alchemisthut.LocationToSouth = townSquare;
        alchemisthut.LocationToEast = bridge;
        bridge.LocationToWest = alchemisthut;
        bridge.LocationToNorth = kings_castle;
        kings_castle.LocationToSouth = bridge;
        townSquare.LocationToEast = militarycamp;
        militarycamp.LocationToWest = townSquare;
        militarycamp.LocationToNorth = bridge;
        bridge.LocationToSouth = militarycamp;
        militarycamp.LocationToEast = torturechambers;
        townSquare.LocationToWest = farmermedows;
        farmermedows.LocationToEast = townSquare;
        farmermedows.LocationToWest = farmhouse;
        farmhouse.LocationToEast = farmermedows;

        Locations.Add(home);
        Locations.Add(townSquare);
        Locations.Add(militarycamp);
        Locations.Add(alchemisthut);
        Locations.Add(bridge);
        Locations.Add(kings_castle);
        Locations.Add(torturechambers);
        Locations.Add(farmhouse);
        Locations.Add(farmermedows);
    }

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
}