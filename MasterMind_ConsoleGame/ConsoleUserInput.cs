using MasterMInd_ConsoleGame.Interfaces;

namespace MasterMind_ConsoleGame
{
    public class ConsoleUserInput : IUserInput
    {
        public int[] GetGuess(int length, int maxDigit)
        {
            while (true)
            {
                Console.Write($"Enter your {length}-digit guess (digits 1-{maxDigit}): ");
                string input = Console.ReadLine();

                if (input == null || input.Length != length || !input.All(char.IsDigit))
                {
                    Console.WriteLine($"Invalid input. Please enter exactly {length} digits.");
                    continue;
                }

                int[] guess = new int[length];
                bool isValid = true;
                for (int i = 0; i < length; i++)
                {
                    guess[i] = int.Parse(input[i].ToString());
                    if (guess[i] < 1 || guess[i] > maxDigit)
                    {
                        Console.WriteLine($"Invalid digit: All digits must be between 1 and {maxDigit}.");
                        isValid = false;
                        break;
                    }
                }

                if (isValid) return guess;
            }
        }
    }
}
