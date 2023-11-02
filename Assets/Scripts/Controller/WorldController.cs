using System;
using UnityEngine;

public class WorldController
{
    private GameObject _player;
    public event Action OnRestartGame;
    public event Action OnResurrectGame;
    public event Action OnGameOver;

    public void Init(GameObject player)
    {
        _player = player;
    }
    public void Restart()
    {
        _player.SetActive(true);
        OnRestartGame?.Invoke();
    }
    
    public void Resurrect()
    {
        _player.SetActive(true);
        OnResurrectGame?.Invoke();
        
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
    }
}
