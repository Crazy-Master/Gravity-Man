using System;
using UnityEngine;
using UnityEngine.UI;

public class StarUI : MonoBehaviour
{
    [SerializeField] private Sprite _starOn;
    [SerializeField] private Sprite _starOff;
    [SerializeField] private Image[] _image;

    public void SetStar(int quantity)
    {
        for (int i = 0; i < _image.Length; i++)
        {
            if (i<quantity)
            {
                _image[i].sprite = _starOn;
            }
            else
            {
                _image[i].sprite = _starOff;
            }
            
        }
    }
}
