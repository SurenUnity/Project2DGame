using System;
using ClientServices;
using Cysharp.Threading.Tasks;
using Utils;

namespace Services.StateSaver
{
    public class StateSaver<T> : IDisposable, IStateSaver<T> where T : class, ISaveData, new()
    {
        private readonly IApplicationService _applicationService;
        
        private IStateSaveLoadBehaviour _saveLoadBehaviour;
        private string _fileName;
        
        public T State { get; private set; }

        public StateSaver(IApplicationService applicationService, IStateSaveLoadBehaviour saveLoadBehaviour)
        {
            _applicationService = applicationService;
            _saveLoadBehaviour = saveLoadBehaviour;
            _fileName = typeof(T).Name;

            _applicationService.ApplicationQuited += OnApplicationQuite;
            _applicationService.ApplicationFocusChanged += OnApplicationFocus;
        }
        
        public async UniTask<T> InitAndLoad()
        {
            return await LoadAsync();
        }
        
        private async UniTask<T> LoadAsync()
        {
            try
            {
                State = await _saveLoadBehaviour.LoadStateAsync<T>(_fileName);
            }
            catch (Exception e)
            {
                Logger.LogError($"Failed to load state: {typeof(T)}{e.Message}");
            }
            finally
            {
                State ??= new T();
            }

            return State;
        }
        
        private void OnApplicationFocus(bool isFocus)
        {
            if (!isFocus)
            {
                SaveAsync().Forget();
            }
        }

        private void OnApplicationQuite()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            SaveAsync().Forget();
#endif
        }
        
        private async UniTaskVoid SaveAsync()
        {
            if (State == null)
            {
                Logger.Log($"State Saver: Couldn't save, state is null. State type: {typeof(T)}");
                return;
            }

            try
            {
                await _saveLoadBehaviour.SaveStateAsync(_fileName, State);
            }
            catch (Exception e)
            {
                Logger.LogWarning($"Exception occured while saving state: {e}");
            }
        }
        
        public void Dispose()
        {
            _applicationService.ApplicationQuited -= OnApplicationQuite;
            _applicationService.ApplicationFocusChanged -= OnApplicationFocus;
        }
    }
}