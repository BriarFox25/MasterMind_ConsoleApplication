using MasterMind.Models;
using MasterMind_ConsoleGame;
using MasterMInd_ConsoleGame;
using MasterMInd_ConsoleGame.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

public class Program
{
    public static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration) // Read settings from appsettings.json
            .CreateLogger();

        var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        IConfiguration config = builder.Build();

        var appSettings = config.GetSection("AppSettings").Get<AppSettings>();

        var services = new ServiceCollection();

        services.AddLogging(configure =>
        {
            configure.ClearProviders();
            configure.AddSerilog(Log.Logger);
            configure.SetMinimumLevel(LogLevel.Debug);
        });

        services.AddSingleton<ISecretGenerator, RandomCodeGenerator>();
        services.AddScoped<IUserInput, ConsoleUserInput>();
        services.AddScoped<IOutputDisplay, ConsoleOutputDisplay>();
        services.AddSingleton<IScoringService, MastermindScoringService>();
        services.AddTransient<IGame, MasterMindGame>();
        services.AddSingleton<Application>();
        services.AddSingleton(Log.Logger);
        services.AddSingleton(appSettings);

        var serviceProvider = services.BuildServiceProvider();

        try
        {
            Log.Information("Application starting up.");
            var app = serviceProvider.GetRequiredService<Application>();
            app.LoadGame();
        }
        catch (Exception ex)
        {
            {
                Log.Information(ex.ToString());
            }
        }
    }

    public class Application
    {
        private readonly IGame _game;

        public Application(IGame game)
        {
            _game = game;
        }

        public void LoadGame()
        {
            Log.Information($"Loading: {_game.GetType().Name}");
            _game.Start();
        }
    }
}

