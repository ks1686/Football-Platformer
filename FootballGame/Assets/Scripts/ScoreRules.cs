using System.Globalization;
using System.Text.RegularExpressions;

public static class ScoreRules
{
    public const string FinalScoreKey = "FinalScore";
    public const int DefaultTargetScore = 400000;

    static readonly Regex FirstInteger = new Regex(@"-?\d+", RegexOptions.Compiled);

    public static bool TryParseScore(string text, out int score)
    {
        score = 0;
        if (string.IsNullOrWhiteSpace(text))
        {
            return false;
        }

        var match = FirstInteger.Match(text);
        if (!match.Success)
        {
            return false;
        }

        return int.TryParse(
            match.Value,
            NumberStyles.Integer,
            CultureInfo.InvariantCulture,
            out score);
    }

    public static string FormatHudScore(int currentScore)
    {
        return "Score: " + currentScore.ToString(CultureInfo.InvariantCulture);
    }

    public static string FormatFinalScore(int currentScore, int targetScore)
    {
        return "Final Score: "
            + currentScore.ToString(CultureInfo.InvariantCulture)
            + " / "
            + targetScore.ToString(CultureInfo.InvariantCulture);
    }

    public static string SanitizeUsername(string raw)
    {
        if (raw == null)
        {
            return string.Empty;
        }

        return raw
            .Replace("\u200B", string.Empty)
            .Replace("\u200C", string.Empty)
            .Replace("\u200D", string.Empty)
            .Replace("\uFEFF", string.Empty)
            .Trim();
    }

    public static bool IsValidUsername(string username)
    {
        return !string.IsNullOrWhiteSpace(username) && username.Length <= 127;
    }
}
