using System;
using System.Collections.Generic;
using System.Linq;

public class Scripture
{
    public Reference Reference { get; }
    public IReadOnlyList<Word> Words => _words;

    private readonly List<Word> _words;

    public Scripture(Reference reference, string verseText)
    {
        Reference = reference ?? throw new ArgumentNullException(nameof(reference));
        _words = SplitIntoWords(verseText ?? string.Empty);
    }

    private List<Word> SplitIntoWords(string text)
    {
        var parts = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var words = new List<Word>();
        foreach (var part in parts)
        {
            words.Add(new Word(part));
        }
        return words;
    }

    private static readonly Random _rand = new Random();

    public void HideRandomWords(int count = 3)
    {
        if (count <= 0)
            return;

        var remaining = _words.Where(w => !w.Hidden).ToList();
        if (remaining.Count == 0)
            return;

        for (int i = 0; i < count && remaining.Count > 0; i++)
        {
            int index = _rand.Next(remaining.Count);
            remaining[index].Hide();
            remaining.RemoveAt(index);
        }
    }

    public bool IsFullyHidden()
    {
        return _words.All(w => w.Hidden);
    }

    public string GetDisplayText()
    {
        return $"{Reference}: {string.Join(' ', _words.Select(w => w.GetDisplayText()))}";
    }
}
