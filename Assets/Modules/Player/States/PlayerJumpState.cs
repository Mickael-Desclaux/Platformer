using MGA.FSM;
using MInputsStore;
using UnityEngine;
#pragma warning disable CS0618 // Type or member is obsolete

namespace MPlayer
{
    public class PlayerJumpState : FSMState<Player>
    {
        private const float _afterJumpDelay = 0.1f;
        private float _afterJumpTimeElapsed;
        private const int _maxJumpCount = 2;
        private int _jumpCount;
        
        public PlayerJumpState(Player context) : base(context)
        {
            
        }

        public override void Enter()
        {
            InputsStoreSingleton.Instance.Subscribe<JumpState>(OnJumped);
            Jump();
        }
        
        public override void Execute()
        {
            MoveState moveState = InputsStoreSingleton.Instance.GetState<MoveState>();
            _afterJumpTimeElapsed += Time.fixedDeltaTime;
            
            if (_afterJumpTimeElapsed < _afterJumpDelay)
            {
                return;
            }
            
            if (Context.IsGrounded)
            {
                Debug.Log(moveState.Direction);
                
                if (moveState.Direction != Vector2.zero)
                {
                    Context.StateMachine.SetState(new PlayerMoveState(Context));
                }
                else
                {
                    Context.StateMachine.SetState(new PlayerIdleState(Context));
                }
            }
        }

        public override void Exit()
        {
            InputsStoreSingleton.Instance.Unsubscribe<JumpState>(OnJumped);
        }
        
        private void OnJumped(JumpState obj)
        {
            if (_jumpCount < _maxJumpCount)
            {
                Jump();
            };
        }
        
        private void Jump()
        {
            _afterJumpTimeElapsed = 0f;
            _jumpCount++;
            JumpState state = InputsStoreSingleton.Instance.GetState<JumpState>();
            Context.Rigidbody2D.velocity = new Vector2(Context.Rigidbody2D.velocity.x, state.Force);
        }
    }
}
