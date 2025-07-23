using Cysharp.Threading.Tasks;
using MVVM.Core;
using Services.Cards;
using Services.Level;
using Services.MatchingGame;
using Services.Score;
using Views.Layer;
using Views.UI.MainMenu;
using Views.UI.MatchGameScreen;
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
        private readonly ICardsService _cardsService;
        private readonly ILevelService _levelService;
        private readonly IScoreService _scoreService;

        public GameStartupService(IViewManager viewManager,
            ICardsService cardsService,
            ILevelService levelService,
            IScoreService scoreService)
        {
            _viewManager = viewManager;
            _cardsService = cardsService;
            _levelService = levelService;
            _scoreService = scoreService;
        }

        public async UniTaskVoid Startup()
        {
            await _levelService.InitAsync();
            await _scoreService.InitAsync();
            await _cardsService.InitAsync();
            
            await _viewManager.ShowAsync<MainMenuScreenView>(LayerNames.Screen);
        }
    }
}