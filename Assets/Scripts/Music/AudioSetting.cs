using UnityEngine;

namespace Core.AudioSystem
{
    public class AudioSetting
    {
        public void Init()
        {
            if (!PlayerPrefs.HasKey("Sound"))
            {
                SaveSound(true);
                SaveMusic(true);
            }
        }
        public bool LoadSound() => PlayerPrefs.GetInt("Sound") == 1;
        public bool LoadMusic() => PlayerPrefs.GetInt("Music") == 1;
        public void SaveSound(bool state) => PlayerPrefs.SetInt("Sound", state ? 1 : 0);
        public void SaveMusic(bool state) => PlayerPrefs.SetInt("Music", state ? 1 : 0);
    }
}