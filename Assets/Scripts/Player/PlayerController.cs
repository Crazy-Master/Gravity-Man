using System;
using System.Collections;
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
        [SerializeField] private LayerMask _groundLayer;
        private SavePlayer _restartSave; //разобраться с гравитацией!!!
        private SavePlayer _resurrectSave;
        private WorldController _worldController;
        private Rigidbody2D _rigidbody2D;
        public event Action<float> OnChangeGravity;
        public event Action<bool> OnChangeGround;
        public float Gravity {private set; get; }
        public bool Ground {private set; get; }
        private bool PreviousGround;

        void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            SetGround(true);
        }
        
        public void Init(WorldController worldController, float gravity)
        {
            _worldController = worldController;
            SetGravity(gravity);
            _worldController.OnRestartGame += Restart;
            _worldController.OnResurrectGame += Resurrect;
            _restartSave.SetData(transform.position, gravity);
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
            }
        }

        private void Update()
        {
            
            bool ground =Physics2D.OverlapCircle(_groundCheck.position, 0.1f, _groundLayer);
            if (ground != PreviousGround)
            {
                SetGround(ground);
            }
            //Debug.Log("Ground"+Ground);
        }

        public void SetGround(bool ground)
        {
            PreviousGround = Ground;
            Ground = ground;
            OnChangeGround?.Invoke(ground);
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