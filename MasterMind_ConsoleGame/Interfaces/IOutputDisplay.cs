namespace MasterMInd_ConsoleGame.Interfaces
{
    public interface IOutputDisplay
    {
        void DisplayMessage(string message);
        void DisplayHint(int correctPosition, int correctDigit);
        void DisplayWelcome(int codeLength, int maxDigit, int maxAttempts);
        void DisplayGameOver(bool gameWon, int[] secretCode);
    }
}
