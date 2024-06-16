using System;

class Program
{
    static void Main(string[] args)
    {

                
                Console.WriteLine("Welcome to Guess My Number!");

                
                Random random = new Random();
                int magicNumber = random.Next(1, 101);

                
                int numGuesses = 0;

               
                bool keepPlaying = true;
                while (keepPlaying)
                {

                    Console.Write("What is your guess? ");
                    int guess = Convert.ToInt32(Console.ReadLine());
                    numGuesses++; 

                    
                    if (guess == magicNumber)
                    {
                        Console.WriteLine("You guessed it in {0} tries!", numGuesses);
                        break; 
                    }
                    else if (guess < magicNumber)
                    {
                        Console.WriteLine("Higher!");
                    }
                    else
                    {
                        Console.WriteLine("Lower!");
                    }

                    
                    Console.Write("Do you want to play again (yes/no)? ");
                    string playAgain = Console.ReadLine().ToLower(); 
                    keepPlaying = playAgain == "yes"; 
                }

                Console.WriteLine("Thanks for playing!");
            }
}