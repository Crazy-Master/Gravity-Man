using Core.WindowSystem;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    private WindowManager _windowManager;

    public void Init(WindowManager windowManager)
    {
        _windowManager = windowManager;
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
        _windowManager.LoadScene();
    }
}
