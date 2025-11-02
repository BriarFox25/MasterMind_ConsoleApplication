using MasterMind_ConsoleGame.Interfaces;

namespace MasterMind_ConsoleGame
{
    public class MastermindScoringService : IScoringService
    {
        public (int correctPosition, int correctDigit) ScoreGuess(int[] secret, int[] guess)
        {
            int correctPosition = 0; // The '+' count
            int correctDigit = 0;    // The '-' count

            bool[] secretUsed = new bool[secret.Length];
            bool[] guessUsed = new bool[guess.Length];

            // Check for Correct Number and Location (+)
            for (int i = 0; i < secret.Length; i++)
            {
                if (secret[i] == guess[i])
                {
                    correctPosition++;
                    secretUsed[i] = true;
                    guessUsed[i] = true;
                }
            }

            // Check for Correct Number (-)
            for (int i = 0; i < guess.Length; i++)
            {
                if (guessUsed[i]) continue; // Skip digits already counted as '+'

                for (int j = 0; j < secret.Length; j++)
                {
                    if (!secretUsed[j] && guess[i] == secret[j])
                    {
                        correctDigit++;
                        secretUsed[j] = true; // Mark the secret digit as used
                        break;
                    }
                }
            }

            return (correctPosition, correctDigit);
        }
    }
}
