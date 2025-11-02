using MasterMind.Models;
using MasterMInd_ConsoleGame;
using MasterMInd_ConsoleGame.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        IConfiguration config = builder.Build();

        var appSettings = config.GetSection("AppSettings").Get<AppSettings>();

        var services = CreateServices(appSettings);

        var app = services.GetRequiredService<Application>();

        app.LoadGame();
    }

    private static ServiceProvider CreateServices(AppSettings settings)
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IGame, MasterMindGame>()
            .AddSingleton<ISecretGenerator, RandomCodeGenerator>()
            .AddScoped<IMessageHandler, ConsoleMessageHandler>()
            .AddSingleton<IGameEngine, MasterMindEngine>()
            .AddSingleton<Application>()
            .AddSingleton(settings)
            
            .BuildServiceProvider();

        return serviceProvider;
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
            _game.Start();
        }
    }
}

