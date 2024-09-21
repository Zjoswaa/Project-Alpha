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
        Location forest = new Location(1, "Forest", "A shadowed, twisted woodland where sunlight never breaks through the canopy. Whispers haunt the air, and those who enter rarely return unchanged.");
        Location townSquare = new Location(2, "Duskmire Town", "The square hums with quiet activity, its worn cobblestones and weathered fountain whispering of old secrets. The air feels still, yet welcoming.");
        Location militarycamp = new Location(3, "Military Camp", "Rows of weathered tents stand in grim silence, you smell the scent of iron and sweat. Every movement feels watched, every command echoing through the stillness.");
        Location alchemisthut = new Location(4, "Alchemist's Hut", "Strange herbs hang from the rafters, and the air is thick with the scent of potions brewing. Mysterious symbols cover the walls, glowing faintly in the dim light.");
        Location torturechambers = new Location(5, "Torture Chambers", "The cold, damp walls echo with distant screams. Rusted chains dangle from the ceiling, and the air is heavy with the stench of fear and decay.");
        Location farmermedows = new Location(6, "Farmer's Meadows", "Rolling fields stretch under a soft, golden sun. The gentle breeze carries the scent of wildflowers and fresh earth, a place of quiet, simple life.");
        Location farmhouse = new Location(7, "Farmhouse", "A cozy, sunlit home with smoke curling from the chimney. Its wooden walls are weathered but welcoming, surrounded by neat gardens and the distant hum of farm life.");
        Location kingsPass = new Location(8, "King's Pass", "A narrow, winding corridor through jagged mountains, its towering cliffs etched with ancient runes. The path is shrouded in mist, with echoes of history whispering through the stone.");
        Location kings_castle = new Location(9, "Royal Palace", "A majestic edifice of grand halls and opulent chambers, adorned with golden tapestries and shimmering chandeliers. The air is filled with a regal silence, broken only by the soft murmur of courtly intrigue.");

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
    public static Location LocationByID(int id) {
        foreach (Location location in Locations)
        {
            if (location.ID == id)
            {
                return location;
            }
        }

        return null;
    }

    public static void WorldMap(Location CurrentLocation)
    {
        string mapLayout = $@"
                                                                              +----------------------+
                                                                              |     {(CurrentLocation.ID == 9 ? "\x1b[1m\x1b[35mRoyal Palace\x1b[0m" : "Royal Palace")}     |
                                                                              +----------------------+
                                                                                         |
                                                                                         |
                                                                                         |
                                                                                         |
                                                    +----------------------+  +----------------------+
                                                    |   {(CurrentLocation.ID == 4 ? "\x1b[1m\x1b[35mAlchemist's Hut\x1b[0m" : "Alchemist's Hut")}    |--|      {(CurrentLocation.ID == 8 ? "\x1b[1m\x1b[35mKing's Pass\x1b[0m" : "King's Pass")}     |
                                                    +----------------------+  +----------------------+
                                                               |                         |
                                                               |                         |
                                                               |                         |
+----------------------+  +----------------------+  +----------------------+  +----------------------+  +----------------------+
|      {(CurrentLocation.ID == 7 ? "\x1b[1m\x1b[35mFarmhouse\x1b[0m" : "Farmhouse")}       |--|   {(CurrentLocation.ID == 6 ? "\x1b[1m\x1b[35mFarmer's Meadows\x1b[0m" : "Farmer's Meadows")}   |--|    {(CurrentLocation.ID == 2 ? "\x1b[1m\x1b[35mDuskmire Town\x1b[0m" : "Duskmire Town")}     |--|     {(CurrentLocation.ID == 3 ? "\x1b[1m\x1b[35mMilitary Camp\x1b[0m" : "Military Camp")}    |--|   {(CurrentLocation.ID == 5 ? "\x1b[1m\x1b[35mTorture Chambers\x1b[0m" : "Torture Chambers")}   |
+----------------------+  +----------------------+  +----------------------+  +----------------------+  +----------------------+
                                                               |
                                                               |
                                                               |
                                                    +----------------------+
                                                    |        {(CurrentLocation.ID == 1 ? "\x1b[1m\x1b[35mForest\x1b[0m" : "Forest")}        |
                                                    +----------------------+
";

        Console.WriteLine(mapLayout);
    }
}
