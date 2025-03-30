using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MPlayer
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _jumpForce = 10f;
        private Vector2 _direction;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        
        private void FixedUpdate()
        {
            if (_direction == Vector2.zero)
            {
                return;
            }

            Vector3 direction = new Vector3(_direction.x, 0, 0);
            transform.position += direction * _speed;
        }

        [UsedImplicitly]
        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            _direction = context.ReadValue<Vector2>();
            _spriteRenderer.flipX = _direction.x < 0;
        }
        
        [UsedImplicitly]
        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            _direction = Vector2.zero;
        }

        [UsedImplicitly]
        private void OnJumpPerformed(InputAction.CallbackContext context)
        {
            _rigidbody2D.AddForceY(_jumpForce, ForceMode2D.Impulse);
        }
    }
}
