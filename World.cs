public class World
{
    public const int LOCATION_ID_HOME = 1;
    public const int LOCATION_ID_TOWN_SQUARE = 2;
    public const int LOCATION_ID_GUARD_POST = 3;
    public const int LOCATION_ID_ALCHEMIST_HUT = 4;
    public const int LOCATION_ID_ALCHEMISTS_GARDEN = 5;
    public const int LOCATION_ID_FARMHOUSE = 6;
    public const int LOCATION_ID_FARM_FIELD = 7;
    public const int LOCATION_ID_BRIDGE = 8;
    public const int LOCATION_ID_SPIDER_FIELD = 9;

    static World()
    {
        PopulateLocations();
    }

    public static void PopulateLocations()
    {
        Location loc1Start = new Location(LOCATION_ID_LOC1, "Start");
        Location loc2      = new Location(LOCATION_ID_LOC2, "Empty location");
        Location loc3      = new Location(LOCATION_ID_LOC3, "Empty location");
        Location loc4      = new Location(LOCATION_ID_LOC4, "Empty location");
        Location loc5      = new Location(LOCATION_ID_LOC5, "Empty location");
        Location loc6Goal  = new Location(LOCATION_ID_LOC6, "Goal");
    }
}