using UnityEngine;

namespace Core.AudioSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class BgMusic : MonoBehaviour
    {
        [SerializeField] private EMusic music;

        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = AudioManager.Instance.GetMusic(music);
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }
}