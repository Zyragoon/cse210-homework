using System;

class Program
{
    static void Main(string[] args)
    {
        Random randomGenerator = new Random();
        int guessCount = 0;
        bool playAgain = true;
        
        while (playAgain)
        {
            int magicNumber = randomGenerator.Next(1, 11);
            int guess = -1;
            
            while (guess != magicNumber)
            {
                Console.Write("What is your guess? ");
                guess = int.Parse(Console.ReadLine());
                guessCount++;
                
                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                }
            }
            
            Console.WriteLine("would you like to play again? (y/n)");
            string playAgainInput = Console.ReadLine();
            if (playAgainInput.ToLower() != "y")
            {
                playAgain = false;
            }
        }
        
        Console.WriteLine("Thanks for playing!");
        Console.WriteLine($"Your guess count is {guessCount}");
    }
}    