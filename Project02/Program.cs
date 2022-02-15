using System;
using Project02.Models;
using Project02.Services;

namespace Project02
{
    class Program
    {
        static void Main(string[] args)
        {
            var gameSettings = new GameSettings { AllowTie = true, DeckCount = 1, DeckCardCount = 52 };

            var deckService = new DeckService(gameSettings);
            var suitService = new SuitService(gameSettings);
            var gameService = new GameService(gameSettings, deckService, suitService);

            var result = gameService.Play();
            Console.WriteLine(result);

            Console.ReadLine();
        }
    }
}