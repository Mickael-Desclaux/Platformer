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
            InputsStoreSingleton.Instance.Subscribe<JumpState>(OnJumped);
        }

        public override void Exit()
        {
            InputsStoreSingleton.Instance.Unsubscribe<MoveState>(OnMoved);
            InputsStoreSingleton.Instance.Unsubscribe<JumpState>(OnJumped);
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

        private void OnJumped(JumpState state)
        {
            if (Context.IsGrounded)
            {
                Context.StateMachine.SetState(new PlayerJumpState(Context));
            }
        }
    }
}
