using Project02.Models;

namespace Project02.Services.Abstractions
{
    public interface ISuitService
    {
        Card ResolveTie(Card firstCard, Card secondCard); 
    }
}
