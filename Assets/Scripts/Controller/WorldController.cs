using System;
using UnityEngine;

public class WorldController
{
    private GameObject _player;
    
    public int ActiveLevel;
    public event Action OnRestartGame;
    public event Action OnResurrectGame;
    public event Action OnLevelComplete;
    public event Action OnGameOver;
    
    public event Action<int, int> OnLoadGame;
    
    public event Action OnLoadMainMenu;
    
    public event Action<int> OnSwapGravity;

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
    public void LevelComplete()
    {
        OnLevelComplete?.Invoke();
    }

    public void LoadGame(int level, int numberPlayer)
    {
        OnLoadGame?.Invoke(level, numberPlayer);
    }
    
    public void LoadMainMenu()
    {
        OnLoadMainMenu?.Invoke();
    }

    public void SwapGravity(int numberPlayer)
    {
        OnSwapGravity?.Invoke(numberPlayer);
    }
    
}
