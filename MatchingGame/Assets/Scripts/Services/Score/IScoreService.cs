using UniRx;

namespace Services.Score
{
    public interface IScoreService
    {
        IReadOnlyReactiveProperty<int> Score { get; }
        void IncreaseScore();
    }
}