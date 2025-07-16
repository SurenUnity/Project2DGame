using Cysharp.Threading.Tasks;
using UnityEngine;
using Component = System.ComponentModel.Component;

namespace ClientServices
{
    public interface IAssetsProvider
    {
        UniTask<T> GetPrefab<T>(string name) where T : Component;
        UniTask<GameObject> GetPrefab(string name);
    }
}