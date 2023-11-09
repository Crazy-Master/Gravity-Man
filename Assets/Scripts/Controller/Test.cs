using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class Test: MonoBehaviour
{
    public PlayerController playerController;
    private WorldController _worldController = new WorldController();

    public PlayerMove playerMove;

    [Space]

    public float Gravity;
    public float Speed;
    public int NumberPlayer;

    private void Start()
    {
        playerController.Init(_worldController, Gravity);
        playerMove.Init(Speed, NumberPlayer, playerController, _worldController);
        _worldController.Init(playerController.gameObject);
    }

}
