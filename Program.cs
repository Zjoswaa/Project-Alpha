class Program
{
    public static void Main(String[] args)
    {
        Location? currentLocation = World.LocationByID(1);
        World.WorldMap();

        while (true)
        {
            if (currentLocation != null)
            {
                Console.WriteLine($"\nYou are currently at {currentLocation.Name}.");
                Console.WriteLine($"Description: {currentLocation.Description}");
                Console.WriteLine("Directions:\n" + currentLocation.Compass());
            }

            Console.WriteLine("Enter a direction to move (N/E/S/W):");
            string direction = Console.ReadLine().ToUpper();
            Location? newLocation = currentLocation?.GetLocationAt(direction);

            if (newLocation != null)
            {
                currentLocation = newLocation;
            }

            else
            {
                Console.WriteLine("The terrain blocks off this path. You can't take this route.");
            }
        }
    }
}
