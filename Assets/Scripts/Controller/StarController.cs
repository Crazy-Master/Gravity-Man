using System;
using UnityEngine;

public class StarController : MonoBehaviour
{
    private WorldController _worldController;
    public void Destroy(WorldController worldController)
    {
        _worldController = worldController;
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _worldController?.SetStars();
    }
}
