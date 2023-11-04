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
    
    public void LoadScene(string scene)
    {
        //_windowManager.LoadScene(scene);
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
}
