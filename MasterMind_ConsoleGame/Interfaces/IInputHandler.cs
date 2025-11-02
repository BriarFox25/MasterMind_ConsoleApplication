namespace MasterMInd_ConsoleGame.Interfaces
{
    public interface IMessageHandler
    {
        int[] GetGuess(int length, int maxDigit);
        void DisplayMessage(string message);
        void DisplayHint(int correctPosition, int correctDigit);
    }
}
