using System;

class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int pointValue)
        : base(name, description, pointValue)
    {
    }

    public override int RecordEvent()
    {
        return PointValue;
    }

    public override string GetStatusText()
    {
        return "[ ]";
    }

    public override string GetDisplayText()
    {
        return $"{GetStatusText()} {Name}: {Description} (Eternal goal, +{PointValue} pts each time)";
    }

    public override string Serialize()
    {
        return $"EternalGoal|{Name}|{Description}|{PointValue}";
    }
}
