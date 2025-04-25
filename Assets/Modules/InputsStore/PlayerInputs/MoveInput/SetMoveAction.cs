using MGA.UniFlux;
using UnityEngine;

namespace MInputsStore
{
    public record SetMoveAction : ImmutableAction<MoveState>
    {
        private readonly Vector2 _direction;

        public SetMoveAction(Vector2 direction)
        {
            _direction = direction;
        }
        
        public override MoveState Reduce(MoveState state)
        {
            return new MoveState(_direction);
        }
    }
}