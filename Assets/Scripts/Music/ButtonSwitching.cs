using System;
using Core.AudioSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ButtonSwitching: MonoBehaviour
    {
        [SerializeField] private ESoundType type;
        [SerializeField] private Image stateImage;
        [SerializeField] private Sprite soundOn;
        [SerializeField] private Sprite soundOff;

        private AudioSetting _setting = new AudioSetting();
        private bool _state;

        private void Start()
        {
            LoadState();
            SwitchingImage();
            SwitchingSoundVolume();
        }

        private void LoadState()
        {
            switch (type)
            {
                case ESoundType.Sound:
                    _state = _setting.LoadSound();
                    break;
                
                case ESoundType.Music:
                    _state = _setting.LoadMusic();
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SaveState()
        {
            switch (type)
            {
                case ESoundType.Sound:
                    _setting.SaveSound(_state);
                    break;
                
                case ESoundType.Music:
                    _setting.SaveMusic(_state);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public void Switching()
        {
            _state = !_state;

            SwitchingImage();
            SaveState();
            SwitchingSoundVolume();
        }

        private void SwitchingSoundVolume()
        {
            switch (type)
            {
                case ESoundType.Sound:
                    AudioManager.Instance.SetVolumeSound(_state ? 0 : -80);
                    break;
                
                case ESoundType.Music:
                    AudioManager.Instance.SetVolumeMusic(_state ? 0 : -80);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void SwitchingImage() => stateImage.sprite = _state ? soundOn : soundOff;
    }
}