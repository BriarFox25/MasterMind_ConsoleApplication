using MasterMInd_ConsoleGame;
using Xunit;
using Assert = Xunit.Assert;

namespace MasterMind_ConsoleGame.Tests
{
    public class MastermindScoringServiceTests
    {
        private readonly MastermindScoringService _scoringService = new MastermindScoringService();

        private (int correctPosition, int correctDigit) Score(int[] secret, int[] guess)
        {
            return _scoringService.ScoreGuess(secret, guess);
        }

        [Fact]
        public void ScoreGuess_PerfectMatch_ReturnsFourPlus()
        {
            // Arrange
            int[] secret = { 1, 2, 3, 4 };
            int[] guess = { 1, 2, 3, 4 };

            // Act
            (int correctPosition, int correctDigit) = Score(secret, guess);

            // Assert: ++++
            Assert.Equal(4, correctPosition);
            Assert.Equal(0, correctDigit);
        }

        [Fact]
        public void ScoreGuess_AllCorrectButWrongPosition_ReturnsFourMinus()
        {
            // Arrange
            int[] secret = { 1, 2, 3, 4 };
            int[] guess = { 4, 3, 2, 1 };

            // Act
            (int correctPosition, int correctDigit) = Score(secret, guess);

            // Assert: ----
            Assert.Equal(0, correctPosition);
            Assert.Equal(4, correctDigit);
        }

        [Fact]
        public void ScoreGuess_NoMatches_ReturnsZeroHints()
        {
            // Arrange
            int[] secret = { 1, 1, 1, 1 };
            int[] guess = { 2, 2, 2, 2 };

            // Act
            (int correctPosition, int correctDigit) = Score(secret, guess);

            // Assert: (Empty hint)
            Assert.Equal(0, correctPosition);
            Assert.Equal(0, correctDigit);
        }

        [Fact]
        public void ScoreGuess_ExerciseExample_ReturnsTwoPlusOneMinus()
        {
            // Arrange: Secret is 1234. Guess is 4233. Expected: ++-
            int[] secret = { 1, 2, 3, 4 };
            int[] guess = { 4, 2, 3, 3 };

            // Act
            (int correctPosition, int correctDigit) = Score(secret, guess);

            // Assert: '2' and '3' in place (++). '4' correct digit, wrong spot (-).
            Assert.Equal(2, correctPosition);
            Assert.Equal(1, correctDigit);
        }

        [Fact]
        public void ScoreGuess_SinglePlusAndTwoMinus()
        {
            // Arrange: Secret is 6543. Guess is 6355. Expected: +--
            int[] secret = { 6, 5, 4, 3 };
            int[] guess = { 6, 3, 5, 5 };

            // Act
            (int correctPosition, int correctDigit) = Score(secret, guess);

            // Assert: 
            // 1. '6' is in place (+)
            // 2. '3' and '5' are correct digits but wrong spots (--)
            Assert.Equal(1, correctPosition);
            Assert.Equal(2, correctDigit);
        }

        [Fact]
        public void ScoreGuess_GuessDuplicatesMustMatchUniqueSecretDigits()
        {
            // Arrange: Secret has 2 ones. Guess has 4 ones.
            int[] secret = { 1, 1, 2, 3 };
            int[] guess = { 1, 1, 1, 1 };

            // Act
            (int correctPosition, int correctDigit) = Score(secret, guess);

            // Assert: 
            // 1. '1' at index 0 is a (+)
            // 2. '1' at index 1 is a (+)
            // 3. The two remaining '1's in the guess don't match the '2' or '3' in the secret.
            Assert.Equal(2, correctPosition);
            Assert.Equal(0, correctDigit);    // Must not be 2 or 4.
        }

        [Fact]
        public void ScoreGuess_PlusTakesPriorityOverMinus()
        {
            // Arrange: The '1' at index 1 is a '+', so it can't be counted as a '-'.
            int[] secret = { 5, 1, 6, 6 };
            int[] guess = { 1, 1, 1, 1 };

            // Act
            (int correctPosition, int correctDigit) = Score(secret, guess);

            // Assert: 
            // 1. '1' at index 1 is a (+) (Uses secret[1] and guess[1])
            // 2. The remaining guess '1's at 0, 2, 3 do not match the remaining secret digits (5, 6, 6).
            Assert.Equal(1, correctPosition);
            Assert.Equal(0, correctDigit);
        }

        [Fact]
        public void ScoreGuess_SecretDuplicatesCanOnlyBeMatchedOnce()
        {
            // Arrange: Secret has three '5's. Guess has one '5' in the wrong spot.
            int[] secret = { 5, 5, 5, 1 };
            int[] guess = { 1, 2, 3, 5 };

            // Act
            (int correctPosition, int correctDigit) = Score(secret, guess);

            // Assert: 
            // 1. The '5' in the guess at index 3 matches *one* of the '5's in the secret (e.g., index 0). (-)
            // 2. The '1' in the guess at index 0 matches the '1' in the secret at index 3. (-)
            Assert.Equal(0, correctPosition);
            Assert.Equal(2, correctDigit); // The score is NOT 4 or 3, as the '5' in the guess can only be used once.
        }
    }
}
