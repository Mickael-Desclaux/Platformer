using UnityEngine;

#pragma warning disable CS0618 // Type or member is obsolete

namespace MPlayer
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _speed = 1f;
        public float Speed => _speed;
        
        public Vector2 Direction { get; set; }
        public bool IsGrounded { get; set; }
        
        [SerializeField] private LayerMask _groundLayerMask;
        [SerializeField] private float _groundRadius = 0.2f;
        [SerializeField] private Transform _playerFeet;

        public PlayerStateMachine StateMachine { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }
        public Rigidbody2D Rigidbody2D { get; private set; }
        
        private void Awake()
        {
            StateMachine = new PlayerStateMachine();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            StateMachine.SetState(new PlayerIdleState(this));
        }

        private void FixedUpdate()
        {
            IsGrounded = Physics2D.OverlapCircle(_playerFeet.position, _groundRadius, _groundLayerMask);
            
            StateMachine.Execute();
        }
        
        #region Debug

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = IsGrounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(_playerFeet.position, _groundRadius);
        }

        #endregion
    }
}
