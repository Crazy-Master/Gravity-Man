using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDirection : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();       
    }
    public void ChangeScaleDir()
    {
        if (rb.gravityScale > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (rb.gravityScale < 0)
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
    }
}
