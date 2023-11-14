using System;
using UnityEngine;

namespace Player
{
    public struct SavePlayer
    {
        public Vector3 Position;
        public float Gravity;
        public void SetData(Vector3 position, float gravity)
        {
            Position = position;
            Gravity = gravity;
        }
    }

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private Transform _forwardCheck;
        [SerializeField] private LayerMask _groundLayer;
        private SavePlayer _restartSave; //разобраться с гравитацией!!!
        private SavePlayer _resurrectSave;
        private WorldController _worldController;
        private Rigidbody2D _rigidbody2D;
        private Vector2 _sizeBox;
        public event Action<float> OnChangeGravity;
        public event Action<bool> OnChangeGround;
        public event Action<bool> OnChangeWall;
        public float Gravity {private set; get; }
        public bool Ground {private set; get; }
        
        public bool Wall {private set; get; }
        
        private bool _previousGround;
        private bool _previousWall;

        void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            var colliderSize = gameObject.GetComponent<CapsuleCollider2D>().size;
            Debug.Log("colliderSize: "+colliderSize);
            _sizeBox = new Vector2( colliderSize.x/2 + 0.05f,colliderSize.y/2 * 0.80f );
        }

        public void Init(WorldController worldController, float gravity)
        {
            _worldController = worldController;
            SetGravity(gravity);
            SetWall(false);
            _worldController.OnRestartGame += Restart;
            _worldController.OnResurrectGame += Resurrect;
            _restartSave.SetData(transform.position, gravity);
            _resurrectSave = _restartSave;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            switch (other.gameObject.tag)
            {
               case "Dead":
                   DeadPlayer();
                   break;
               case "Resurrect":
                   CheckPointController point = other.GetComponent<CheckPointController>();
                   _resurrectSave.SetData(point.GetPosition(), Math.Abs(Gravity) * point.GetGravity());
                   break;
               case "LevelComplete":
                   _worldController.LevelComplete();
                   break;
               case "Star":
                   other.GetComponent<StarController>().Destroy(_worldController);
                   break;
            }
        }

        private void FixedUpdate()
        {
            bool wall =Physics2D.OverlapBox(transform.position, _sizeBox,0f, _groundLayer);
            bool ground =Physics2D.OverlapCircle(_groundCheck.position, 0.1f, _groundLayer);
            if (wall != _previousWall) SetWall(wall);
            if (ground != _previousGround) SetGround(ground);
            
        }

        public void SetGround(bool ground)
        {
            _previousGround = Ground;
            Ground = ground;
            OnChangeGround?.Invoke(ground);
        }
        
        public void SetWall(bool wall)
        {
            _previousWall = Wall;
            Wall = wall;
            OnChangeWall?.Invoke(wall);
            Debug.Log("wall: "+ wall);
        }
        /*public void OnCollisionEnter2D(Collision2D other)
        {
            switch (other.gameObject.tag)
            {
                case "Ground":
                    Ground = Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
                    Debug.Log("Ground"+Ground);
                    break;
            }
        }

        public void OnCollisionExit2D(Collision2D other)
        {
            switch (other.gameObject.tag)
            {
                case "Ground":
                    Ground = false;
                    Debug.Log("Ground"+Ground);
                    break;
            }
        }*/


        public void SetGravity(float gravity)
        {
            _rigidbody2D.gravityScale = gravity;
            Gravity = gravity;
            OnChangeGravity?.Invoke(gravity);
        }
        
        
    
        #region InteractionPauseMenu

        /*private IEnumerator SetOldPosition()
        {
            if (Ground)
            {
                _resurrectSave.SetData(gameObject.transform.position, _rigidbody2D.gravityScale);
                yield return new WaitForSeconds(5f); 
            }
            else
            {
                yield return new WaitForSeconds(1f); 
            }
            StartCoroutine(SetOldPosition());
        }*/

        private void Restart()
        {
            gameObject.transform.position = _restartSave.Position;
            SetGravity(_restartSave.Gravity);
            _rigidbody2D.velocity = Vector2.zero;
            ChangeScaleDir();
        }

        private void Resurrect() //переделать на чекпоинты???
        {
            gameObject.transform.position = _resurrectSave.Position;
            SetGravity(_resurrectSave.Gravity);
            _rigidbody2D.velocity = Vector2.zero;
            ChangeScaleDir();
        }
        
        private void DeadPlayer()
        {
            _worldController.GameOver();
            gameObject.SetActive(false);
        }
    
        #endregion

        private void OnDestroy()
        {
            _worldController.OnRestartGame -= Restart;
            _worldController.OnResurrectGame -= Resurrect;
        }
        
        public void ChangeScaleDir()
        {
            var localScale = transform.localScale;
            if (Gravity > 0)
            {
                transform.localScale = new Vector3(localScale.x, Math.Abs(localScale.y), localScale.z);
            }

            if (Gravity < 0)
            {
                transform.localScale = new Vector3(localScale.x, -Math.Abs(localScale.y), localScale.z);
            }
        }
        
    }
}