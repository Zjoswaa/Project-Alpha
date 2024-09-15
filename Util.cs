static class Util {
    public static void pressAnyKey() {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
        Console.ResetColor();
    }

    public static void pressAnyKey(string Message) {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write($"{Message}");
        Console.ReadKey();
        Console.Clear();
        Console.ResetColor();
    }
}
