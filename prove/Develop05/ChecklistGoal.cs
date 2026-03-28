using System;

class ChecklistGoal : Goal
{
    private int _currentCount;
    private int _targetCount;
    private int _bonusPoints;

    public int CurrentCount => _currentCount;
    public int TargetCount => _targetCount;
    public int BonusPoints => _bonusPoints;

    public ChecklistGoal(string name, string description, int pointValue, int targetCount, int bonusPoints)
        : base(name, description, pointValue)
    {
        _targetCount = targetCount;
        _bonusPoints = bonusPoints;
        _currentCount = 0;
    }

    public ChecklistGoal(string name, string description, int pointValue, int targetCount, int bonusPoints, int currentCount, bool isComplete)
        : base(name, description, pointValue)
    {
        _targetCount = targetCount;
        _bonusPoints = bonusPoints;
        _currentCount = currentCount;
        IsComplete = isComplete;
    }

    public override int RecordEvent()
    {
        if (IsComplete)
        {
            Console.WriteLine("You already completed this checklist goal.");
            return 0;
        }

        _currentCount++;
        int earnedPoints = PointValue;

        if (_currentCount >= _targetCount)
        {
            IsComplete = true;
            earnedPoints += _bonusPoints;
            Console.WriteLine($"Goal completed! Bonus awarded: {_bonusPoints} pts.");
        }

        return earnedPoints;
    }

    public override string GetDisplayText()
    {
        return $"{GetStatusText()} {Name}: {Description} (Completed {_currentCount}/{_targetCount} times, +{PointValue} pts each time, bonus {BonusPoints} pts)";
    }

    public override string Serialize()
    {
        return $"ChecklistGoal|{Name}|{Description}|{PointValue}|{TargetCount}|{BonusPoints}|{CurrentCount}|{IsComplete}";
    }
}
