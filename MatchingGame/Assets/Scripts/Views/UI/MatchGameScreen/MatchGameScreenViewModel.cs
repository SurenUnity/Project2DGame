using Cysharp.Threading.Tasks;
using MVVM.Core.ViewModel;
using Services.Level;
using Services.MatchingGame;
using Services.Score;
using UniRx;
using Views.Layer;
using Views.UI.Core;
using Views.UI.LosePopup;
using Views.UI.WinPopup;

namespace Views.UI.MatchGameScreen
{
    public interface IMatchGameScreenViewModel : IViewModel
    {
        IReadOnlyReactiveProperty<int> Score { get; }
        IReadOnlyReactiveProperty<int> Level { get; }
        IReadOnlyReactiveProperty<int> TriesLeft { get; }
    }
    
    public class MatchGameScreenViewModel : BaseViewModel, IMatchGameScreenViewModel
    {
        private readonly IMatchingGameService _matchingGameService;
        private readonly IScoreService _scoreService;
        private readonly ILevelService _levelService;

        public IReadOnlyReactiveProperty<int> Score => _scoreService.Score;
        public IReadOnlyReactiveProperty<int> Level => _levelService.Level;
        public IReadOnlyReactiveProperty<int> TriesLeft => _matchingGameService.TriesLeftCount;

        public MatchGameScreenViewModel(IMatchingGameService matchingGameService,
            IScoreService scoreService,
            ILevelService levelService)
        {
            _matchingGameService = matchingGameService;
            _scoreService = scoreService;
            _levelService = levelService;
            _matchingGameService.OnWin += OnWin;
            _matchingGameService.OnLose += OnLose;
        }

        private void OnLose()
        {
            OpenLosePopup().Forget();
        }

        private void OnWin()
        {
            OpenWinPopup().Forget();
        }

        private async UniTaskVoid OpenWinPopup()
        {
            await ViewManager.ShowAsync<WinPopupView>(LayerNames.Popup);
        }

        private async UniTaskVoid OpenLosePopup()
        {
            await ViewManager.ShowAsync<LosePopupView>(LayerNames.Popup);
        }

        public override void Dispose()
        {
            _matchingGameService.OnWin -= OnWin;
        }
    }
}