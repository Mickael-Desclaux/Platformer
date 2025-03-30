using MGA.FSM;
using UnityEngine;

namespace MPlayer
{
    public class PlayerIdleState : FSMState<Player>
    {
        public PlayerIdleState(Player context) : base(context)
        {
            
        }

        public override void Enter()
        {
            Context.Moved += OnMoved;
            Context.Jumped += OnJumped;
        }

        public override void Exit()
        {
            Context.Moved -= OnMoved;
            Context.Jumped -= OnJumped;
        }

        private void OnMoved(Vector2 direction)
        {
            Context.Direction = direction;

            if (direction == Vector2.zero)
            {
                return;
            }
            
            Context.StateMachine.SetState(new PlayerMoveState(Context));
        }

        private void OnJumped()
        {
            if (Context.IsGrounded)
            {
                Context.StateMachine.SetState(new PlayerJumpState(Context));
            }
        }
    }
}
