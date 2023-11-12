using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

[Serializable]
public struct LanguageData
{
	public Sprite spritesFlag;
	public string language;
}

public class LanguageSwitching : MonoBehaviour
{
	
	
    [SerializeField] private LanguageData[] _languageDatas = new LanguageData[3]; //0 - россия,1 - англия, 2 - турция
    [SerializeField] private Image _image;

    private string _languageSave;
    private void Start()
    {
	    _languageSave = YandexGame.savesData.language;
	    Switching(_languageSave);
    }

    public void ButtonSwitching()
    {
	    for (int i = 0; i < _languageDatas.Length; i++)
	    {
		    if (_languageDatas[i].language == _languageSave)
		    {
			    if (i == _languageDatas.Length-1) 
				    YandexGame.SwitchLanguage(_languageDatas[0].language);
			    else 
				    YandexGame.SwitchLanguage(_languageDatas[i+1].language);
			    return;
		    }
	    }
	    
    }
    
    private void Switching(string lang)
    {
	    _languageSave = YandexGame.savesData.language;
	    for (int i = 0; i < _languageDatas.Length; i++)
	    {
		    if (_languageDatas[i].language == lang)
		    {
			    _image.sprite = _languageDatas[i].spritesFlag;
			    return;
		    }
	    }
    }
    
    private void OnEnable() => YandexGame.SwitchLangEvent += Switching;
    private void OnDisable() => YandexGame.SwitchLangEvent -= Switching;
    
}
