using Cysharp.Threading.Tasks;

namespace Services.StateSaver
{
    public interface IStateSaver<T> where T : class, ISaveData, new()
    {
        T State { get; }
        UniTask<T> InitAndLoad();
    }
}