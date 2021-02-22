namespace Battleship.UI
{
    using System;
    using Battleship.Service;
    using Battleship.UI.Service;
    using Microsoft.Extensions.DependencyInjection;

    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IDisplayService, DisplayService>()
                .AddSingleton<ILoadService, LoadService>()
                .AddSingleton<IAIPlayerService, AIPlayerService>()
                .BuildServiceProvider();

            Console.WriteLine(@"Battleship game is about to start. First put your ship on the board. 
                One battleship 5 fields long and 2 destroyers 4 fields long. For each ship provide
                its beggining (e.g. A1) and vertical/horizontal orientation on board.");

            var game = new GameService(
                serviceProvider.GetService<ILoadService>(),
                serviceProvider.GetService<IDisplayService>(),
                serviceProvider.GetService<IAIPlayerService>()
                );
            game.Play();

            Console.WriteLine("Press enter to finish.");
            Console.ReadLine();
        }
    }
}
