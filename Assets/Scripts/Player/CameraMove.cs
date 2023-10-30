using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform _player1;
    private float smoothSpeed = 0.125f;
    private Vector3 _positionPlayer;
    private Vector3 offset = new Vector3(10,0,0);

    public void LateUpdate()
    {
        Vector3 cameraPosition = new Vector3(_player1.position.x + offset.x, transform.position.y , transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, cameraPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}