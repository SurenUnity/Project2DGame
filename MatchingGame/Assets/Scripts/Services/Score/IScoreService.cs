using UniRx;

namespace Services.Score
{
    public interface IScoreService
    {
        IReadOnlyReactiveProperty<int> Score { get; }
        IReadOnlyReactiveProperty<int> TotalScore { get; }
        void IncreaseTotalScore();
        void IncreaseScore();
    }
}