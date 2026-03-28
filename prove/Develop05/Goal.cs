using System;

abstract class Goal
{
    private string _name;
    private string _description;
    private int _pointValue;

    public string Name => _name;
    public string Description => _description;
    public int PointValue => _pointValue;
    public bool IsComplete { get; protected set; }

    protected Goal(string name, string description, int pointValue)
    {
        _name = name;
        _description = description;
        _pointValue = pointValue;
        IsComplete = false;
    }

    public abstract int RecordEvent();

    public virtual string GetStatusText()
    {
        return IsComplete ? "[X]" : "[ ]";
    }

    public virtual string GetDisplayText()
    {
        return $"{GetStatusText()} {Name}: {Description} ({PointValue} pts)";
    }

    public abstract string Serialize();

    public static Goal Deserialize(string dataLine)
    {
        string[] parts = dataLine.Split('|');
        string type = parts[0];

        return type switch
        {
            "SimpleGoal" => new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]), bool.Parse(parts[4])),
            "EternalGoal" => new EternalGoal(parts[1], parts[2], int.Parse(parts[3])),
            "ChecklistGoal" => new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]), int.Parse(parts[6]), bool.Parse(parts[7])),
            _ => throw new InvalidOperationException($"Unknown goal type: {type}"),
        };
    }
}
