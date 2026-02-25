using System;
using System.Runtime.ExceptionServices;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your first name? ");
        String  first = Console.ReadLine();

        Console.Write("what is your last name?");
        String last = Console.ReadLine();

        Console.WriteLine($"your name is {last},{first},{last}.");

       
        

    }
}