using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Sample scriptures for demonstration
        var scriptures = new List<Scripture>
        {
            new Scripture(new Reference("Proverbs", 3, 5, 6), "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight."),
            new Scripture(new Reference("John", 3, 16), "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.")
        };

        // Randomly select a scripture to display
        Random rand = new Random();
        Scripture currentScripture = scriptures[rand.Next(scriptures.Count)];

        // Main loop
        while (true)
        {
            Console.Clear();
            currentScripture.Display();
            Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit.");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
                break;

            currentScripture.HideRandomWords();
        }
    }
}

// This class represents the Scripture, including its text and reference
class Scripture
{
    private Reference reference; // Stores the reference (book, chapter, verses)
    private List<Word> words; // Stores each word in the scripture

    public Scripture(Reference reference, string text)
    {
        this.reference = reference;
        words = text.Split(' ').Select(wordText => new Word(wordText)).ToList();
    }

    // Display the scripture with the reference and the current state of each word
    public void Display()
    {
        Console.WriteLine(reference);
        Console.WriteLine(string.Join(" ", words.Select(word => word.GetDisplayedText())));
    }

    // Hide a few random words in the scripture
    public void HideRandomWords()
    {
        Random rand = new Random();
        foreach (var word in words)
        {
            if (!word.IsHidden && rand.Next(2) == 0)
            {
                word.Hide();
            }
        }
    }
}

// This class represents the reference of the scripture (e.g., "John 3:16")
class Reference
{
    public string Book { get; private set; }
    public int Chapter { get; private set; }
    public int StartVerse { get; private set; }
    public int EndVerse { get; private set; }

    public Reference(string book, int chapter, int startVerse, int endVerse = -1)
    {
        Book = book;
        Chapter = chapter;
        StartVerse = startVerse;
        EndVerse = endVerse == -1 ? startVerse : endVerse;
    }

    // Return the string representation of the reference
    public override string ToString()
    {
        return EndVerse == StartVerse ? $"{Book} {Chapter}:{StartVerse}" : $"{Book} {Chapter}:{StartVerse}-{EndVerse}";
    }
}

// This class represents each word in the scripture, including its hidden state
class Word
{
    private string text;
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        this.text = text;
        IsHidden = false;
    }

    // Hide the word
    public void Hide()
    {
        IsHidden = true;
    }

    // This gets the displayed text of the word (either the actual word or underscores if hidden)
    public string GetDisplayedText()
    {
        return IsHidden ? new string('_', text.Length) : text;
    }
}
