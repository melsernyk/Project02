using System;
using System.Collections.Generic;
using System.Linq;
using Project02.Behaviours.Abstractions;
using Project02.Models;
using Project02.Services.Abstractions;

namespace Project02.Behaviours
{
    public class MultiDeckGameBehaviour : IGameBehaviour
    {
        private readonly List<Deck> _decks = new();
        private readonly GameSettings _settings;

        private readonly IDeckService _deckService;
        private readonly ISuitService _suitService;

        public MultiDeckGameBehaviour(GameSettings settings, IDeckService deckService, ISuitService suitService)
        {
            _settings = settings;
            _deckService = deckService;
            _suitService = suitService;

            for (var i = 0; i < settings.DeckCount; i++)
            {
                var deck = _deckService.CreateDeck();
                _decks.Add(deck);
            }
        }

        public string Play()
        {
            var cards = new List<Card>();
            for (var i = 0; i < _decks.Count; i++)
            {
                var deck = _decks[i];
                var card = _deckService.PlayCard(deck);
                Console.WriteLine($"Card from deck #{i + 1}: {card.Suit}, {card.Value}");

                cards.Add(card);
            }

            var winningCard = cards.First();
            var equalCards = new List<Card> { winningCard };
            foreach (var card in cards.Skip(1))
            {
                if (card.Value > winningCard.Value)
                {
                    winningCard = card;
                }
                else
                {
                    if (card.Value == winningCard.Value)
                        equalCards.Add(card);
                }
            }

            if (!equalCards.Skip(1).Any()) return Messages.GetWinMessage(winningCard);
            else
            {
                if (_settings.AllowTie) return Messages.GetTieMessage(equalCards.ToArray());
                else
                {
                    if (string.IsNullOrEmpty(_settings.SuitPrecedence)) return null;
                    else
                    {
                        winningCard = equalCards.First();
                        foreach (var card in equalCards.Skip(1))
                        {
                            winningCard = _suitService.ResolveTie(card, winningCard);
                        }

                        return Messages.GetWinMessage(winningCard);
                    }
                }
            }
        }
    }
}
