using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 public class SpawnSprite : MonoBehaviour
{
    [SerializeField] private Image _segment1;
    [SerializeField] private Image _segment2;
    [SerializeField] private Image _segment3;
    [SerializeField] private Image _segment4;
    [SerializeField] private Image _segment5;
    [SerializeField] private Image _segment6;
    [SerializeField] private Image _segment7;
    [SerializeField] private Image _segment8;


    public PickRandomBoost _pickRanomBoost;

    public void Awake()
    {
        _segment1.sprite = _pickRanomBoost.items[0].sprite;
        _segment2.sprite = _pickRanomBoost.items[1].sprite;
        _segment3.sprite = _pickRanomBoost.items[2].sprite;
        _segment4.sprite = _pickRanomBoost.items[3].sprite;
        _segment5.sprite = _pickRanomBoost.items[4].sprite;
        _segment6.sprite = _pickRanomBoost.items[5].sprite;
        _segment7.sprite = _pickRanomBoost.items[6].sprite;
        _segment8.sprite = _pickRanomBoost.items[7].sprite;
    }
}
