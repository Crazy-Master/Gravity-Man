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
        
        [Inject]
        private void Construct(StructureWindow structure) => _structureWindow = structure;

        private void Awake()
        {
            _canvas = gameObject.GetComponent<Canvas>().transform; 
            
            var obj = Instantiate(_structureWindow.GetWindow(_firstWindow),_canvas);
            obj.GetComponent<WindowController>().Init(this, _bootstrap);
            _stackWindows.Push(obj);
        }

        #region OpenWindows

        public void OpenWindow(GetEWindow window)
        {
            var obj = Instantiate(_structureWindow.GetWindow(window.eWindow), _canvas);
            obj.GetComponent<WindowController>().Init(this, _bootstrap);
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

        private GameObject OpenWindow(EWindow window)
        {
            var obj = Instantiate(_structureWindow.GetWindow(window), _canvas);
            _stackWindows.Peek().SetActive(false);
            return obj;
        }

        #endregion

    }
}
