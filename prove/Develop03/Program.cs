using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string csvFile = Path.Combine(AppContext.BaseDirectory, "bom.csv");
        if (!File.Exists(csvFile))
        {
            Console.WriteLine($"Could not find CSV file at: {csvFile}");
            return;
        }

        List<Scripture> scriptures = ScriptureLoader.LoadFromCsv(csvFile);
        if (scriptures.Count == 0)
        {
            Console.WriteLine("No scriptures were loaded from the CSV file.");
            return;
        }

        Random rand = new Random();
        Scripture selected = scriptures[rand.Next(scriptures.Count)];

        while (true)
        {
            Console.Clear();
            Console.WriteLine(selected.GetDisplayText());

            if (selected.IsFullyHidden())
            {
                Console.WriteLine();
                Console.WriteLine("All words are hidden. Great job!");
                break;
            }

            Console.WriteLine();
            Console.WriteLine("Press ENTER to hide a word, or type 'quit' to exit.");
            string input = Console.ReadLine()?.Trim().ToLower() ?? string.Empty;
            if (input == "quit")
                break;

            selected.HideRandomWords(1);
        }

        Console.WriteLine("Done. Press Enter to exit.");
        Console.ReadLine();
    }
}