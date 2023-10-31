using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class SceneLoaderUI : MonoBehaviour
{
    [SerializeField] private Slider progressSlider;
    [SerializeField] private SceneLoader _sceneLoader;

    private void OnEnable()
    {
        _sceneLoader.OnProgressLoad += Loading;
    }

    public void Loading(float progress)
    {
        progressSlider.value = progress;
    }
    
    private void OnDisable()
    {
        _sceneLoader.OnProgressLoad -= Loading;
    }
}
