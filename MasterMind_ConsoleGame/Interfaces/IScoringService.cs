namespace MasterMInd_ConsoleGame.Interfaces
{
    public interface IScoringService
    {
        // Returns the count of '+' and '-' hints
        (int correctPosition, int correctDigit) ScoreGuess(int[] secret, int[] guess);
    }
}
