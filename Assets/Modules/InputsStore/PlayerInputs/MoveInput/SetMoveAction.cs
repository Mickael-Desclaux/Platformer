using MGA.UniFlux;
using UnityEngine;

namespace MInputsStore
{
    public record SetMoveAction : ImmutableAction<MoveState>
    {
        public readonly Vector2 Move;

        public SetMoveAction(Vector2 move)
        {
            Move = move;
        }
        
        public override MoveState Reduce(MoveState state)
        {
            return new MoveState(Move);
        }
    }
}