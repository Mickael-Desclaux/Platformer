using MGA.FSM;
using UnityEngine;
#pragma warning disable CS0618 // Type or member is obsolete

namespace MPlayer
{
    public class PlayerJumpState : FSMState<Player>
    {
        private bool _hasJumped;
        
        public PlayerJumpState(Player context) : base(context)
        {
            
        }

        public override void Enter()
        {
            Jump();
        }

        public override void Execute()
        {
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
            Context.Rigidbody2D.velocity = new Vector2(Context.Rigidbody2D.velocity.x, Context.JumpForce);
            _hasJumped = true;
        }
    }
}
