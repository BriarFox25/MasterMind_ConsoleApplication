using MasterMind.Models;
using MasterMInd_ConsoleGame.Interfaces;

namespace MasterMInd_ConsoleGame
{
    public class MasterMindGame(ISecretGenerator secretGenerator, IMessageHandler messageHandler, IGameEngine gameEngine, AppSettings appSettings) : IGame
    {
        private readonly ISecretGenerator _secretGenerator = secretGenerator;
        private readonly IMessageHandler _messageHandler = messageHandler;
        private readonly IGameEngine _gameEngine = gameEngine;
        private readonly AppSettings _appSettings = appSettings;


        public void Start()
        {
            Console.WriteLine($"Welcome to Mastermind!");
            ShowRules();

            int[] secretCode = _secretGenerator.GenerateSecret(_appSettings.SecretLength, _appSettings.MaxGuessValue);

            // Game Rounds
            for (int attempt = 1; attempt <= _appSettings.MaxAttempts; attempt++)
            {
                _messageHandler.DisplayMessage($"\nAttempt {attempt} of {_appSettings.MaxAttempts}");
                int[] guess = _messageHandler.GetGuess(_appSettings.SecretLength, _appSettings.MaxGuessValue);

                (int correctPosition, int correctDigit) = _gameEngine.CheckGuess(secretCode, guess);

                _messageHandler.DisplayHint(correctPosition, correctDigit);

                if (correctPosition == _appSettings.SecretLength)
                {
                    // Win Game
                    _messageHandler.DisplayMessage("\nYou Win!");
                    return; // End the game
                }
            }

            // Lose Game
            _messageHandler.DisplayMessage("\n-------------------------------------------");
            _messageHandler.DisplayMessage($"Uhoh! The secret code was: {string.Join("", secretCode)}");
            _messageHandler.DisplayMessage("You lost. Better luck next time!");
        }

        public void ShowRules()
        {
            _messageHandler.DisplayMessage("Rules:");
            _messageHandler.DisplayMessage($"1. The computer generates a secret {_appSettings.SecretLength}-digit code using digits from 1 to {_appSettings.MaxGuessValue}.");
            _messageHandler.DisplayMessage($"2. You have {_appSettings.MaxAttempts} attempts to guess the code.");
            _messageHandler.DisplayMessage("3. After each guess, you'll receive feedback in the form of pluses (+) and minuses (-):");
            _messageHandler.DisplayMessage("   - A plus (+) indicates a correct digit in the correct position.");
            _messageHandler.DisplayMessage("   - A minus (-) indicates a correct digit but in the wrong position.");
            _messageHandler.DisplayMessage("4. Use this feedback to refine your guesses and crack the code!");
            _messageHandler.DisplayMessage("Good luck!\n");
            }
        }
}
