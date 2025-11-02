using MasterMInd_ConsoleGame.Interfaces;

namespace MasterMInd_ConsoleGame
{
    public class RandomCodeGenerator : ISecretGenerator
    {
        public int[] GenerateSecret(int length, int maxDigit)
        {
            Random random = new Random();
            int[] code = new int[length];
            for (int i = 0; i < length; i++)
            {
                code[i] = random.Next(1, maxDigit + 1);
            }
            return code;
        }
    }
}
