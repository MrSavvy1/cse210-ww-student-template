using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JournalApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Journal journal = new Journal();
            bool running = true;

            // Menu loop
            while (running)
            {
                Console.Clear();
                Console.WriteLine("Journal Menu");
                Console.WriteLine("1. Write a new entry");
                Console.WriteLine("2. Display journal entries");
                Console.WriteLine("3. Save journal to file");
                Console.WriteLine("4. Load journal from file");
                Console.WriteLine("5. Quit");

                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        journal.WriteEntry();
                        break;
                    case "2":
                        journal.DisplayEntries();
                        break;
                    case "3":
                        Console.Write("Enter filename to save: ");
                        string saveFileName = Console.ReadLine();
                        journal.SaveToFile(saveFileName);
                        break;
                    case "4":
                        Console.Write("Enter filename to load: ");
                        string loadFileName = Console.ReadLine();
                        journal.LoadFromFile(loadFileName);
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
            }
        }
    }

    // Journal class that encapsulates the responsibilities of a journal
    class Journal
    {
        private List<Entry> entries = new List<Entry>(); // List to store journal entries
        private List<string> prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
        }; // List of prompts

        // Method to write a new entry
        public void WriteEntry()
        {
            Random rand = new Random();
            string prompt = prompts[rand.Next(prompts.Count)];
            Console.WriteLine(prompt);

            string response = Console.ReadLine();
            DateTime date = DateTime.Now;

            Entry newEntry = new Entry(prompt, response, date);
            entries.Add(newEntry);

            Console.WriteLine("Entry added.");
        }

        // Method to display all entries
        public void DisplayEntries()
        {
            foreach (var entry in entries)
            {
                Console.WriteLine(entry);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        // Method to save journal to a file
        public void SaveToFile(string fileName)
        {
            using (StreamWriter outputFile = new StreamWriter(fileName))
            {
                foreach (var entry in entries)
                {
                    outputFile.WriteLine(entry.ToFileString());
                }
            }
            Console.WriteLine("Journal saved.");
        }

        // Method to load journal from a file
        public void LoadFromFile(string fileName)
        {
            entries.Clear();
            string[] lines = File.ReadAllLines(fileName);

            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                string prompt = parts[0];
                string response = parts[1];
                DateTime date = DateTime.Parse(parts[2]);

                Entry entry = new Entry(prompt, response, date);
                entries.Add(entry);
            }
            Console.WriteLine("Journal loaded.");
        }
    }

    // Entry class that encapsulates the responsibilities of an entry
    class Entry
    {
        public string Prompt { get; private set; } // Prompt for the entry
        public string Response { get; private set; } // Response to the prompt
        public DateTime Date { get; private set; } // Date of the entry

        public Entry(string prompt, string response, DateTime date)
        {
            Prompt = prompt;
            Response = response;
            Date = date;
        }

        // Override ToString method to display entry details
        public override string ToString()
        {
            return $"{Date.ToShortDateString()} - {Prompt}: {Response}";
        }

        // Method to format entry for file saving
        public string ToFileString()
        {
            return $"{Prompt}|{Response}|{Date}";
        }
    }
}
