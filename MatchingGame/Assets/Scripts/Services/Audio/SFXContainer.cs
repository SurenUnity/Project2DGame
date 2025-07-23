using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Services.Audio
{
    [CreateAssetMenu(fileName = "SfxContainer", menuName = "Audio/SFXContainer", order = 0)]
    public class SFXContainer : ScriptableObject
    {
        public SFXContainerData[] sfxContainersData;
    }

    [Serializable]
    public class SFXContainerData
    {
        public string id;
        public AudioClip clip;
    }
}