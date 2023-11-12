using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class EntityMove : MonoBehaviour
    

{
    private Rigidbody2D rb;
    private Animator anim;
    private int direction = 1;
    [SerializeField] private float _gravitationNow = 1;
    [SerializeField] private float _speedMove;
    private bool isJumping = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        Debug.Log(anim.GetBool("onPlace"));
        Run();
        SwapGravity();

    }    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        anim.SetBool("onPlace", true);
    }




    void Run()
    {
        transform.Translate(Vector3.right * _speedMove * Time.deltaTime, Space.World);

        if (anim.GetBool("onPlace") && rb.gravityScale > 0)
        {
            anim.SetBool("isJump", false);
            anim.SetBool("isRun1", false);
            anim.SetBool("isRun", true);
        }
        else { anim.SetBool("isRun", false); }

        if (anim.GetBool("onPlace") && rb.gravityScale < 0)
        {
            anim.SetBool("isJump", false);
            anim.SetBool("isRun", false);
            anim.SetBool("isRun1", true);
        }
        else { anim.SetBool("isRun1", false); }
    }

    void SwapGravity()
    {
        if (Input.GetMouseButtonDown(0) && anim.GetBool("onPlace"))
        {
            anim.SetBool("onPlace", false);
            rb.gravityScale = rb.gravityScale * -1;
            anim.SetBool("isJump", true);

        }
    }
}
