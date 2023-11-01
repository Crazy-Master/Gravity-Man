using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 _startPos; //разобраться с гравитацией!!!
    private Vector3 _oldPosition;
    private Bootstrap _bootstrap;
    
    void Start()
    {
        StartCoroutine(SetOldPosition());
    }

    public void OnCollisionEnter2D()
    {
       
    }

    public void Init(Bootstrap bootstrap, Vector3 startPos)
    {
        _startPos = startPos;
        _bootstrap = bootstrap;
        _bootstrap.OnRestartGame += RestartGame;
        _bootstrap.OnResurrectGame += ResurrectGame;
    }
    
    #region InteractionPauseMenu

    IEnumerator SetOldPosition()
    {
        if (gameObject.activeSelf)
        {
            _oldPosition = gameObject.transform.position;
            yield return new WaitForSeconds(3f);
        }
        yield return null;
    }
    private void RestartGame()
    {
        gameObject.transform.position = _startPos;
        StartCoroutine(SetOldPosition());
    }
    private void ResurrectGame()
    {
        gameObject.transform.position = _oldPosition;
        StartCoroutine(SetOldPosition());
    }
    #endregion

    private void OnDestroy()
    {
        _bootstrap.OnRestartGame -= RestartGame;
        _bootstrap.OnResurrectGame -= ResurrectGame;
    }
}
