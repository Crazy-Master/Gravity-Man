using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core.WindowSystem
{
    public class WindowManager : MonoBehaviour
    {
        private StructureWindow _structureWindow;
        private Transform _canvas;
        private Stack<GameObject> _stackWindows = new Stack<GameObject>();
        [SerializeField] private EWindow _firstWindow;
        [SerializeField] private Bootstrap _bootstrap;
        private WorldController _worldController;
        
        [Inject]
        private void Construct(StructureWindow structure) => _structureWindow = structure;

        private void Awake()
        {
            _canvas = gameObject.GetComponent<Canvas>().transform;
            _worldController = _bootstrap.GetWorldController();
            _worldController.OnGameOver += GameOver;
            _worldController.OnLevelComplete += LevelComplete;
            var obj = Instantiate(_structureWindow.GetWindow(_firstWindow),_canvas);
            obj.GetComponent<WindowController>().Init(this, _worldController);
            _stackWindows.Push(obj);
        }

        #region OpenWindows

        public void OpenWindow(GetEWindow window)
        {
            var obj = Instantiate(_structureWindow.GetWindow(window.eWindow), _canvas);
            obj.GetComponent<WindowController>().Init(this, _worldController);
            _stackWindows.Peek().SetActive(false);
            _stackWindows.Push(obj);
        }

        public void CloseWindow()
        {
            if (_stackWindows.Count > 1)
            {
                Destroy(_stackWindows.Pop());
                _stackWindows.Peek().SetActive(true); 
            }
        }

        private void GameOver()
        {
            OpenWindow(EWindow.GameOver);
        }
        
        private void LevelComplete()
        {
            OpenWindow(EWindow.LevelComplete);
        }

        #endregion

        #region LoadingScene

        public void LoadLevel(int level)
        {
            _bootstrap.StartLevel(level);
        }
        
        public void LoadMenu()
        {
            _bootstrap.StartMainMenu();
        }
        public void LoadWindow(string scene)
        {
            
            OpenWindow(EWindow.LoadingMenu).GetComponent<SceneLoader>().LoadSceneAsync(scene);
        }

        private GameObject OpenWindow(EWindow window)
        {
            var obj = Instantiate(_structureWindow.GetWindow(window), _canvas);
            if (window != EWindow.LoadingMenu) obj.GetComponent<WindowController>().Init(this, _worldController);
            _stackWindows.Peek().SetActive(false);
            _stackWindows.Push(obj);
            return obj;
        }

        #endregion

        private void OnDestroy()
        {
            _worldController.OnLevelComplete -= LevelComplete;
            _worldController.OnGameOver -= GameOver;
        }
    }
}
