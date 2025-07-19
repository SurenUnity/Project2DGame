using Cysharp.Threading.Tasks;
using MVVM.Core;
using Services.MatchingGame;
using Views.Layer;
using Views.World.Cards;

namespace Services
{
    public interface IGameStartupService
    {
        UniTaskVoid Startup();
    }
    
    public class GameStartupService : IGameStartupService
    {
        private readonly IViewManager _viewManager;
        private readonly IMatchingGameService _matchingGameService;

        public GameStartupService(IViewManager viewManager,
            IMatchingGameService matchingGameService)
        {
            _viewManager = viewManager;
            _matchingGameService = matchingGameService;
        }

        public async UniTaskVoid Startup()
        {
            await _viewManager.ShowAsync<CardsView>(LayerNames.World);
            _matchingGameService.StartGame();
        }
    }
}