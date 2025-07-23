using Cysharp.Threading.Tasks;
using UniRx;

namespace Services.Level
{
    public interface ILevelService
    {
        IReadOnlyReactiveProperty<int> Level { get; }
        IReadOnlyReactiveProperty<ILevelItem> LevelItem { get; }
        int LevelsCount { get; }
        UniTask InitAsync();
        void NextLevel();
        void SetLevel(int level);
    }
}