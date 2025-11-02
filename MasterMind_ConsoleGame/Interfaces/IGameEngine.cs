namespace MasterMInd_ConsoleGame.Interfaces
{
    public interface IGameEngine
    {
        (int correctPosition, int correctDigit) CheckGuess(int[] secret, int[] guess);
    }
}
