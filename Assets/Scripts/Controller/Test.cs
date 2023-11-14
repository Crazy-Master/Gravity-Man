using UnityEngine;
using Player;
using Unity.Mathematics;
using Unity.VisualScripting;

public class Test: MonoBehaviour
{
    private WorldController _worldController = new WorldController();
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject[] _player;
    public GameObject playerPrefab;

    [Space]

    public float Gravity;
    public float Speed;
    public int NumberPlayer;

    private void Start()
    {
        InitPlayer();
        _camera.AddComponent<CameraMove>().Init(_player[0].transform);
    }

    private void InitPlayer()
    {
        _player = new GameObject[NumberPlayer];
        for (int i = 0; i < _player.Length; i++)
        {
            _player[i] = Instantiate(playerPrefab, new Vector3(1, -5, 0), quaternion.identity);
            var pController = _player[i].GetComponent<PlayerController>();
            pController.Init(_worldController, Gravity);
            var PlayerMove = _player[i].GetComponent<PlayerMove>();
            PlayerMove.Init(Speed, i, pController, _worldController);
        }
        _worldController.SetPlayers(_player);
       
    }

}
