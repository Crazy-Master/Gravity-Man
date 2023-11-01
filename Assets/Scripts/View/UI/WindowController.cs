using Core.WindowSystem;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    private WindowManager _windowManager;
    private Bootstrap _bootstrap;

    public void Init(WindowManager windowManager, Bootstrap bootstrap)
    {
        _windowManager = windowManager;
        _bootstrap = bootstrap;
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
        _windowManager.LoadScene(scene);
    }
    
    public void Restart()
    {
        _windowManager.CloseWindow();
        _bootstrap.Restart();
    }
    public void Resurrect()
    {
        _windowManager.CloseWindow();
        _bootstrap.Resurrect();
    }
}
