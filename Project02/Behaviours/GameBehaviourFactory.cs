using Project02.Behaviours.Abstractions;
using Project02.Models;
using Project02.Services.Abstractions;

namespace Project02.Behaviours
{
    public class GameBehaviourFactory : IGameBehaviourFactory
    {
        private readonly GameSettings _settings;
        private readonly IDeckService _deckService;
        private readonly ISuitService _suitService;

        public GameBehaviourFactory(GameSettings settings, IDeckService deckService, ISuitService suitService)
        {
            _settings = settings;
            _deckService = deckService;
            _suitService = suitService;
        }

        public IGameBehaviour GetGameBehaviour()
        {
            if (_settings.DeckCount == 1) return new SingleDeckGameBehaviour(_settings, _deckService, _suitService);
            return new MultiDeckGameBehaviour(_settings, _deckService, _suitService);
        }
    }
}
