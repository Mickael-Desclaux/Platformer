using MGA.FSM;
using UnityEngine;
#pragma warning disable CS0618 // Type or member is obsolete

namespace MPlayer
{
    public class PlayerMoveState : FSMState<Player>
    {
        private Vector2 _direction;

        public PlayerMoveState(Player context, Vector2 currentDirection) : base(context)
        {
            _direction = currentDirection;
        }
        
        public override void Enter()
        {
            Context.Moved += OnMoved;
            ReverseSprite();
        }

        public override void Execute()
        {
            if (_direction == Vector2.zero)
            {
                return;
            }

            Vector3 direction = new Vector3(_direction.x, 0, 0);
            Context.transform.position += direction * Context.Speed;
            
            if (_direction != Vector2.zero)
            {
                Context.Rigidbody2D.velocity = new Vector2(_direction.x * Context.Speed, Context.Rigidbody2D.velocity.y);
            }
        }

        public override void Exit()
        {
            Context.Moved -= OnMoved;
        }

        private void OnMoved(Vector2 direction)
        {
            if (direction == Vector2.zero)
            {
                Context.Rigidbody2D.velocity = new Vector2(0, Context.Rigidbody2D.velocity.y);
                Context.StateMachine.SetState(new PlayerIdleState(Context));
                return;
            }
            
            _direction = direction;
            ReverseSprite();
        }

        private void ReverseSprite()
        {
            Context.SpriteRenderer.flipX = _direction.x < 0;
        }
    }
}
