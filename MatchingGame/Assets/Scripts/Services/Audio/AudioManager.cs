using System.Linq;
using UnityEngine;
using Logger = Utils.Logger;

namespace Services.Audio
{
    public class AudioManager : MonoBehaviour, IAudioManager
    {
        [SerializeField] private SFXContainer _sfxContainer;
        [SerializeField] private AudioSource _sfxAudioSource;

        public void PlaySfx(string id)
        {
            var sfx = _sfxContainer.sfxContainersData.FirstOrDefault(v => v.id == id);
            if (sfx == null)
            {
                Logger.LogError($"Didn't find audio clip by id: {id}");
                return;
            }
            _sfxAudioSource.PlayOneShot(sfx.clip);
        }
    }
}