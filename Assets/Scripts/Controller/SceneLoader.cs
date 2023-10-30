using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public event Action<float> OnProgressLoad;
    public void LoadSceneAsync(string sceneNumber)
    {
        StartCoroutine(LoadSceneAsyncCoroutine(sceneNumber));
    }

    private IEnumerator LoadSceneAsyncCoroutine(string sceneNumber)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneNumber);

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f); // Нормализуем прогресс загрузки
            OnProgressLoad?.Invoke(progress);

            yield return null;
        }
    }
}