using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MPlayer
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _speed = 1;
        private Vector2 _direction;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
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

        private void FixedUpdate()
        {
            if (_direction == Vector2.zero)
            {
                return;
            }

            Vector3 direction = new Vector3(_direction.x, 0, 0);
            transform.position += direction * _speed;
        }
    }
}
