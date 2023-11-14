using System;
using UnityEngine;

public class WorldController
{
    private GameObject[] _players;
    
    public int ActiveLevel;
    public int Stars { get; private set; }
    public event Action OnRestartGame;
    public event Action OnResurrectGame;
    public event Action<int> OnLevelComplete;
    public event Action OnGameOver;
    
    public event Action<int, int> OnLoadGame;
    
    public event Action OnLoadMainMenu;
    
    public event Action<int> OnSwapGravity;

    public void SetStars()
    {
        Debug.Log("star");
        if (Stars != 3) Stars++;
    }
    public void SetPlayers(GameObject[] players)
    {
        _players = players;
    }
    public void Restart()
    {
        foreach (var player in _players)
        {
            player.SetActive(true);
        }
        OnRestartGame?.Invoke();
    }
    
    public void Resurrect()
    {
        foreach (var player in _players)
        {
            player.SetActive(true);
        }
        OnResurrectGame?.Invoke();
        
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
    }
    public void LevelComplete()
    {
        OnLevelComplete?.Invoke(Stars);
    }

    public void LoadGame(int level, int numberPlayer)
    {
        ActiveLevel = level;
        OnLoadGame?.Invoke(level, numberPlayer);
        Stars = 0;
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
