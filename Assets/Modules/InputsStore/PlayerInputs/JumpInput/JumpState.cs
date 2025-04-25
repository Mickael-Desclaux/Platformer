using MGA.UniFlux;

namespace MInputsStore
{
    public record JumpState : ImmutableState
    {
        public readonly float Force;

        public JumpState(float force)
        {
            Force = force;
        }
    }
}