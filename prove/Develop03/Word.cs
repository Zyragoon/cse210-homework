using System.Text;

public class Word
{
    public string Text { get; }
    public bool Hidden { get; private set; }

    public Word(string text)
    {
        Text = text ?? string.Empty;
        Hidden = false;
    }

    public void Hide()
    {
        Hidden = true;
    }

    public string GetDisplayText()
    {
        if (!Hidden)
            return Text;

        var result = new StringBuilder(Text.Length);
        foreach (char c in Text)
        {
            if (char.IsLetterOrDigit(c))
                result.Append('_');
            else
                result.Append(c);
        }
        return result.ToString();
    }
}
