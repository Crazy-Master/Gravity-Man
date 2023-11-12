using UnityEngine;

namespace Core.AudioSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class BgMusic : MonoBehaviour
    {
        [SerializeField] private EMusic music;
        
        public static BgMusic Instance;

        private AudioSource _audioSource;

        private void Start()
        {
            if (Instance) Destroy(gameObject);
            else Instance = this;
            
            _audioSource = GetComponent<AudioSource>();
            SetMusic(music);
        }

        public void SetMusic(EMusic eMusic)
        {
            _audioSource.clip = AudioManager.Instance.GetMusic(eMusic);
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }
}