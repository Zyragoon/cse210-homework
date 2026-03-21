using System;
using System.Collections.Generic;

namespace Develop04
{
    internal class ListingActivity : Activity
    {
        private static readonly string[] Prompts = new string[]
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        public ListingActivity() : base(
            "Listing",
            "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
        {
        }

        protected override void Execute()
        {
            var rand = new Random();
            string prompt = Prompts[rand.Next(Prompts.Length)];
            Console.WriteLine(prompt);
            Console.WriteLine("You have a few seconds to think about this.");
            ShowCountdown(5);
            Console.WriteLine();
            Console.WriteLine("Start listing items (press Enter after each). Press Enter on an empty line to finish early if you want.");

            List<string> items = new List<string>();
            DateTime endTime = DateTime.Now.AddSeconds(DurationSeconds);

            while (DateTime.Now < endTime)
            {
                Console.Write("- ");
                string entry = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(entry))
                    break;

                items.Add(entry.Trim());
            }

            Console.WriteLine($"\nYou listed {items.Count} items.");
        }
    }
}