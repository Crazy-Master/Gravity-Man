using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteUI : MonoBehaviour
{
    [SerializeField] private Image[] _stars = new Image[3];
    [SerializeField] private Sprite _starLite;
    [SerializeField] private WindowController _windowController;

    public void Start()
    {
        var star = _windowController.worldController.Stars;
        for (int i = 0; i < star; i++)
        {
            _stars[i].sprite = _starLite;
        }
    }
}
