using System;
using Project02.Behaviours.Abstractions;
using Project02.Models;
using Project02.Services.Abstractions;

namespace Project02.Behaviours
{
    public class SingleDeckGameBehaviour : IGameBehaviour
    {
        private readonly Deck _deck;
        private readonly GameSettings _settings;
        private readonly IDeckService _deckService;
        private readonly ISuitService _suitService;

        public SingleDeckGameBehaviour(GameSettings settings, IDeckService deckService, ISuitService suitService)
        {
            _settings = settings;
            _deckService = deckService;
            _suitService = suitService;

            _deck = _deckService.CreateDeck();
        }

        public string Play()
        {
            var result = string.Empty;
            var firstCard = _deckService.PlayCard(_deck);
            var secondCard = _deckService.PlayCard(_deck);

            Console.WriteLine($"First card: {firstCard.Suit}, {firstCard.Value}");
            Console.WriteLine($"Second card: {secondCard.Suit}, {secondCard.Value}");

            if (firstCard.Value == secondCard.Value)
            {
                if (!_settings.AllowTie)
                {
                    if (!string.IsNullOrEmpty(_settings.SuitPrecedence))
                    {
                        var winningCard = _suitService.ResolveTie(firstCard, secondCard);
                        return Messages.GetWinMessage(winningCard);
                    }
                }
                else
                {
                    return Messages.GetTieMessage(firstCard, secondCard);
                }
            }
            else
            {
                if (firstCard.Value > secondCard.Value)
                    return Messages.GetWinMessage(firstCard);
                else return Messages.GetWinMessage(secondCard);
            }

            return result;
        }
    }
}
