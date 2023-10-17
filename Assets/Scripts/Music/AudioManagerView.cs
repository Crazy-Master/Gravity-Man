using UnityEngine;

namespace Core.AudioSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManagerView : MonoBehaviour
    {
        public static AudioManagerView Instance;
        
        private AudioSource _audioSource;

        private void Awake()
        {
            if (Instance) Destroy(gameObject);
            else Instance = this;
            
            _audioSource = GetComponent<AudioSource>();
            _audioSource.loop = false;
            _audioSource.playOnAwake = false;
        }

        public void PlaySound(ESound sound)
        {
            _audioSource.clip = AudioManager.Instance.GetSound(sound);
            _audioSource.Play();
        }
    }
}
