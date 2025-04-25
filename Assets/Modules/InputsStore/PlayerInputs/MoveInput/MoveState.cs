using MGA.UniFlux;
using UnityEngine;

namespace MInputsStore
{
    public record MoveState : ImmutableState
    {
        public readonly Vector2 Move;

        public MoveState(Vector2 move)
        {
            Move = move;
        }
    }
}
