using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

#pragma warning disable CS0618 // Type or member is obsolete

namespace MPlayer
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        #region To Remove

        public event Action<Vector2> Moved;

        #endregion
        
        [SerializeField] private float _speed = 1f;
        public float Speed => _speed;
        
        [SerializeField] private float _jumpForce = 10f;
        [SerializeField] private LayerMask _groundLayerMask;
        [SerializeField] private float _groundRadius = 0.2f;
        [SerializeField] private Transform _playerFeet;
        private bool _isGrounded;
        private bool _hasJumped;

        public PlayerStateMachine StateMachine { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }
        public Rigidbody2D Rigidbody2D { get; private set; }
        
        private void Awake()
        {
            StateMachine = new PlayerStateMachine();
            StateMachine.Debug = true;
            SpriteRenderer = GetComponent<SpriteRenderer>();
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            StateMachine.SetState(new PlayerIdleState(this));
        }

        private void FixedUpdate()
        {
            _isGrounded = Physics2D.OverlapCircle(_playerFeet.position, _groundRadius, _groundLayerMask);
            
            StateMachine.Execute();
        }

        [UsedImplicitly]
        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            Moved?.Invoke(context.ReadValue<Vector2>());
        }
        
        [UsedImplicitly]
        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            Moved?.Invoke(Vector2.zero);
        }

        [UsedImplicitly]
        private void OnJumpPerformed(InputAction.CallbackContext context)
        {
            if (_isGrounded)
            {
                Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, _jumpForce);
                
                _hasJumped = true;
                
                return;
            }
            
            if (_hasJumped == true)
            {
                Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, _jumpForce);
                _hasJumped = false;
            }
            
        }




        #region Debug

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = _isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(_playerFeet.position, _groundRadius);
        }

        #endregion
    }
}
