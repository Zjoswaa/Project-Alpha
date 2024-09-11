class Program
{
    public static void Main(String[] args)
    {
        Location currentLocation = World.LocationByID(1);
        Location.PlayerMovement(World.LocationByID(3));
    }
}
