using MGA.FSM;
using UnityEngine;
#pragma warning disable CS0618 // Type or member is obsolete

namespace MPlayer
{
    public class PlayerMoveState : FSMState<Player>
    {
        public PlayerMoveState(Player context) : base(context)
        {
        }
        
        public override void Enter()
        {
            Context.Moved += OnMoved;
            Context.Jumped += OnJumped;
            ReverseSprite();
        }

        public override void Execute()
        {
            if (Context.Direction == Vector2.zero)
            {
                return;
            }

            Vector3 direction = new Vector3(Context.Direction.x, 0, 0);
            Context.transform.position += direction * Context.Speed;
            
            if (Context.Direction != Vector2.zero)
            {
                Context.Rigidbody2D.velocity = new Vector2(Context.Direction.x * Context.Speed, Context.Rigidbody2D.velocity.y);
            }
        }

        public override void Exit()
        {
            Context.Moved -= OnMoved;
            Context.Jumped -= OnJumped;
        }

        private void OnMoved(Vector2 direction)
        {
            if (direction == Vector2.zero)
            {
                Context.Rigidbody2D.velocity = new Vector2(0, Context.Rigidbody2D.velocity.y);
                Context.StateMachine.SetState(new PlayerIdleState(Context));
                return;
            }
            
            Context.Direction = direction;
            ReverseSprite();
        }

        private void ReverseSprite()
        {
            Context.SpriteRenderer.flipX = Context.Direction.x < 0;
        }
        
        private void OnJumped()
        {
            if (Context.IsGrounded)
            {
                Context.StateMachine.SetState(new PlayerJumpState(Context));
            }
        }
    }
}
