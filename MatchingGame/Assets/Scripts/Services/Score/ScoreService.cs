using Cysharp.Threading.Tasks;
using Services.StateSaver;
using States.Score;
using UniRx;

namespace Services.Score
{
    public class ScoreService : IScoreService
    {
        private readonly IStateSaver<ScoreStateModel> _stateSaver;

        private ScoreStateModel _scoreStateModel;
        
        private IReactiveProperty<int> _score = new ReactiveProperty<int>();
        private IReactiveProperty<int> _totalScore = new ReactiveProperty<int>();
        
        public IReadOnlyReactiveProperty<int> Score => _score;
        public IReadOnlyReactiveProperty<int> TotalScore => _totalScore;

        public ScoreService(IStateSaver<ScoreStateModel> stateSaver)
        {
            _stateSaver = stateSaver;
        }

        public async UniTask InitAsync()
        {
            await LoadState();
        }

        private async UniTask LoadState()
        {
            _scoreStateModel = await _stateSaver.InitAndLoad();
            _totalScore.Value = _scoreStateModel.TotalScore;
        }
        
        public void IncreaseScore()
        {
            _score.Value++;
        }

        public void IncreaseTotalScore()
        {
            _totalScore.Value += _score.Value;
            _scoreStateModel.TotalScore = _totalScore.Value;
            _score.Value = 0;
        }
    }
}