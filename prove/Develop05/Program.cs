using System;
using System.Collections.Generic;
using System.IO;

// Base class Goal
abstract class Goal
{
    private string title;
    private string description;
    private int points;

    public Goal(string title, string description, int points)
    {
        this.title = title;
        this.description = description;
        this.points = points;
    }

    public abstract void DisplayGoal();
    public abstract void RecordEvent();
    public abstract bool IsComplete();

    public string GetTitle() => title;
    public string GetDescription() => description;
    public int GetPoints() => points;

    public virtual string Serialize()
    {
        return $"{GetType().Name}:{title},{description},{points}";
    }

    public static Goal Deserialize(string data)
    {
        string[] parts = data.Split(':');
        string type = parts[0];
        string[] attributes = parts[1].Split(',');

        switch (type)
        {
            case nameof(SimpleGoal):
                return new SimpleGoal(attributes[0], attributes[1], int.Parse(attributes[2]), bool.Parse(attributes[3]));
            case nameof(EternalGoal):
                return new EternalGoal(attributes[0], attributes[1], int.Parse(attributes[2]));
            case nameof(ChecklistGoal):
                return new ChecklistGoal(attributes[0], attributes[1], int.Parse(attributes[2]), int.Parse(attributes[3]), int.Parse(attributes[4]), int.Parse(attributes[5]));
            case nameof(ProgressGoal):
                return new ProgressGoal(attributes[0], attributes[1], int.Parse(attributes[2]), int.Parse(attributes[3]), int.Parse(attributes[4]));
            case nameof(NegativeGoal):
                return new NegativeGoal(attributes[0], attributes[1], int.Parse(attributes[2]), bool.Parse(attributes[3]));
            default:
                throw new Exception("Unknown goal type");
        }
    }
}

// Derived class SimpleGoal
class SimpleGoal : Goal
{
    private bool isComplete;

    public SimpleGoal(string title, string description, int points, bool isComplete = false)
        : base(title, description, points)
    {
        this.isComplete = isComplete;
    }

    public override void DisplayGoal()
    {
        Console.WriteLine($"{GetTitle()} - {GetDescription()} - Points: {GetPoints()} - Complete: {isComplete}");
    }

    public override void RecordEvent()
    {
        if (!isComplete)
        {
            isComplete = true;
            Program.AddPoints(GetPoints());
        }
    }

    public override bool IsComplete() => isComplete;

    public override string Serialize()
    {
        return base.Serialize() + $",{isComplete}";
    }
}

// Derived class EternalGoal
class EternalGoal : Goal
{
    public EternalGoal(string title, string description, int points)
        : base(title, description, points)
    {
    }

    public override void DisplayGoal()
    {
        Console.WriteLine($"{GetTitle()} - {GetDescription()} - Points: {GetPoints()} - Eternal Goal");
    }

    public override void RecordEvent()
    {
        Program.AddPoints(GetPoints());
    }

    public override bool IsComplete() => false;
}

// Derived class ChecklistGoal
class ChecklistGoal : Goal
{
    private int targetCount;
    private int currentCount;
    private int bonusPoints;

    public ChecklistGoal(string title, string description, int points, int targetCount, int currentCount = 0, int bonusPoints = 0)
        : base(title, description, points)
    {
        this.targetCount = targetCount;
        this.currentCount = currentCount;
        this.bonusPoints = bonusPoints;
    }

    public override void DisplayGoal()
    {
        Console.WriteLine($"{GetTitle()} - {GetDescription()} - Points: {GetPoints()} - Completed: {currentCount}/{targetCount}");
    }

    public override void RecordEvent()
    {
        if (currentCount < targetCount)
        {
            currentCount++;
            Program.AddPoints(GetPoints());
            if (currentCount == targetCount)
            {
                Program.AddPoints(bonusPoints);
            }
        }
    }

    public override bool IsComplete() => currentCount >= targetCount;

    public override string Serialize()
    {
        return base.Serialize() + $",{targetCount},{currentCount},{bonusPoints}";
    }
}

// Derived class ProgressGoal
class ProgressGoal : Goal
{
    private int totalProgress;
    private int currentProgress;

    public ProgressGoal(string title, string description, int points, int totalProgress, int currentProgress = 0)
        : base(title, description, points)
    {
        this.totalProgress = totalProgress;
        this.currentProgress = currentProgress;
    }

    public override void DisplayGoal()
    {
        Console.WriteLine($"{GetTitle()} - {GetDescription()} - Points: {GetPoints()} - Progress: {currentProgress}/{totalProgress}");
    }

    public override void RecordEvent()
    {
        if (currentProgress < totalProgress)
        {
            currentProgress++;
            Program.AddPoints(GetPoints());
        }
    }

    public override bool IsComplete() => currentProgress >= totalProgress;

    public override string Serialize()
    {
        return base.Serialize() + $",{totalProgress},{currentProgress}";
    }
}

// Derived class NegativeGoal
class NegativeGoal : Goal
{
    private bool isComplete;

    public NegativeGoal(string title, string description, int points, bool isComplete = false)
        : base(title, description, -points)
    {
        this.isComplete = isComplete;
    }

    public override void DisplayGoal()
    {
        Console.WriteLine($"{GetTitle()} - {GetDescription()} - Points: {GetPoints()} - Complete: {isComplete}");
    }

    public override void RecordEvent()
    {
        if (!isComplete)
        {
            isComplete = true;
            Program.AddPoints(GetPoints());
        }
    }

    public override bool IsComplete() => isComplete;

    public override string Serialize()
    {
        return base.Serialize() + $",{isComplete}";
    }
}

// Main program class
class Program
{
    private static List<Goal> goals = new List<Goal>();
    private static int userScore = 0;
    private static int userLevel = 1;
    private static int levelUpThreshold = 100;

    public static void AddPoints(int points)
    {
        userScore += points;
        if (userScore >= userLevel * levelUpThreshold)
        {
            userLevel++;
            Console.WriteLine($"Congratulations! You've reached Level {userLevel}!");
        }
    }

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\nEternal Quest Program");
            Console.WriteLine("1. Display Goals");
            Console.WriteLine("2. Create New Goal");
            Console.WriteLine("3. Record Event");
            Console.WriteLine("4. Save Goals");
            Console.WriteLine("5. Load Goals");
            Console.WriteLine("6. Display Score");
            Console.WriteLine("7. Display Level");
            Console.WriteLine("8. Exit");

            Console.Write("Select an option: ");
            int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    DisplayGoals();
                    break;
                case 2:
                    CreateNewGoal();
                    break;
                case 3:
                    RecordEvent();
                    break;
                case 4:
                    SaveGoals();
                    break;
                case 5:
                    LoadGoals();
                    break;
                case 6:
                    DisplayScore();
                    break;
                case 7:
                    DisplayLevel();
                    break;
                case 8:
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void DisplayGoals()
    {
        foreach (var goal in goals)
        {
            goal.DisplayGoal();
        }
    }

    static void CreateNewGoal()
    {
        Console.WriteLine("Select goal type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.WriteLine("4. Progress Goal");
        Console.WriteLine("5. Negative Goal");

        int goalType = int.Parse(Console.ReadLine());

        Console.Write("Enter title: ");
        string title = Console.ReadLine();

        Console.Write("Enter description: ");
        string description = Console.ReadLine();

        Console.Write("Enter points: ");
        int points = int.Parse(Console.ReadLine());

        switch (goalType)
        {
            case 1:
                goals.Add(new SimpleGoal(title, description, points));
                break;
            case 2:
                goals.Add(new EternalGoal(title, description, points));
                break;
            case 3:
                Console.Write("Enter target count: ");
                int targetCount = int.Parse(Console.ReadLine());

                Console.Write("Enter bonus points: ");
                int bonusPoints = int.Parse(Console.ReadLine());

                goals.Add(new ChecklistGoal(title, description, points, targetCount, 0, bonusPoints));
                break;
            case 4:
                Console.Write("Enter total progress: ");
                int totalProgress = int.Parse(Console.ReadLine());

                goals.Add(new ProgressGoal(title, description, points, totalProgress));
                break;
            case 5:
                goals.Add(new NegativeGoal(title, description, points));
                break;
            default:
                Console.WriteLine("Invalid goal type.");
                break;
        }
    }

    static void RecordEvent()
    {
        Console.WriteLine("Select a goal to record an event:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].GetTitle()}");
        }

        int goalIndex = int.Parse(Console.ReadLine()) - 1;
        if (goalIndex >= 0 && goalIndex < goals.Count)
        {
            goals[goalIndex].RecordEvent();
        }
        else
        {
            Console.WriteLine("Invalid goal selection.");
        }
    }

    static void SaveGoals()
    {
        Console.Write("Enter filename to save goals: ");
        string filename = Console.ReadLine();

        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine(userScore);
            outputFile.WriteLine(userLevel);
            foreach (var goal in goals)
            {
                outputFile.WriteLine(goal.Serialize());
            }
        }

        Console.WriteLine("Goals saved successfully.");
    }

    static void LoadGoals()
    {
        Console.Write("Enter filename to load goals: ");
        string filename = Console.ReadLine();

        if (File.Exists(filename))
        {
            string[] lines = File.ReadAllLines(filename);
            userScore = int.Parse(lines[0]);
            userLevel = int.Parse(lines[1]);
            goals.Clear();

            for (int i = 2; i < lines.Length; i++)
            {
                goals.Add(Goal.Deserialize(lines[i]));
            }

            Console.WriteLine("Goals loaded successfully.");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }

    static void DisplayScore()
    {
        Console.WriteLine($"User Score: {userScore}");
    }

    static void DisplayLevel()
    {
        Console.WriteLine($"User Level: {userLevel}");
    }
}
