using UniRx;

namespace Services.Score
{
    public class ScoreService : IScoreService
    {
        private IReactiveProperty<int> _score = new ReactiveProperty<int>();
        private IReactiveProperty<int> _totalScore = new ReactiveProperty<int>();
        
        public IReadOnlyReactiveProperty<int> Score => _score;
        public IReadOnlyReactiveProperty<int> TotalScore => _totalScore; 

        public void IncreaseScore()
        {
            _score.Value++;
        }

        public void IncreaseTotalScore()
        {
            _totalScore.Value += _score.Value;
            _score.Value = 0;
        }
    }
}