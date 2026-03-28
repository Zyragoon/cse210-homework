using System;

static class Menu
{
    public static void ShowMainMenu(int currentScore)
    {
        Console.WriteLine("\n=== Eternal Quest Menu ===");
        Console.WriteLine($"Total score: {currentScore}");
        Console.WriteLine("1. Create a new goal");
        Console.WriteLine("2. Show goals");
        Console.WriteLine("3. Record an event");
        Console.WriteLine("4. Save goals");
        Console.WriteLine("5. Load goals");
        Console.WriteLine("6. Show score and rank");
        Console.WriteLine("7. Quit");
        Console.Write("Choose an option: ");
    }

    public static void ShowGoalTypes()
    {
        Console.WriteLine("\nPick goal type:");
        Console.WriteLine("1. Simple goal (complete once for fixed points)");
        Console.WriteLine("2. Eternal goal (repeatable with points each time)");
        Console.WriteLine("3. Checklist goal (complete a number of times and earn a bonus)");
        Console.Write("Goal type: ");
    }
}
