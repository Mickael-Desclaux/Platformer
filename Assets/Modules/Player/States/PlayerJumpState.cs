using MGA.FSM;

namespace MPlayer
{
    public class PlayerJumpState : FSMState<Player>
    {
        public PlayerJumpState(Player context) : base(context)
        {
        }
    }
}
