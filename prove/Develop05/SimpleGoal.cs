using System;

class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int pointValue)
        : base(name, description, pointValue)
    {
    }

    public SimpleGoal(string name, string description, int pointValue, bool isComplete)
        : base(name, description, pointValue)
    {
        IsComplete = isComplete;
    }

    public override int RecordEvent()
    {
        if (IsComplete)
        {
            Console.WriteLine("This simple goal has already been completed.");
            return 0;
        }

        IsComplete = true;
        return PointValue;
    }

    public override string GetDisplayText()
    {
        return $"{GetStatusText()} {Name}: {Description} (Complete for {PointValue} pts)";
    }

    public override string Serialize()
    {
        return $"SimpleGoal|{Name}|{Description}|{PointValue}|{IsComplete}";
    }
}
