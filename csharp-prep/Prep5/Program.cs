using System;

class SimpleFunctions
{
    static void Main(string[] args)
    {
        
        DisplayWelcome();

        
        string userName = PromptUserName();


        int favoriteNumber = PromptUserNumber();


        int squaredNumber = SquareNumber(favoriteNumber);


        DisplayResult(userName, squaredNumber);
    }

    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the program!");
    }

    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        string name = Console.ReadLine();
        return name;
    }

    static int PromptUserNumber()
    {
        Console.Write("Please enter your favorite number: ");
        int number = Convert.ToInt32(Console.ReadLine());
        return number;
    }

    static int SquareNumber(int number)
    {
        return number * number; 
    }

    static void DisplayResult(string name, int squaredNumber)
    {
        Console.WriteLine($"{name}, the square of your number is {squaredNumber}");
    }
}
