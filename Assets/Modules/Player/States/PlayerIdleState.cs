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
        }

        public override void Exit()
        {
            Context.Moved -= OnMoved;
        }

        private void OnMoved(Vector2 direction)
        {
            if (direction == Vector2.zero)
            {
                return;
            }
            
            Context.StateMachine.SetState(new PlayerMoveState(Context, direction));
        }
    }
}
