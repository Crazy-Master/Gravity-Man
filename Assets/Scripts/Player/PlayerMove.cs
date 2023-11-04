using System;
using UnityEngine;

namespace Player
{
    public class PlayerMove : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Animator anim;
        private int direction = 1;
        private float _speedMove = 3;
        private int _numderPlayer;
        private PlayerController _playerController;



        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        public void Init(float speed, int numderPlayer, PlayerController playerController)
        {
            _speedMove = speed;
            _numderPlayer = numderPlayer;
            _playerController = playerController;
            _playerController.OnChangeGravity += SetAnimationGravity;
            _playerController.OnChangeGround += SetAnimationGround;
            SetAnimationGravity(_playerController.Gravity);
            SetAnimationGround(_playerController.Ground);
        }

        void Update()
        {
            Run();
            switch (_numderPlayer)
            {
                case 1:
                    if (Input.GetMouseButtonDown(0) && _playerController.Ground)
                    {
                        SwapGravity();
                    }

                    break;
                default:
                    if (Input.GetKeyDown(KeyCode.Space) && _playerController.Ground)
                    {
                        SwapGravity();
                    }

                    break;
            }
        }


        void Run()
        {
            transform.Translate(Vector3.right * _speedMove * Time.deltaTime, Space.World);
        }

        private void SetAnimationGravity(float gravity)
        {
            if (gravity > 0)
            {
                anim.SetBool("NormalGravity", true);  
            }
            else
            {
                anim.SetBool("NormalGravity", false);  
            }

        }
        
        private void SetAnimationGround(bool ground)
        {
            anim.SetBool("OnGround", ground);
        }

        void SwapGravity()
        {
            anim.SetTrigger("Jump");
            _playerController.SetGravity(_playerController.Gravity*-1);
        }
        #region MaximumSpeedLimit

        private void FixedUpdate()
        {
            if (rb.velocity.magnitude >= _speedMove)
            {
                rb.velocity = rb.velocity.normalized * _speedMove;
            }
        }

        #endregion

        private void OnDestroy()
        {
            _playerController.OnChangeGravity -= SetAnimationGravity;
            _playerController.OnChangeGround -= SetAnimationGround;
        }
    }

}
