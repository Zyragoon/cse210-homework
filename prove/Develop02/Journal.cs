using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    public List<Entry> _entries = new List<Entry>();

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void Display()
    {
        foreach (var entry in _entries)
        {
            Console.WriteLine($"Date: {entry.Date}");
            Console.WriteLine($"Prompt: {entry.Prompt}");
            Console.WriteLine($"Response: {entry.Response}");
            Console.WriteLine();
        }
    }

    public void SaveToFile(string filename)
    {
        
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine("Date,Prompt,Response");
            foreach (var entry in _entries)
            {
                string date = EscapeCSV(entry.Date);
                string prompt = EscapeCSV(entry.Prompt);
                string response = EscapeCSV(entry.Response);
                writer.WriteLine($"{date},{prompt},{response}");
            }
        }
    }

    public void LoadFromFile(string filename)
    {
        _entries.Clear();
        using (StreamReader reader = new StreamReader(filename))
        {
            string line = reader.ReadLine(); 
            while ((line = reader.ReadLine()) != null)
            {
                var parts = ParseCSV(line);
                if (parts.Length == 3)
                {
                    Entry entry = new Entry
                    {
                        Date = parts[0],
                        Prompt = parts[1],
                        Response = parts[2]
                    };
                    _entries.Add(entry);
                }
            }
        }
    }

    private string EscapeCSV(string field)
    {
        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
        {
            return "\"" + field.Replace("\"", "\"\"") + "\"";
        }
        return field;
    }

    private string[] ParseCSV(string line)
    {
        
        List<string> result = new List<string>();
        bool inQuotes = false;
        string current = "";
        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (c == '\"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '\"')
                {
                    current += '\"';
                    i++; // skip next
                }
                else
                {
                    inQuotes = !inQuotes;
                }
            }
            else if (c == ',' && !inQuotes)
            {
                result.Add(current);
                current = "";
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