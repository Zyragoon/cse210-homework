

namespace Develop04
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Mindfulness Program\n");
                Console.WriteLine("1. Breathing Activity");
                Console.WriteLine("2. Reflection Activity");
                Console.WriteLine("3. Listing Activity");
                Console.WriteLine("4. Quit");
                Console.Write("Select an option (1-4): ");

                string input = Console.ReadLine();
                Activity activity = null;

                switch (input)
                {
                    case "1":
                        activity = new BreathingActivity();
                        break;
                    case "2":
                        activity = new ReflectionActivity();
                        break;
                    case "3":
                        activity = new ListingActivity();
                        break;
                    case "4":
                        Console.WriteLine("Goodbye. Stay mindful!");
                        return;
                    default:
                        Console.WriteLine("Please enter a valid selection.");
                        Thread.Sleep(1000);
                        continue;
                }

                activity.Run();
                Console.WriteLine("Press Enter to return to the main menu...");
                Console.ReadLine();
            }
        }
    }
}
