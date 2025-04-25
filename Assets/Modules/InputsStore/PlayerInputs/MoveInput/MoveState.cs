using MGA.UniFlux;
using UnityEngine;

namespace MInputsStore
{
    public record MoveState : ImmutableState
    {
        public readonly Vector2 Direction;

        public MoveState(Vector2 direction)
        {
            Direction = direction;
        }
    }
}
