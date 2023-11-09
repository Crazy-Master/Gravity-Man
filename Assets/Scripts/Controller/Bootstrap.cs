using System;
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
    [SerializeField] private GameObject _backgroundParallax;
    private WorldController _worldController = new WorldController();
    private SettingLevel _settingLevel;
    [SerializeField] private BackgroundCreator _backgroundCreator;
    
    private GameObject _structureLevel;
    private GameObject _player;
    private GameObject _playerTwo;
    private GameObject _enemy;
    
    private int level = 1; //загрузка из яндекса

    [Inject]
    private void Construct(StructureLoadLevel structure) => _structureLL = structure;

    private void Start()
    {
        StartLevel(1);
    }

    public void StartMainMenu()
    {
        DestroyLevel();
        _backgroundCreator.Restart(EScene.MenuScene);
    }

    public void StartLevel(int level)
    {
        _settingLevel = _structureLL.GetLevel(level);
        _backgroundCreator.Restart(_settingLevel.BackgroundScene);
        GenerationLevel();
        GenerationPlayer(_settingLevel.posPlayer, _settingLevel.gravity, _settingLevel.speed);
    }
    private void GenerationLevel()
    {
        _structureLevel = new GameObject("StructureLevel");
        _structureLevel.transform.SetParent(Instantiate(_settingLevel.grid).transform);
        _structureLevel.transform.SetParent(Instantiate(_settingLevel.decor).transform);
    }

    private void GenerationPlayer(Vector3 startPos, float gravity, float speed)
    {
        _player = Instantiate(_prefabPlayer,startPos, Quaternion.identity);
        var playerController = _player.GetComponent<PlayerController>();
        playerController.Init(_worldController, gravity);
        _player.GetComponent<PlayerMove>().Init(speed, 1, playerController);
        _camera.AddComponent<CameraMove>().Init(_player.transform);
        _worldController.Init(_player);
    }

   

        private void DestroyLevel()
    {
        Destroy(_structureLevel);
        Destroy(_player);
        if (_playerTwo != null) Destroy(_player);
    }

    public WorldController GetWorldController()
    {
        return _worldController;
    }
}
