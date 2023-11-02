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
        private SavePlayer _restartSave; //разобраться с гравитацией!!!
        private SavePlayer _resurrectSave;
        private WorldController _worldController;
        private Rigidbody2D _rigidbody2D;
        public float Gravity {private set; get; }
        public bool Ground {private set; get; }

        void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            StartCoroutine(SetOldPosition());
            Ground = true;
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
            }
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            switch (other.gameObject.tag)
            {
                case "Ground":
                    Ground = true;
                    break;
            }
        }

        public void OnCollisionExit2D(Collision2D other)
        {
            switch (other.gameObject.tag)
            {
                case "Ground":
                    Ground = false;
                    break;
            }
        }


        public void SetGravity(float gravity)
        {
            _rigidbody2D.gravityScale = gravity;
            Gravity = gravity;
        }
        
        
    
        #region InteractionPauseMenu

        private IEnumerator SetOldPosition()
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
        }

        private void Restart()
        {
            gameObject.transform.position = _restartSave.Position;
            _rigidbody2D.gravityScale = _restartSave.Gravity;
            _rigidbody2D.velocity = Vector2.zero;
            StartCoroutine(SetOldPosition());
        }
        
        private void Resurrect() //переделать на чекпоинты???
        {
            gameObject.transform.position = _resurrectSave.Position;
            _rigidbody2D.gravityScale = _resurrectSave.Gravity;
            _rigidbody2D.velocity = Vector2.zero;
            StartCoroutine(SetOldPosition());
        }
        
        private void DeadPlayer()
        {
            StopCoroutine(SetOldPosition());
            _worldController.GameOver();
            gameObject.SetActive(false);
        }
    
        #endregion

        private void OnDestroy()
        {
            _worldController.OnRestartGame -= Restart;
            _worldController.OnResurrectGame -= Resurrect;
        }
        
    }
}