using System.Collections.Generic;
using Core.Level;
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
        private int _levelActive;
        private int _numberPlayer;
        private bool _gameMode;
        
        private StructureLoadLevel _structureLL;

        [Inject]
        private void Construct(StructureWindow structure) => _structureWindow = structure;
        
        [Inject]
        private void Construct(StructureLoadLevel structure) => _structureLL = structure;

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

        public void OpenWindow(EWindow window)
        {
            if (_gameMode)
            {
                Time.timeScale = 0;
            }
            var obj = Instantiate(_structureWindow.GetWindow(window), _canvas);
            if (window != EWindow.LoadingMenu) obj.GetComponent<WindowController>().Init(this, _worldController);
            if (_stackWindows.Count != 0) _stackWindows.Peek().SetActive(false);
            _stackWindows.Push(obj);
        }

        public void CloseWindow()
        {
            if (_stackWindows.Count > 1)
            {
                Destroy(_stackWindows.Pop());
                _stackWindows.Peek().SetActive(true);
            }
            if (_stackWindows.Count == 1)
            {
                Time.timeScale = 1;
            }
            
        }

        private void GameOver()
        {
            OpenWindow(EWindow.GameOver);
        }
        
        private void LevelComplete(int star)
        {
            OpenWindow(EWindow.LevelComplete);
            
        }
        
        #endregion

        #region LoadingScene

        public void NextLevel()
        {
            LoadLevel(_levelActive+1, _numberPlayer);
        }
        public void LoadLevel(int level, int numberPlayer)
        {
            _levelActive = level;
            _numberPlayer = numberPlayer;
            DestroyWindow();
            OpenWindow(EWindow.GameMenu);
            _gameMode = true;
            Time.timeScale = 1;
            _worldController.LoadGame(level, numberPlayer);
        }
        
        public void LoadMainMenu()
        {
            DestroyWindow();
            OpenWindow(EWindow.MineMenu);
            _gameMode = false;
            _worldController.LoadMainMenu();
        }

        

        private void DestroyWindow()
        {
            for (int i = 0; i <= _stackWindows.Count; i++)
            {
                Destroy(_stackWindows.Pop());
            }
        }
        #endregion

        private void OnDestroy()
        {
            _worldController.OnLevelComplete -= LevelComplete;
            _worldController.OnGameOver -= GameOver;
        }
    }
}
