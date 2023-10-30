using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDirection : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private Vector3 newScale;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();       
    }
    public void ChangeScaleDir()
    {
       
        if (rb.gravityScale > 0)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            //newScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            //transform. = new Vector3(x, y, z);

        }

        if (rb.gravityScale < 0)
        {
            transform.localScale = new Vector3(0.5f, -0.5f, 0.5f);
            //newScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
        }

        //transform.localScale = newScale;

    }
}
