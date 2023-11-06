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
        private Vector2 movement;
        



        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            movement = new Vector2(1f,0f);
            
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

        private void FixedUpdate()
        {
            //if (rb.velocity.magnitude >= _speedMove)
            //{
            //    rb.velocity = rb.velocity.normalized * _speedMove;
            //}

            Vector3 velocity = rb.velocity;

            //// Ограничиваем скорость по оси X
            velocity.x = Mathf.Clamp(velocity.x, -_speedMove, _speedMove);

            rb.velocity = velocity;
            Run();
        }
        


        void Run()
        {

            //if (rb.velocity.x < _maxSpeed * 0.94)
            //{
            //    rb.AddForce(movement * 4);
            //}

            rb.AddForce(movement * 10000 * Time.deltaTime);

            //////rb.velocity = transform.right*Time.deltaTime*_speedMove*5;
            ////transform.Translate(Vector3.right * _speedMove * Time.deltaTime, Space.World);
            //float x = rb.velocity.x;
            //Math.Round(rb.velocity.y);
            Debug.Log(Math.Round(rb.velocity.y,1) + ",,,," + Math.Round(rb.velocity.x, 1));
            //if (rb.velocity.x < (_speedMove * 0.98))
            //{
            //    rb.velocity.(transform.right * 1000 * Time.deltaTime);
                
            //}
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
        }
    }

}
