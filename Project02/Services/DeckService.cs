using System;
using Project02.Models;
using Project02.Services.Abstractions;

namespace Project02.Services
{
    public class DeckService : IDeckService
    {
        private readonly Random _random = new Random();
        private readonly GameSettings _settings;

        public DeckService(GameSettings settings)
        {
            _settings = settings;
        }

        public Deck CreateDeck()
        {
            var deck = new Deck();
            var cardSuit = Suit.Diamonds;
            var cardCountPerSuit = _settings.DeckCardCount / 4;
            for (var i = 0; i < _settings.DeckCardCount; i++)
            {
                if (i >= cardCountPerSuit && i % cardCountPerSuit == 0)
                    cardSuit = (Suit)((int)cardSuit + 1);

                deck.Cards.Add(new Card(i % cardCountPerSuit + 1, cardSuit));
            }

            if (_settings.AllowWildcard)
                deck.Cards.Add(new Card(101, Suit.Unknown));

            return deck;
        }

        public Card PlayCard(Deck deck)
        {
            var index = _random.Next(deck.Cards.Count);
            return deck.Cards[index];
        }
    }
}
