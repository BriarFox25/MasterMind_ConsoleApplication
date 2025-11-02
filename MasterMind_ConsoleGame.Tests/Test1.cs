using Xunit;
using MasterMInd_ConsoleGame;
using Assert = Xunit.Assert;
public class MasterMindEngineTests
{
    private readonly MasterMindEngine _engine = new MasterMindEngine();

    [Fact]
    public void CheckGuess_PerfectMatch_ReturnsFourPlus()
    {
        // Arrange
        int[] secret = { 1, 2, 3, 4 };
        int[] guess = { 1, 2, 3, 4 };

        // Act
        // The result is a tuple: (correctPosition, correctDigit)
        (int correctPosition, int correctDigit) = _engine.CheckGuess(secret, guess);

        // Assert
        Assert.Equal(4, correctPosition); // Should be '++++'
        Assert.Equal(0, correctDigit);    // Should be zero '-'
    }

    [Fact]
    public void CheckGuess_NoMatches_ReturnsZeroHints()
    {
        // Arrange
        int[] secret = { 1, 1, 1, 1 };
        int[] guess = { 2, 2, 2, 2 };

        // Act
        (int correctPosition, int correctDigit) = _engine.CheckGuess(secret, guess);

        // Assert
        Assert.Equal(0, correctPosition);
        Assert.Equal(0, correctDigit);
    }

    [Fact]
    public void CheckGuess_AllCorrectButWrongPosition_ReturnsFourMinus()
    {
        // Arrange
        int[] secret = { 1, 2, 3, 4 };
        int[] guess = { 4, 3, 2, 1 };

        // Act
        (int correctPosition, int correctDigit) = _engine.CheckGuess(secret, guess);

        // Assert
        Assert.Equal(0, correctPosition); // Should be zero '+'
        Assert.Equal(4, correctDigit);    // Should be '----'
    }

    [Fact]
    public void CheckGuess_MixedExample_ReturnsTwoPlusOneMinus()
    {
        // Arrange: Secret is 1234. Example guess: 4233
        int[] secret = { 1, 2, 3, 4 };
        int[] guess = { 4, 2, 3, 3 }; // '2' and '3' are correct position. '4' is wrong position.

        // Act
        (int correctPosition, int correctDigit) = _engine.CheckGuess(secret, guess);

        // Assert
        Assert.Equal(2, correctPosition); // '2' and '3' are correct position (++)
        Assert.Equal(1, correctDigit);    // '4' is correct digit, wrong spot (-)
    }

    [Fact]
    public void CheckGuess_AvoidsDoubleCounting_PlusTakesPriority()
    {
        // Arrange: The '1' at index 1 is ONLY a '+', not a '-'
        int[] secret = { 5, 1, 6, 6 };
        int[] guess = { 1, 1, 1, 1 };

        // Act
        (int correctPosition, int correctDigit) = _engine.CheckGuess(secret, guess);

        // Assert
        Assert.Equal(1, correctPosition); // One correct position (+)
        Assert.Equal(0, correctDigit);    // Zero correct digits in wrong spot (-)
    }
}