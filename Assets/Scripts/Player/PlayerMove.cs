using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Player
{
    public class PlayerMove : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private Animator _anim;
        private int _direction = 1;
        private float _speedMove = 3;
        private int _numderPlayer;
        private PlayerController _playerController;
        private WorldController _worldController;
        private Vector3 _movement = new Vector3(1,0,0);

        [SerializeField] private Transform _targetForward;
        public float speed;
        public float smoothTime = 0.3f;



        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
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
                case 0:
                    if (Input.GetKeyDown(KeyCode.A) && _playerController.Ground)
                    {
                        SwapGravity();
                    }
           
                    break;
                case 1:
                    if (Input.GetKeyDown(KeyCode.S) && _playerController.Ground)
                    {
                        SwapGravity();
                    }
           
                    break;
                case 2:
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
            RunMoveTowards();
        }

        private void RunMoveTowards()
        {
            var force = (float)Math.Round((_speedMove - _rb.velocity.x) * 5);
               if (_playerController.Wall == false && force > 0)
               {
                   Debug.Log("111");
                   _rb.AddForce(_movement * force, ForceMode2D.Impulse);
               }

               //Debug.Log("speed = " + Math.Round(_rb.velocity.x, 2) + ";" + " force = " + force);
        }


        /*private void SpeedLimit()
        {
            Vector3 velocity = _rb.velocity;
            
            velocity.x = Mathf.Clamp(velocity.x, -_speedMove, _speedMove);

            _rb.velocity = velocity;
        }*/

        private void SetAnimationGravity(float gravity)
        {
            if (gravity > 0)
            {
                _anim.SetBool("NormalGravity", true);  
            }
            else
            {
                _anim.SetBool("NormalGravity", false);  
            }

        }
        
        private void SetAnimationGround(bool ground)
        {
            _anim.SetBool("OnGround", ground);
        }
        void SwapGravity()
        {
            _anim.SetTrigger("Jump");
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
