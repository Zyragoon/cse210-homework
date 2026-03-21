using System;
using System.Diagnostics;

namespace Develop04
{
    internal class BreathingActivity : Activity
    {
        public BreathingActivity() : base(
            "Breathing",
            "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
        {
        }

        protected override void Execute()
        {
            Stopwatch timer = Stopwatch.StartNew();

            while (timer.Elapsed.TotalSeconds < DurationSeconds)
            {
                Console.Write("Breathe in...");
                ShowCountdown(4);
                Console.WriteLine();

                if (timer.Elapsed.TotalSeconds >= DurationSeconds) break;

                Console.Write("Breathe out...");
                ShowCountdown(4);
                Console.WriteLine();
            }

            timer.Stop();
        }
    }
}