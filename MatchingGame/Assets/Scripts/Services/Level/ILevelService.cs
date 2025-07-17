using UniRx;

namespace Services.Level
{
    public interface ILevelService
    {
        IReadOnlyReactiveProperty<int> Level { get; }
        IReadOnlyReactiveProperty<ILevelItem> LevelItem { get; }
        void NextLevel();
    }
}