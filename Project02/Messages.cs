using Project02.Models;

namespace Project02
{
    public static class Messages
    {
        public static string GetTieMessage(params Card[] cards)
        {
            var message = string.Empty;
            for (int i = 0; i < cards.Length; i++)
            {
                var card = cards[i];
                message += $"Card #{i+1}: {card.Suit}, {card.Value}; ";
            }
            return $"Tie. {message.Trim()}";
        }

        public static string GetWinMessage(Card card)
        {
            if (card.Suit == Suit.Unknown) return "Winning card: Wildcard";
            return $"Winning card: {card.Suit}, {card.Value}";
        }
    }
}
