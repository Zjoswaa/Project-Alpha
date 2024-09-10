class Program {
    public static void Main(String[] args) {
        while (true)
        {
            Console.WriteLine("Enter a direction to move (N/E/S/W):");
            string direction = Console.ReadLine().ToUpper();
            Location? currentLocation = World.LocationByID(1);

            if (currentLocation != null)
            {
                Console.WriteLine($"Moving {direction} to {newLocation.Name}");
                Console.WriteLine($"Description: {newLocation.Description}");
                Console.WriteLine("Directions:\n" + newLocation.Compass());   
            }

            else
            {
                "You can't go that way."
            }
        }
    }
}
