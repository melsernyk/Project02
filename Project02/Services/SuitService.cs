using System.Collections.Generic;
using Project02.Models;
using Project02.Services.Abstractions;

namespace Project02.Services
{
    public class SuitService : ISuitService
    {
        private readonly GameSettings _settings;

        public SuitService(GameSettings settings)
        {
            _settings = settings;
        }

        public Card ResolveTie(Card firstCard, Card secondCard)
        {
            var suits = _settings.SuitPrecedence.Split(",");
            var suitPrecedence = new Dictionary<string, int>();
            for (var i = 0; i < suits.Length; i++)
            {
                suitPrecedence.Add(suits[i], i);
            }

            var winningCard = firstCard;
            if (suitPrecedence[secondCard.Suit.ToString()] > suitPrecedence[firstCard.Suit.ToString()])
                winningCard = secondCard;
            return winningCard;
        }
    }
}
