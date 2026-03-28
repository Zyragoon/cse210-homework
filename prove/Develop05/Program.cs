class Program
{
    private static readonly List<Goal> Goals = new();
    private static int _totalScore;

    static void Main()
    {
        Console.WriteLine("Welcome to Eternal Quest: your goal tracking adventure.");
        LoadDefaultData();

        while (true)
        {
            Menu.ShowMainMenu(_totalScore);
            string choice = Console.ReadLine()?.Trim() ?? string.Empty;

            switch (choice)
            {
                case "1": CreateGoal(); break;
                case "2": ListGoals(); break;
                case "3": RecordEvent(); break;
                case "4": SaveGoals(); break;
                case "5": LoadGoals(); break;
                case "6": ShowScore(); break;
                case "7":
                    Console.WriteLine("Farewell, brave quester. Keep making progress!");
                    return;
                default:
                    Console.WriteLine("Please choose a valid option from the menu.");
                    break;
            }

            Console.WriteLine();
        }
    }

    private static void CreateGoal()
    {
        Menu.ShowGoalTypes();
        string choice = Console.ReadLine()?.Trim() ?? string.Empty;

        Console.Write("Goal name: ");
        string name = Console.ReadLine() ?? string.Empty;
        Console.Write("Goal description: ");
        string description = Console.ReadLine() ?? string.Empty;
        int points = PromptForInt("Point value for this goal: ");

        switch (choice)
        {
            case "1":
                Goals.Add(new SimpleGoal(name, description, points));
                Console.WriteLine("Simple goal created.");
                break;
            case "2":
                Goals.Add(new EternalGoal(name, description, points));
                Console.WriteLine("Eternal goal created.");
                break;
            case "3":
                int targetCount = PromptForInt("How many times must the checklist goal be completed? ");
                int bonusPoints = PromptForInt("Bonus points awarded when the checklist is complete: ");
                Goals.Add(new ChecklistGoal(name, description, points, targetCount, bonusPoints));
                Console.WriteLine("Checklist goal created.");
                break;
            default:
                Console.WriteLine("Unknown goal type. No goal was created.");
                break;
        }
    }

    private static void ListGoals()
    {
        Console.WriteLine("\nGoal List:");

        if (Goals.Count == 0)
        {
            Console.WriteLine("No goals created yet.");
            return;
        }

        for (int i = 0; i < Goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Goals[i].GetDisplayText()}");
        }
    }

    private static void RecordEvent()
    {
        if (Goals.Count == 0)
        {
            Console.WriteLine("You have no goals to record. Create one first.");
            return;
        }

        ListGoals();
        int goalNumber = PromptForInt("Enter the number of the goal you completed: ");

        if (goalNumber < 1 || goalNumber > Goals.Count)
        {
            Console.WriteLine("That goal number is invalid.");
            return;
        }

        Goal selectedGoal = Goals[goalNumber - 1];
        int earned = selectedGoal.RecordEvent();

        if (earned > 0)
        {
            _totalScore += earned;
            Console.WriteLine($"You earned {earned} points!");
            Console.WriteLine(GetQuestRankMessage(_totalScore));
        }
        else
        {
            Console.WriteLine("No points earned from that action.");
        }
    }

    private static void SaveGoals()
    {
        Console.Write("Enter filename to save goals (for example goals.txt): ");
        string filename = Console.ReadLine()?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(filename))
        {
            Console.WriteLine("Invalid filename.");
            return;
        }

        try
        {
            using StreamWriter writer = new(filename);
            writer.WriteLine($"Score|{_totalScore}");
            foreach (Goal goal in Goals)
            {
                writer.WriteLine(goal.Serialize());
            }

            Console.WriteLine($"Saved {Goals.Count} goals and score to {filename}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save goals: {ex.Message}");
        }
    }

    private static void LoadGoals()
    {
        Console.Write("Enter filename to load goals from: ");
        string filename = Console.ReadLine()?.Trim() ?? string.Empty;

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        try
        {
            string[] lines = File.ReadAllLines(filename);
            Goals.Clear();
            _totalScore = 0;

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                if (line.StartsWith("Score|"))
                {
                    _totalScore = int.Parse(line.Split('|')[1]);
                    continue;
                }

                Goals.Add(Goal.Deserialize(line));
            }

            Console.WriteLine($"Loaded {Goals.Count} goals and score {_totalScore} from {filename}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load goals: {ex.Message}");
        }
    }

    private static void ShowScore()
    {
        Console.WriteLine($"Current score: {_totalScore}");
        Console.WriteLine(GetQuestRankMessage(_totalScore));
    }

    private static int PromptForInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine()?.Trim() ?? string.Empty;

            if (int.TryParse(input, out int value) && value >= 0)
            {
                return value;
            }

            Console.WriteLine("Please enter a valid non-negative integer.");
        }
    }

    private static string GetQuestRankMessage(int score)
    {
        return score switch
        {
            < 100 => "Rank: Squire — your quest has begun.",
            < 300 => "Rank: Explorer — keep chasing completion.",
            < 600 => "Rank: Champion — your progress inspires others.",
            < 1000 => "Rank: Hero — the Eternal Quest shines with your effort.",
            _ => "Rank: Eternal Knight — you are mastering your journey!"
        };
    }

    private static void LoadDefaultData()
    {
        const string defaultFile = "goals.txt";

        if (File.Exists(defaultFile))
        {
            try
            {
                string[] lines = File.ReadAllLines(defaultFile);
                Goals.Clear();
                _totalScore = 0;

                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    if (line.StartsWith("Score|"))
                    {
                        _totalScore = int.Parse(line.Split('|')[1]);
                        continue;
                    }

                    Goals.Add(Goal.Deserialize(line));
                }

                Console.WriteLine($"Loaded saved goals from {defaultFile}.");
            }
            catch
            {
                Console.WriteLine("Could not load default saved goals. Starting a new quest.");
            }
        }
        else
        {
            Console.WriteLine("Starting with a fresh eternal quest.");
        }
    }
}
