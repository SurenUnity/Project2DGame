using Cysharp.Threading.Tasks;
using UniRx;

namespace Services.Score
{
    public interface IScoreService
    {
        IReadOnlyReactiveProperty<int> Score { get; }
        IReadOnlyReactiveProperty<int> TotalScore { get; }
        UniTask InitAsync();
        void IncreaseTotalScore();
        void IncreaseScore();
    }
}