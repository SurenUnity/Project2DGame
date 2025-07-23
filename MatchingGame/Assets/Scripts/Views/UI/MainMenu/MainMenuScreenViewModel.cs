using Cysharp.Threading.Tasks;
using MVVM.Core.ViewModel;
using Services.Level;
using Services.MatchingGame;
using Services.Score;
using UniRx;
using Views.Layer;
using Views.UI.MatchGameScreen;
using Views.World.Cards;

namespace Views.UI.MainMenu
{
    public interface IMainMenuScreenViewModel : IViewModel
    {
        int LevelsCount { get; }
        IReadOnlyReactiveProperty<int> TotalScore { get; }
        UniTaskVoid SelectLevel(int level);
        void NewGame();
        void Continue();
    }
    
    public class MainMenuScreenViewModel : BaseViewModel, IMainMenuScreenViewModel
    {
        private readonly LevelService _levelService;
        private readonly IScoreService _scoreService;
        private readonly IMatchingGameService _matchingGameService;

        public int LevelsCount => _levelService.LevelsCount;
        public IReadOnlyReactiveProperty<int> TotalScore => _scoreService.TotalScore;

        public MainMenuScreenViewModel(LevelService levelService,
            IScoreService scoreService,
            IMatchingGameService matchingGameService)
        {
            _levelService = levelService;
            _scoreService = scoreService;
            _matchingGameService = matchingGameService;
        }

        public async UniTaskVoid SelectLevel(int level)
        {
            _levelService.SetLevel(level);
            await ViewManager.ShowAsync<CardsView>(LayerNames.World);
            await ViewManager.ShowAsync<MatchGameScreenView>(LayerNames.Screen);
            _matchingGameService.StartGame();
        }

        public void NewGame()
        {
            SelectLevel(1).Forget();
        }

        public void Continue()
        {
            SelectLevel(_levelService.Level.Value).Forget();
        }
        
        public override void Dispose()
        {
        }
    }
}