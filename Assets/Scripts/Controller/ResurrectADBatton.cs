using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ResurrectADBatton : MonoBehaviour
{
    [SerializeField] private Button _buttonResurrect;
    [SerializeField] private int _idAD;

    private void Start()
    {
        _buttonResurrect.onClick.AddListener(()=>YandexGame.RewVideoShow(_idAD));
    }
}
