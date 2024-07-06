using System;

class Program
{
    static void Main(string[] args)
    {
        
                
                Console.WriteLine("Enter your grade percentage: ");
                int grade = Convert.ToInt32(Console.ReadLine());

                
                char letterGrade;
                string sign = "";

                if (grade >= 90)
                {
                    letterGrade = 'A';
                }
                else if (grade >= 80)
                {
                    letterGrade = 'B';
                    sign = (grade % 10 >= 7) ? "+" : ""; 
                }
                else if (grade >= 70)
                {
                    letterGrade = 'C';
                    sign = (grade % 10 >= 7) ? "+" : (grade % 10 < 3) ? "-" : "";
                }
                else if (grade >= 60)
                {
                    letterGrade = 'D';
                }
                else
                {
                    letterGrade = 'F';
                    
                    if (grade == 100)
                    {
                        letterGrade = 'A';
                    }
                }


                bool passed = grade >= 70;

              Console.WriteLine($"Your letter grade is: {letterGrade}{sign}");
                Console.WriteLine(passed ? "Congratulations! You passed the course." : "Keep practicing, you'll get it next time!");
            }
        
}