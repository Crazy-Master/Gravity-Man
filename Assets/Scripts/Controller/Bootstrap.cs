using System;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;


public class Bootstrap : MonoBehaviour
{
    private StructureLoadLevel _structureLL;
    [SerializeField] private GameObject _player;
    [SerializeField] private Camera _camera;
    private WorldController _worldController = new WorldController();
    public SavePlayer SavePlayer;
    private SettingLevel _settingLevel;
    private int level = 1; //загрузка из яндекса

    [Inject]
    private void Construct(StructureLoadLevel structure) => _structureLL = structure;

    private void Awake()
    {
        GenerationLevel(level);
        _settingLevel = _structureLL.GetLevel(level);
        GenerationPlayer(_settingLevel.posPlayer, _settingLevel.gravity, _settingLevel.speed);
    }

    private void GenerationLevel(int level)
    {
        var settingLevel = _structureLL.GetLevel(level);
        Instantiate(settingLevel.grid);
        Instantiate(settingLevel.decor);
    }

    private void GenerationPlayer(Vector3 startPos, float gravity, float speed)
    {
        var player = Instantiate(_player,startPos, Quaternion.identity);
        var playerController = player.GetComponent<PlayerController>();
        playerController.Init(_worldController, gravity);
        player.GetComponent<PlayerMove>().Init(speed, 1, playerController);
        _camera.AddComponent<CameraMove>().Init(player.transform);
        _worldController.Init(player);
    }
    

    public WorldController GetWorldController()
    {
        return _worldController;
    }
}
