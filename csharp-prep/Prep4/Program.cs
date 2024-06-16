using System;
using System.Collections.Generic;

class NumberListProcessor
{
    static void Main(string[] args)
    {
        
        List<int> numbers = new List<int>();

      
        int num;
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");
        do
        {
            Console.Write("Enter number: ");
            num = Convert.ToInt32(Console.ReadLine());
            if (num != 0)
            {
                numbers.Add(num);
            }
        } while (num != 0);

        
        int sum = numbers.Sum();

        double average;
        if (numbers.Count > 0)
        {
            average = (double)sum / numbers.Count;
        }
        else
        {
            average = double.NaN;   }


        int max = numbers.Max();


        Console.WriteLine($"\nThe sum is: {sum}");
        Console.WriteLine($"The average is: {average:F4}"); /
        Console.WriteLine($"The largest number is: {max}");

        int smallestPositive = numbers.Where(n => n > 0).Min();

        Console.WriteLine($"The smallest positive number is: {smallestPositive}");

        numbers.Sort();

        Console.WriteLine("\nThe sorted list is:");
        foreach (int number in numbers)
        {
            Console.WriteLine(number);
        }
    }
}
