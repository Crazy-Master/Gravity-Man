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
    }

    public void StartLevel(int level, int numberPlayer)
    {
        DestroyLevel();
        _settingLevel = _structureLL.GetLevel(level);
        _backgroundCreator.Restart(_settingLevel.BackgroundScene);
        GenerationLevel();
        GenerationPlayer(_settingLevel.posPlayer, _settingLevel.gravity, _settingLevel.speed);
    }
    private void GenerationLevel()
    {
        _structureLevel = new GameObject("StructureLevel");
        Instantiate(_settingLevel.grid).transform.SetParent(_structureLevel.transform);
        Instantiate(_settingLevel.decor).transform.SetParent(_structureLevel.transform);
    }

    private void GenerationPlayer(Vector3 startPos, float gravity, float speed)
    {
        _player = Instantiate(_prefabPlayer,startPos, Quaternion.identity);
        var playerController = _player.GetComponent<PlayerController>();
        playerController.Init(_worldController, gravity);
        _player.GetComponent<PlayerMove>().Init(speed, 1, playerController, _worldController);
        _camera.AddComponent<CameraMove>().Init(_player.transform);
        _worldController.Init(_player);
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
