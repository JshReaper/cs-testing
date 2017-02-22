using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    class Walk : IPlayerBehavior
    {

        private Animator animator;
        private Transform transform;
        private Player player;
        public Walk(Transform transform,Animator animator, Player player)
        {
            this.transform = transform;
            this.animator = animator;
            this.player = player;
        }

        public void Execute(Direction currentDirection)
        {
            Vector2 translation = Vector2.Zero;
            KeyboardState keyState = Keyboard.GetState();
            
            if (keyState.IsKeyDown(Keys.W) || keyState.IsKeyDown(Keys.Up))
            {
                translation += new Vector2(0, -1);
                animator.PlayAnimation("WalkBack");
            }
            if (keyState.IsKeyDown(Keys.D) || keyState.IsKeyDown(Keys.Right))
            {
                translation += new Vector2(1, 0);

                animator.PlayAnimation("WalkRight");
            }
            if (keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.Left))
            {
                translation += new Vector2(-1, 0);

                animator.PlayAnimation("WalkLeft");
            }
            if (keyState.IsKeyDown(Keys.S) || keyState.IsKeyDown(Keys.Down))
            {
                translation += new Vector2(0, +1);

                animator.PlayAnimation("WalkFront");
            }
            transform.Translate(translation * GameWorld.Instance.deltaTime * player.Speed);
            animator.PlayAnimation("Walk" + currentDirection);
        }
    }
}