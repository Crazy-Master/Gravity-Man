using System;
using UnityEngine;

public class Updatable : MonoBehaviour, IUpdatable
{
    public event Action OnUpdate;

    public void Update()
    {
        OnUpdate?.Invoke(); 
    }
}
