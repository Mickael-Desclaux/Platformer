using MGA.FSM;
using MInputsStore;
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
            InputsStoreSingleton.Instance.Subscribe<MoveState>(OnMoved);
            Context.Jumped += OnJumped;
        }

        public override void Exit()
        {
            InputsStoreSingleton.Instance.Unsubscribe<MoveState>(OnMoved);
            Context.Jumped -= OnJumped;
        }

        private void OnMoved(MoveState state)
        {
            Context.Direction = state.Direction;

            if (state.Direction == Vector2.zero)
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
