using System;
using System.Linq;
using UnityEngine;

namespace MVVM
{
    [Serializable]
    public class ViewLayerModel
    {
        [field: SerializeField] public ViewLayer[] Layers { get; private set; }

        public Transform GetLayer(string layerName)
        {
            return Layers.FirstOrDefault(v => v.Name == layerName)?.LayerTransform;
        }
    }
}