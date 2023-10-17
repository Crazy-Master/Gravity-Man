using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace Core.AudioSystem
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        
        [SerializeField] private AudioDataBase dataBase;
        [SerializeField] private AudioMixerGroup mixer;

        private AudioSetting _setting;
        
        private void Awake()
        {
            if (Instance) Destroy(gameObject);
            else Instance = this;
            _setting = new AudioSetting();
        }

        private void Start()
        {
            SetVolumeSound(_setting.LoadSound() ? 0 : -80);
            SetVolumeMusic(_setting.LoadMusic() ? 0 : -80);
        }

        public void SetVolumeSound(float value)
        {
            mixer.audioMixer.SetFloat("SoundSetting", value);
        }
        public void SetVolumeMusic(float value)
        {
            mixer.audioMixer.SetFloat("MusicSetting", value);
        }

        /// <summary>
        /// Блок для старта рекламы
        /// "Используется в Яндекс играх"
        /// </summary>
        public void StartReward()
        {
            mixer.audioMixer.SetFloat("SoundSetting", -80);
            mixer.audioMixer.SetFloat("MusicSetting", -80);
        }

        /// <summary>
        /// Блок для завершения рекламы
        /// "Используется в Яндекс играх"
        /// </summary>
        public void EndReward()
        {
            SetVolumeSound(_setting.LoadSound() ? 0 : -80);
            SetVolumeMusic(_setting.LoadMusic() ? 0 : -80);
        }
        
        public AudioClip GetMusic(EMusic music) => (from t in dataBase.SettingMusic where t.eMusic == music select t.musicClip).FirstOrDefault();
        public AudioClip GetSound(ESound sound) => (from t in dataBase.SettingSounds where t.eSound == sound select t.soundClip).FirstOrDefault();
    }
}
