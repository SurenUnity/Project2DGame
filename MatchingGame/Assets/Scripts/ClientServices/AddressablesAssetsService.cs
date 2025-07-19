using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Component = System.ComponentModel.Component;

namespace ClientServices
{
    public class AddressablesAssetsService : IAssetsProvider
    {
        private Dictionary<string, Sprite> _sprites = new();
        
        private CancellationTokenSource _loadAssetsCts;
        
        public AddressablesAssetsService()
        {
            Addressables.InitializeAsync();
        }
        
        public async UniTask<T> GetPrefab<T>(string name) where T : Component
        {
            var prefab = await Addressables.LoadAssetAsync<GameObject>(name);
            var component = prefab.GetComponent<T>();
            return component;
        }

        public async UniTask<GameObject> GetPrefab(string name)
        {
            return await Addressables.LoadAssetAsync<GameObject>(name);
        }

        public async UniTask<Sprite> GetSprite(string name)
        {
            if (!_sprites.TryGetValue(name, out var sprite))
            {
                sprite = await Addressables.LoadAssetAsync<Sprite>(name);
                _sprites.Add(name, sprite);
            }

            return sprite;
        }
    }
}