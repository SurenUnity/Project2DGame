using System;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Utils;

namespace Services.StateSaver
{
    public class JsonStateSaveLoadBehaviour : IStateSaveLoadBehaviour
    {
        private readonly string _directoryName = "SaveData";
        private CancellationTokenSource _cancellationTokenSource = new();
        
        public async UniTask<T> LoadStateAsync<T>(string stateName) where T : class, new()
        {
            var filePath = GetFileFullPath(stateName);
            
            if (File.Exists(filePath) == false)
            {
                return GetDefault();
            }

            T result;
            var dataStr = await File.ReadAllTextAsync(filePath, _cancellationTokenSource.Token);
            try
            {
                result = JsonConvert.DeserializeObject<T>(dataStr);
            }
            catch (Exception e)
            {
                Logger.LogExceptionWithParams($"Unable to deserialize storage {typeof(T).FullName}", e);
                return GetDefault();
            }

            if (result == null)
            {
                return GetDefault();
            }
            
            return result;
            
            T GetDefault() => new();
        }

        public async UniTask SaveStateAsync<T>(string stateName, T stateData)
        {
            var filePath = GetFileFullPath(stateName);

            try
            {
                var jsonStr = JsonConvert.SerializeObject(stateData);
                await File.WriteAllTextAsync(filePath, jsonStr, _cancellationTokenSource.Token);
            }
            catch (Exception e)
            {
                Logger.LogExceptionWithParams($"Unable to serialize storage {typeof(T).FullName}", e);
            }
        }
        private string GetFileFullPath(string fileName)
        {
            PersistentPathUtility.TryCreateDirectory(_directoryName);
            return PersistentPathUtility.GetFilePath(Path.Combine(_directoryName, fileName));
        }
    }
}