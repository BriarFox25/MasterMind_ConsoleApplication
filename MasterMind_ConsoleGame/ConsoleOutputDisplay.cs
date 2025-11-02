using MasterMind.Models;
using MasterMInd_ConsoleGame;
using MasterMInd_ConsoleGame.Interfaces;
using Microsoft.Extensions.Logging;

namespace MasterMind_ConsoleGame
{
    public class ConsoleOutputDisplay(AppSettings appSettings) : IOutputDisplay
    {
        private readonly AppSettings _appSettings = appSettings;

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void DisplayWelcome(int length, int maxDigit, int maxAttempts)
        {
            DisplayMessage($"Welcome to Mastermind!");
            ShowRules();
        }

        public void DisplayHint(int correctPosition, int correctDigit)
        {
            string hint = "";

            // Print all '+' signs first (Correct position and digit)
            for (int i = 0; i < correctPosition; i++)
            {
                hint += "+";
            }

            // Print all '-' signs second (Correct digit, wrong position)
            for (int i = 0; i < correctDigit; i++)
            {
                hint += "-";
            }

            DisplayMessage($"Hint: {hint}");
        }

        public void DisplayGameOver(bool won, int[] secretCode)
        {
            DisplayMessage("\n-------------------------------------------");
            if (won)
            {
                DisplayMessage("\nYou Win!");
            }
            else
            {
                DisplayMessage("\n-------------------------------------------");
                DisplayMessage($"Uhoh! The secret code was: {string.Join("", secretCode)}");
                DisplayMessage("You lost. Better luck next time!");
            }
        }

        private void ShowRules()
        {
            DisplayMessage("Rules:");
            DisplayMessage($"1. The computer generates a secret {_appSettings.SecretLength}-digit code using digits from 1 to {_appSettings.MaxGuessValue}.");
            DisplayMessage($"2. You have {_appSettings.MaxAttempts} attempts to guess the code.");
            DisplayMessage("3. After each guess, you'll receive feedback in the form of pluses (+) and minuses (-):");
            DisplayMessage("   - A plus (+) indicates a correct digit in the correct position.");
            DisplayMessage("   - A minus (-) indicates a correct digit but in the wrong position.");
            DisplayMessage("4. Use this feedback to refine your guesses and crack the code!");
            DisplayMessage("Good luck!\n");
        }
    }
}
