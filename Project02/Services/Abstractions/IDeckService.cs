using Project02.Models;

namespace Project02.Services.Abstractions
{
    public interface IDeckService
    {
        Deck CreateDeck();
        Card PlayCard(Deck deck);
    }
}
