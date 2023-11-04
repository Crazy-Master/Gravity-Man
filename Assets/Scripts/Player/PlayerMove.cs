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



        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        public void Init(float speed, int numderPlayer, PlayerController playerController)
        {
            _speedMove = speed;
            _numderPlayer = numderPlayer;
            _playerController = playerController;
        }

        void Update()
        {
            //Debug.Log(anim.GetBool("onPlace"));
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
            else
            {
                anim.SetBool("isRun", false);
            }

            if (anim.GetBool("onPlace") && rb.gravityScale < 0)
            {
                anim.SetBool("isJump", false);
                anim.SetBool("isRun", false);
                anim.SetBool("isRun1", true);
            }
            else
            {
                anim.SetBool("isRun1", false);
            }
        }

        void SwapGravity()
        {
            anim.SetBool("onPlace", false);
            _playerController.SetGravity(_playerController.Gravity*-1);
            anim.SetBool("isJump", true);
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

    }

}
