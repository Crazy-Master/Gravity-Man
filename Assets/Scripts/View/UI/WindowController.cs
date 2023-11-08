using Core.WindowSystem;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    private WindowManager _windowManager;
    private WorldController _worldController;

    public void Init(WindowManager windowManager, WorldController worldController)
    {
        _windowManager = windowManager;
        _worldController = worldController;
    }

    public void OpenWindow(GetEWindow eWindow)
    {
        _windowManager.OpenWindow(eWindow);
    }

    public void CloseWindow()
    {
        _windowManager.CloseWindow();
    }
    
    public void NextLevel()
    {
        _windowManager.NextLevel();
    }
    
    public void LoadSingleLevel(int level)
    {
        _windowManager.LoadLevel(level, 1);
    }
    public void LoadMultiplayerLevel()
    {
        _windowManager.LoadLevel(1, 2);
    }
    
    public void LoadMainMenu()
    {
        _windowManager.LoadMenu();
    }
    public void Restart()
    {
        _windowManager.CloseWindow();
        _worldController.Restart();
    }
    public void Resurrect()
    {
        _windowManager.CloseWindow();
        _worldController.Resurrect();
    }

    public void TapSwapGravity(int numberPlayer)
    {
        _worldController.SwapGravity(numberPlayer);
    }
}
