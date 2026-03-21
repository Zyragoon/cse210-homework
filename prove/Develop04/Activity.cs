
using System.Diagnostics;


namespace Develop04
{
    internal abstract class Activity
    {
        protected string Name { get; }
        protected string Description { get; }
        protected int DurationSeconds { get; private set; }

        protected Activity(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void Run()
        {
            ShowStart();
            Execute();
            ShowEnd();
        }

        private void RequestDuration()
        {
            while (true)
            {
                Console.Write("Enter duration in seconds: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int seconds) && seconds > 0)
                {
                    DurationSeconds = seconds;
                    break;
                }
                Console.WriteLine("Please enter a positive integer value.");
            }
        }

        private void ShowStart()
        {
            Console.Clear();
            Console.WriteLine($"--- {Name} Activity ---");
            Console.WriteLine(Description);
            Console.WriteLine();
            RequestDuration();
            Console.WriteLine("\nGet ready...");
            ShowSpinner(3);
            Console.WriteLine();
        }

        private void ShowEnd()
        {
            Console.WriteLine();
            Console.WriteLine("Well done!");
            Console.WriteLine($"You have completed {DurationSeconds} seconds of the {Name} Activity.");
            ShowSpinner(3);
            Console.WriteLine();
            Console.WriteLine();
        }

        protected void ShowSpinner(int seconds)
        {
            string[] seq = new string[] { "|", "/", "-", "\\" };
            Stopwatch timer = Stopwatch.StartNew();
            int idx = 0;
            while (timer.Elapsed.TotalSeconds < seconds)
            {
                Console.Write(seq[idx]);
                Thread.Sleep(250);
                Console.Write('\b');
                idx = (idx + 1) % seq.Length;
            }
            Console.Write(' ');
            Console.Write('\b');
        }

        protected void ShowCountdown(int seconds)
        {
            for (int i = seconds; i >= 1; i--)
            {
                Console.Write(i);
                Thread.Sleep(1000);
                Console.Write("\b \b");
            }
        }

        protected abstract void Execute();
    }
}