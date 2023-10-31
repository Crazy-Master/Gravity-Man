using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Core.WindowSystem
{
    public class WindowManager : MonoBehaviour
    {
        private WindowDataBase _windowDataBase;
        private Transform _canvas;
        private Stack<GameObject> _stackWindows = new Stack<GameObject>();
        [SerializeField] private EWindow _firstWindow;
        
        [Inject]
        private void Construct(WindowDataBase structure) => _windowDataBase = structure;

        private void Awake()
        {
            _canvas = gameObject.GetComponent<Canvas>().transform; 
            Init();
        }

        private void Init()
        {
            var obj = Instantiate(_windowDataBase.GetWindow(_firstWindow),_canvas);
            obj.GetComponent<WindowController>().Init(this);
            _stackWindows.Push(obj);
        }

        #region OpenWindows

        public void OpenWindow(GetEWindow window)
        {
            var obj = Instantiate(_windowDataBase.GetWindow(window.eWindow), _canvas);
            obj.GetComponent<WindowController>().Init(this);
            _stackWindows.Peek().SetActive(false);
            _stackWindows.Push(obj);
        }

        public void CloseWindow()
        {
            Destroy(_stackWindows.Pop());
            _stackWindows.Peek().SetActive(true);
        }

        #endregion

        #region LoadingScene

        public void LoadScene(string scene)
        {
            OpenWindow(EWindow.LoadingManu).GetComponent<SceneLoader>().LoadSceneAsync(scene);
        }

        public void LoadScene()
        {
            var scene = SceneManager.GetActiveScene().name;
            OpenWindow(EWindow.LoadingManu).GetComponent<SceneLoader>().LoadSceneAsync(scene);
        }

        private GameObject OpenWindow(EWindow window)
        {
            var obj = Instantiate(_windowDataBase.GetWindow(window), _canvas);
            _stackWindows.Peek().SetActive(false);
            return obj;
        }

        #endregion
        
    }
}
