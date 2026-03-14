using System;
using System.Text.RegularExpressions;

public class Reference
{
    public string Book { get; }
    public int Chapter { get; }
    public int VerseStart { get; }
    public int? VerseEnd { get; }
    public string Raw { get; }

    public Reference(string raw)
    {
        Raw = raw?.Trim() ?? throw new ArgumentNullException(nameof(raw));

        var match = Regex.Match(Raw, @"^(?<book>.+?)\s+(?<chapter>\d+):(?<verseStart>\d+)(-(?<verseEnd>\d+))?$");
        if (!match.Success)
        {
            throw new ArgumentException($"Invalid scripture reference format: '{raw}'", nameof(raw));
        }

        Book = match.Groups["book"].Value;
        Chapter = int.Parse(match.Groups["chapter"].Value);
        VerseStart = int.Parse(match.Groups["verseStart"].Value);
        if (match.Groups["verseEnd"].Success)
            VerseEnd = int.Parse(match.Groups["verseEnd"].Value);
    }

    public override string ToString()
    {
        if (VerseEnd.HasValue)
            return $"{Book} {Chapter}:{VerseStart}-{VerseEnd}";
        return $"{Book} {Chapter}:{VerseStart}";
    }
}
