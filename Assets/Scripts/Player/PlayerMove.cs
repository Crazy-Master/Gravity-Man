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
        private WorldController _worldController;
        private Vector2 movement;
        



        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            movement = new Vector2(1f,0f);
            
        }

        public void Init(float speed, int numderPlayer, PlayerController playerController, WorldController worldController)
        {
            _speedMove = speed;
            _numderPlayer = numderPlayer;
            _playerController = playerController;
            _worldController = worldController;
            _playerController.OnChangeGravity += SetAnimationGravity;
            _playerController.OnChangeGround += SetAnimationGround;
            _worldController.OnSwapGravity += TapSwapGravity;
            SetAnimationGravity(_playerController.Gravity);
            SetAnimationGround(_playerController.Ground);
        }

        void Update()
        {
            
            switch (_numderPlayer)
            {
                case 1:
                    if (Input.GetKeyDown(KeyCode.A) && _playerController.Ground)
                    {
                        SwapGravity();
                    }
           
                    break;
                case 2:
                    if (Input.GetKeyDown(KeyCode.S) && _playerController.Ground)
                    {
                        SwapGravity();
                    }
           
                    break;
                case 3:
                    if (Input.GetKeyDown(KeyCode.D) && _playerController.Ground)
                    {
                        SwapGravity();
                    }
           
                    break;
                default:
                    if (Input.GetKeyDown(KeyCode.F) && _playerController.Ground)
                    {
                        SwapGravity();
                    }
                    break;
            } 
        }
        
        private void TapSwapGravity(int numderPlayer)
        {
            if (numderPlayer == _numderPlayer && _playerController.Ground)
            {
                SwapGravity();
            }
        }

        private void FixedUpdate()
        {
            //if (rb.velocity.magnitude >= _speedMove)
            //{
            //    rb.velocity = rb.velocity.normalized * _speedMove;
            //}

            Vector3 velocity = rb.velocity;

            //// ������������ �������� �� ��� X
            velocity.x = Mathf.Clamp(velocity.x, -_speedMove, _speedMove);

            rb.velocity = velocity;
            Run();
        }
        


        void Run()
        {

            rb.AddForce(movement * 3000 * Time.deltaTime);
            
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
        

        private void OnDestroy()
        {
            _playerController.OnChangeGravity -= SetAnimationGravity;
            _playerController.OnChangeGround -= SetAnimationGround;
            _worldController.OnSwapGravity -= TapSwapGravity;
        }
    }

}
