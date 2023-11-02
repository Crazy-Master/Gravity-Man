using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        #endregion

        #region LoadingScene

        public void LoadScene(string scene)
        {
            OpenWindow(EWindow.LoadingManu).GetComponent<SceneLoader>().LoadSceneAsync(scene);
        }

        private GameObject OpenWindow(EWindow window)
        {
            var obj = Instantiate(_structureWindow.GetWindow(window), _canvas);
            obj.GetComponent<WindowController>().Init(this, _worldController);
            _stackWindows.Peek().SetActive(false);
            _stackWindows.Push(obj);
            return obj;
        }

        #endregion

        private void OnDestroy()
        {
            _worldController.OnGameOver -= GameOver;
        }
    }
}
