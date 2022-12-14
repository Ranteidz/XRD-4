using System;
using System.Linq;
using UnityEngine;

namespace _scripts.Audio
{
    public class BaseAudioController : MonoBehaviour
    {
        [SerializeField] private SoundAndName[] _sounds = { };

        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayOnce(string clipName, float volumeModifier = 0)
        {
            var audioClip = _sounds.FirstOrDefault(clip =>
                string.Equals(clip.SoundName, clipName, StringComparison.CurrentCultureIgnoreCase)).AudioClip;
            if (audioClip != null)
            {
                _audioSource.volume = 1 - volumeModifier;
                _audioSource.PlayOneShot(audioClip);
            }
            else
            {
                Debug.LogError($"Clip with name {clipName} is not registered!");
            }
        }

        public void StopPlayingSound()
        {
            _audioSource.Stop();
        }

        [Serializable]
        public struct SoundAndName
        {
            public string SoundName;
            public AudioClip AudioClip;
        }
    }
}