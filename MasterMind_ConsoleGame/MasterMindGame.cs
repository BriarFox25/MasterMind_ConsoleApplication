using MasterMind.Models;
using MasterMInd_ConsoleGame.Interfaces;

namespace MasterMInd_ConsoleGame
{
    public class MasterMindGame(ISecretGenerator secretGenerator, IUserInput inputHandler, IOutputDisplay outputDisplay, IScoringService gameEngine, AppSettings appSettings) : IGame
    {
        private readonly ISecretGenerator _secretGenerator = secretGenerator;
        private readonly IUserInput _inputHandler = inputHandler;
        private readonly IOutputDisplay _outputDisplay = outputDisplay;
        private readonly IScoringService _scoreingService = gameEngine;
        private readonly AppSettings _appSettings = appSettings;

        public void Start()
        {
            _outputDisplay.DisplayWelcome(_appSettings.SecretLength, _appSettings.MaxGuessValue, _appSettings.MaxAttempts);

            bool gameWon = false;
            int[] secretCode = _secretGenerator.GenerateSecret(_appSettings.SecretLength, _appSettings.MaxGuessValue);

            // Game Rounds
            for (int attempt = 1; attempt <= _appSettings.MaxAttempts; attempt++)
            {
                _outputDisplay.DisplayMessage($"\nAttempt {attempt} of {_appSettings.MaxAttempts}");
                int[] guess = _inputHandler.GetGuess(_appSettings.SecretLength, _appSettings.MaxGuessValue);

                (int correctPosition, int correctDigit) = _scoreingService.ScoreGuess(secretCode, guess);

                _outputDisplay.DisplayHint(correctPosition, correctDigit);

                if (correctPosition == _appSettings.SecretLength)
                {
                    gameWon = true;
                    break;
                }
            }

            //Game Over
            _outputDisplay.DisplayGameOver(gameWon, secretCode);
        }
    }
}
