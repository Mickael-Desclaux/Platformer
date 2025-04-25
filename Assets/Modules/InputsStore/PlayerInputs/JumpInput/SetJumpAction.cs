using MGA.UniFlux;

namespace MInputsStore
{
    public record SetJumpAction : ImmutableAction<JumpState>
    {
        private readonly float _force;

        public SetJumpAction(float force)
        {
            _force = force;
        }
        
        public override JumpState Reduce(JumpState state)
        {
            return new JumpState(_force);
        }
    }
}