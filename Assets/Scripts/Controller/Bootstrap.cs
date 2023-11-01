using System;
using Core.WindowSystem;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour
{
    private StructureLoadLevel _structureLL;
    [SerializeField] private GameObject _player;
    [SerializeField] private Camera _camera;
    private WorldController _worldController = new WorldController();
    public event Action OnRestartGame;
    public event Action OnResurrectGame;
    
    [Inject]
    private void Construct(StructureLoadLevel structure) => _structureLL = structure;

    private void Awake()
    {
        int level = 1;
        GenerationLevel(level); //загрузка из яндекса
        GenerationPlayer(level);
    }

    private void GenerationLevel(int level)
    {
        var settingLevel = _structureLL.GetLevel(level);
        Instantiate(settingLevel.grid);
        Instantiate(settingLevel.decor);
    }

    private void GenerationPlayer(int level)
    {
        var startPos = _structureLL.GetLevel(level).posPlayer;
        _player = Instantiate(_player,startPos, Quaternion.identity);
        _player.GetComponent<PlayerController>().Init(this,startPos);
        _camera.AddComponent<CameraMove>().Init(_player.transform);
    }

    public void Restart()
    {
        _player.SetActive(true);
        OnRestartGame?.Invoke();
    }
    
    public void Resurrect()
    {
        _player.SetActive(true);
        OnResurrectGame?.Invoke();
    }
    
}
