using Project02.Behaviours;
using Project02.Behaviours.Abstractions;
using Project02.Models;
using Project02.Services.Abstractions;

namespace Project02.Services
{
    public class GameService : IGameService
    {
        private readonly IGameBehaviour _gameBehaviour;

        public GameService(GameSettings settings, IDeckService deckService, ISuitService suitService)
        {
            var gameBehaviourFactory = new GameBehaviourFactory(settings, deckService, suitService);
            _gameBehaviour = gameBehaviourFactory.GetGameBehaviour();
        }

        public string Play()
        {
            return _gameBehaviour.Play();
        }
    }
}
