using System;
using System.Collections.Generic;
using System.IO;

public static class ScriptureLoader
{
    public static List<Scripture> LoadFromCsv(string path)
    {
        var scriptures = new List<Scripture>();

        using var reader = new StreamReader(path);
        // Skip header row
        reader.ReadLine();

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var parts = ParseCsvLine(line);
            // Expect columns: Book,Chapter,Verse,Text
            if (parts.Length < 4)
                continue;

            string referenceText = $"{parts[0]} {parts[1]}:{parts[2]}";
            string verseText = parts[3];

            try
            {
                var reference = new Reference(referenceText);
                scriptures.Add(new Scripture(reference, verseText));
            }
            catch
            {
                // Ignore malformed rows
            }
        }

        return scriptures;
    }

    private static string[] ParseCsvLine(string line)
    {
        var result = new List<string>();
        bool inQuotes = false;
        string current = string.Empty;

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (c == '"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    current += '"';
                    i++;
                }
                else
                {
                    inQuotes = !inQuotes;
                }
            }
            else if (c == ',' && !inQuotes)
            {
                result.Add(current);
                current = string.Empty;
            }
            else
            {
                current += c;
            }
        }

        result.Add(current);
        return result.ToArray();
    }
}
