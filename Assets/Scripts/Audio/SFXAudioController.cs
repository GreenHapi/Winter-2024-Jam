using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SfxAudioController : MonoBehaviour
    {
        [Header("Audio Settings")]
        [SerializeField] private float _pitchMin = 0.8f;
        [SerializeField] private float _pitchMax = 1.1f;

        [SerializeField] private AudioClip[] _audioClips;

        private AudioSource _audioSource;
        private float _timeSinceLastPlay;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Play()
        {
            _audioSource.Play();
        }

        public void PlayRandom()
        {
            if (_audioClips.Length == 0)
            {
                return;
            }

            PlayClip(GetRandomClip(), _pitchMin, _pitchMax);
        }

        private void PlayClip(AudioClip clip, float pitchMin, float pitchMax)
        {
            _audioSource.pitch = Random.Range(pitchMin, pitchMax);
            _audioSource.PlayOneShot(clip, _audioSource.volume);
        }

        private AudioClip GetRandomClip()
        {
            int index = Random.Range(0, _audioClips.Length);
            return _audioClips[index];
        }
    }
}