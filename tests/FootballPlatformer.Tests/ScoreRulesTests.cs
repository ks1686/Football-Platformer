using Xunit;

public class ScoreRulesTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("Score:")]
    [InlineData("Final Score:")]
    public void TryParseScore_rejects_non_numeric_labels(string text)
    {
        Assert.False(ScoreRules.TryParseScore(text, out var score));
        Assert.Equal(0, score);
    }

    [Theory]
    [InlineData("1500", 1500)]
    [InlineData("Score: 1500", 1500)]
    [InlineData("Final Score: 1500 / 400000", 1500)]
    [InlineData("0", 0)]
    [InlineData("  Score: 42", 42)]
    [InlineData("-5", -5)]
    [InlineData("Score: -12", -12)]
    public void TryParseScore_reads_first_integer(string text, int expected)
    {
        Assert.True(ScoreRules.TryParseScore(text, out var score));
        Assert.Equal(expected, score);
    }

    [Fact]
    public void TryParseScore_rejects_values_beyond_int_range()
    {
        Assert.False(ScoreRules.TryParseScore("99999999999", out var score));
        Assert.Equal(0, score);
        Assert.False(ScoreRules.TryParseScore("Final Score: 99999999999 / 400000", out var overflowed));
        Assert.Equal(0, overflowed);
    }

    [Fact]
    public void Formatters_match_hud_and_end_screen()
    {
        Assert.Equal("Score: 1500", ScoreRules.FormatHudScore(1500));
        Assert.Equal(
            "Final Score: 1500 / 400000",
            ScoreRules.FormatFinalScore(1500, ScoreRules.DefaultTargetScore));
        Assert.Equal("Score: 0", ScoreRules.FormatHudScore(0));
        Assert.Equal("Score: -7", ScoreRules.FormatHudScore(-7));
        Assert.Equal("Final Score: 0 / 400000", ScoreRules.FormatFinalScore(0, ScoreRules.DefaultTargetScore));
    }

    [Fact]
    public void SanitizeUsername_strips_zero_width_and_trims()
    {
        Assert.Equal(string.Empty, ScoreRules.SanitizeUsername("\u200B"));
        Assert.Equal(string.Empty, ScoreRules.SanitizeUsername(null));
        Assert.Equal("Karim", ScoreRules.SanitizeUsername("  Karim\u200B  "));
        Assert.False(ScoreRules.IsValidUsername(ScoreRules.SanitizeUsername("\u200B")));
        Assert.True(ScoreRules.IsValidUsername("Karim"));
        Assert.False(ScoreRules.IsValidUsername(""));
        Assert.False(ScoreRules.IsValidUsername(null));
        Assert.True(ScoreRules.IsValidUsername(new string('a', 127)));
        Assert.False(ScoreRules.IsValidUsername(new string('a', 128)));
    }

    [Fact]
    public void FinalScoreKey_is_stable()
    {
        Assert.Equal("FinalScore", ScoreRules.FinalScoreKey);
        Assert.Equal(400000, ScoreRules.DefaultTargetScore);
    }
}
