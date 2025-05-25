using MGA.UniJect;
using MUIStore;
using MUIStore.State;
using UnityEngine;

#pragma warning disable CS0618 // Type or member is obsolete

namespace MPlayer
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _speed = 1f;
        public float Speed => _speed;
        
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
            StateMachine.Debug = true;
            SpriteRenderer = GetComponent<SpriteRenderer>();
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            StateMachine.SetState(new PlayerIdleState(this));

            _uiStore1.Subscribe<TestUIState>(OnUIStateChange);
            _uiStore2.Subscribe<TestUIState>(OnUIStateChange);
            
            _uiStore1.Dispatch(new TestUIIncrementAction());
            _uiStore2.Dispatch(new TestUIIncrementAction());
        }

        private void OnUIStateChange(TestUIState obj)
        {
            Debug.Log(obj.Count);
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
        
        [Inject] private UIStore _uiStore1;
        [Inject] private UIStore _uiStore2;

        #endregion
    }
}
