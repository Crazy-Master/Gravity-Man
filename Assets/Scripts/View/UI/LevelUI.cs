using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Core.Level
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField] private GameObject _locked;
        [SerializeField] private GameObject _unlock;
        [SerializeField] private Button _button;
        [SerializeField] private Image[] _starSprite = new Image[3];
        [SerializeField] private Sprite _starLite;
        private int _star;

        public void Init(LevelData levelData, WindowController windowController)
        {
            if (levelData.locked == false)
            {
                _button.onClick.AddListener(()=>windowController.LoadSingleLevel(levelData.level));
                _locked.SetActive(false);
                _unlock.SetActive(true);
                _unlock.GetComponent<TextMeshProUGUI>().text = levelData.level.ToString();
                _button.enabled = true;
                for (int i = 0; i < levelData.star; i++)
                {
                    _starSprite[i].sprite = _starLite;
                }
            }
        }
    }
}



