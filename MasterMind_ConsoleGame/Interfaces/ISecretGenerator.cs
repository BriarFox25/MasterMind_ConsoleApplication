namespace MasterMind_ConsoleGame.Interfaces
{
    public interface ISecretGenerator
    {
        int[] GenerateSecret(int length, int maxDigit);
    }
}