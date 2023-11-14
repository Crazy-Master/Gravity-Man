using Core.AudioSystem;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using View.Background;
using Zenject;


public class Bootstrap : MonoBehaviour
{
    private StructureLoadLevel _structureLL;
    [SerializeField] private GameObject _prefabPlayer;
    [SerializeField] private Camera _camera;
    [SerializeField] private BackgroundCreator _backgroundCreator;
    private LevelManager _levelManager;
    private WorldController _worldController = new WorldController();
    private SettingLevel _settingLevel;

    private int _numberPlayer = 1;
    private GameObject _structureLevel;
    private GameObject _player;
    private GameObject _playerTwo;
    private GameObject _enemy;
    
    private int level = 1; //загрузка из яндекса

    [Inject]
    private void Construct(StructureLoadLevel structure) => _structureLL = structure;

    private void Start()
    {
        _levelManager = new LevelManager(_worldController);

        StartMainMenu();
        _worldController.OnLoadGame += StartLevel;
        _worldController.OnLoadMainMenu += StartMainMenu;
    }

    public void StartMainMenu()
    {
        DestroyLevel();
        _backgroundCreator.Restart(EScene.MenuScene);
        BgMusic.Instance.SetMusic(EMusic.MainMenu);
    }

    public void StartLevel(int level, int numberPlayer)
    {
        DestroyLevel();
        _settingLevel = _structureLL.GetLevel(level);
        _backgroundCreator.Restart(_settingLevel.BackgroundScene);
        GenerationLevel();
        GenerationPlayer(_settingLevel.posPlayer, _settingLevel.gravity, _settingLevel.speed);
        BgMusic.Instance.SetMusic(EMusic.Game);
    }
    private void GenerationLevel()
    {
        _structureLevel = new GameObject("StructureLevel");
        Instantiate(_settingLevel.grid).transform.SetParent(_structureLevel.transform);
        Instantiate(_settingLevel.decor).transform.SetParent(_structureLevel.transform);
    }

    private void GenerationPlayer(Vector3 startPos, float gravity, float speed)
    {
        GameObject[] players = new GameObject[_numberPlayer];
        for (int i = 0; i < _numberPlayer; i++)
        {
            _player = Instantiate(_prefabPlayer,startPos, Quaternion.identity);
            var playerController = _player.GetComponent<PlayerController>();
            playerController.Init(_worldController, gravity);
            _player.GetComponent<PlayerMove>().Init(speed, i, playerController, _worldController);
            _camera.AddComponent<CameraMove>().Init(_player.transform);

            players[i] = _player;
        }
        _worldController.SetPlayers(players);
    }

   

        private void DestroyLevel()
    {
        if (_structureLevel != null) Destroy(_structureLevel);
        if (_player != null) Destroy(_player);
        if (_playerTwo != null) Destroy(_player);
    }

    public WorldController GetWorldController()
    {
        return _worldController;
    }

    private void OnDestroy()
    {
        _worldController.OnLoadGame -= StartLevel;
        _worldController.OnLoadMainMenu -= StartMainMenu;
    }
}
