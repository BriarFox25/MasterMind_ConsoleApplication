using MasterMInd_ConsoleGame.Interfaces;

namespace MasterMInd_ConsoleGame
{
    public class ConsoleMessageHandler : IMessageHandler
    {
        public int[] GetGuess(int length, int maxDigit)
        {
            while (true)
            {
                Console.Write($"Enter your {length}-digit guess (digits 1-{maxDigit}): ");
                string input = Console.ReadLine();

                if (input == null || input.Length != length || !input.All(char.IsDigit))
                {
                    DisplayMessage($"Invalid input. Please enter exactly {length} digits.");
                    continue;
                }

                int[] guess = new int[length];
                bool isValid = true;
                for (int i = 0; i < length; i++)
                {
                    guess[i] = int.Parse(input[i].ToString());
                    if (guess[i] < 1 || guess[i] > maxDigit)
                    {
                        DisplayMessage($"Invalid digit: All digits must be between 1 and {maxDigit}.");
                        isValid = false;
                        break;
                    }
                }

                if (isValid) return guess;
            }
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void DisplayHint(int correctPosition, int correctDigit)
        {
            string hint = "";

            // Print '+' signs first (Correct position and digit)
            for (int i = 0; i < correctPosition; i++)
            {
                hint += "+";
            }

            // Print '-' signs second (Correct digit, wrong position)
            for (int i = 0; i < correctDigit; i++)
            {
                hint += "-";
            }

            Console.WriteLine($"Hint: {hint}");
        }
    }
}
