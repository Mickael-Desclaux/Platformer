using MGA.FSM;
using MInputsStore;
using UnityEngine;
#pragma warning disable CS0618 // Type or member is obsolete

namespace MPlayer
{
    public class PlayerJumpState : FSMState<Player>
    {
        private bool _hasJumped;
        private const float _afterJumpDelay = 0.1f;
        private float _afterJumpTimeElapsed;
        
        public PlayerJumpState(Player context) : base(context)
        {
            
        }

        public override void Enter()
        {
            Jump();
        }

        public override void Execute()
        {
            _afterJumpTimeElapsed += Time.fixedDeltaTime;
            
            if (_afterJumpTimeElapsed < _afterJumpDelay)
            {
                return;
            }
            
            if (Context.IsGrounded && _hasJumped)
            {
                _hasJumped = false;
            }
            
            if (Context.IsGrounded && !_hasJumped)
            {
                if (Context.Direction != Vector2.zero)
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
            
        }
        
        private void Jump()
        {
            _afterJumpTimeElapsed = 0f;
            JumpState state = InputsStoreSingleton.Instance.GetState<JumpState>();
            Context.Rigidbody2D.velocity = new Vector2(Context.Rigidbody2D.velocity.x, state.Force);
            _hasJumped = true;
        }
    }
}
