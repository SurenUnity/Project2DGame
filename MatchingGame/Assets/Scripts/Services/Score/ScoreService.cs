using UniRx;

namespace Services.Score
{
    public class ScoreService : IScoreService
    {
        private IReactiveProperty<int> _score = new ReactiveProperty<int>();
        
        public IReadOnlyReactiveProperty<int> Score => _score;

        public void IncreaseScore()
        {
            _score.Value++;
        }
    }
}