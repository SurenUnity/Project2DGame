using Cysharp.Threading.Tasks;

namespace Services.StateSaver
{
    public interface IStateSaveLoadBehaviour
    {
        public UniTask<T> LoadStateAsync<T>(string stateName) where T : class, new();
        public UniTask SaveStateAsync<T>(string stateName, T stateData);
    }
}