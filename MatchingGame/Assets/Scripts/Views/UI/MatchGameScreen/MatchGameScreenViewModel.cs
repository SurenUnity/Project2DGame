using MVVM.Core.ViewModel;
using Services.Level;
using Services.MatchingGame;
using Services.Score;
using UniRx;
using Views.UI.Core;

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
        }

        public override void Dispose()
        {
        }
    }
}