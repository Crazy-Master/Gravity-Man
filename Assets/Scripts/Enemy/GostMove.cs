using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GostMove : MonoBehaviour
{
    
    public Transform target;
    private float _speed = 2f;
    private float _speedPlayer;

    [SerializeField] private float _minDistanceEnemy;
    [SerializeField] private float _minSpeedCf = 0.9f;
    [SerializeField] private float _maxDistanceEnemy;
    [SerializeField] private float _maxSpeedCf = 1.3f;


    public float amplitude = 1f; // Амплитуда качания по оси Y
    public float frequency = 1f; // Частота качания по оси Y
    public float rotationSpeed = 5f; // Скорость поворота

    //private float startY;

                                                                               //ИЗМЕНИТЬ В КОНЦЕ 2ф НА СПИД
    //public void Init(float Speed, GameObject Player)
    //{
    //    _speed = _speedPlayer = Speed;
        

    //}

    public void Start()
    {
      //  startY = transform.position.y;
    }
    private void Update()
    {
        Vector3 direction = target.position - transform.position;

        #region Move test

        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Quaternion newRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);


        ////direction.Normalize();

        //transform.Translate(direction * speed * Time.deltaTime,Space.Self);

        //  transform.Translate(direction.x * speed * Time.deltaTime, direction.y+ newY, 0f,Space.World);

        //  transform.position = new Vector3(transform.position.x, direction.y+newY, 0f);

        // float newY = startY + amplitude * Mathf.Sin(Time.time * frequency);
        //transform.localPosition = new Vector3(transform.position.x,newY, 0f);

        #endregion

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.position += direction.normalized * _speed * Time.deltaTime;      
        
        if (Mathf.Abs(direction.x) < _minDistanceEnemy)
        {
           _speed = _minSpeedCf * 2f;
        }
        else if (Mathf.Abs(direction.x) > _maxDistanceEnemy)
        {
            _speed = _maxSpeedCf * 2f;
        }
        else
        {
            _speed = 2f;
        }



    }
}
