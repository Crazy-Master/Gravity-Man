using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private int direction = 1;
    [SerializeField] private float _gravitationNow=1;
    [SerializeField] private float _speedMove;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        Run();
        SwapGravity();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        anim.SetBool("isJump", false);
    }



    void Run()
    {
        transform.Translate(Vector3.right * _speedMove * Time.deltaTime);

        anim.SetBool("isRun", true);             
        
    }

    void SwapGravity()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.gravityScale = rb.gravityScale * -1;

            if (rb.gravityScale>0)
            {                
                transform.localScale = new Vector3(1, 1, 1);
            }

            if (rb.gravityScale < 0)
            {               
                transform.localScale = new Vector3(1, -1, 1);
            }
        }
    }
    

}

