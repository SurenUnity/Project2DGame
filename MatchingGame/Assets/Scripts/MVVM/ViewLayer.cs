using System;
using UnityEngine;

namespace MVVM
{
    [Serializable]
    public class ViewLayer
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Transform LayerTransform { get; private set; }
    }
}