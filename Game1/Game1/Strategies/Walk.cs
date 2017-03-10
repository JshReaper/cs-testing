using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    class Walk : IStrategy
    {

        private Animator animator;
        private Transform transform;
        private Player player;
        /// <summary>
        /// sets the animator transform and player
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="animator"></param>
        /// <param name="player"></param>
        public Walk(Transform transform,Animator animator, Player player)
        {
            this.transform = transform;
            this.animator = animator;
            this.player = player;
        }
        /// <summary>
        /// moves the player plays the animation based on that movement
        /// </summary>
        /// <param name="currentDirection"></param>
        public void Execute(Direction currentDirection)
        {
            Vector2 translation = Vector2.Zero;
            KeyboardState keyState = Keyboard.GetState();
            
            if (keyState.IsKeyDown(Keys.W) || keyState.IsKeyDown(Keys.Up))
            {
                translation += new Vector2(0, -1);
                currentDirection = Direction.Back;
            }
            if (keyState.IsKeyDown(Keys.D) || keyState.IsKeyDown(Keys.Right))
            {
                translation += new Vector2(1, 0);

                currentDirection = Direction.Right;
            }
            if (keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.Left))
            {
                translation += new Vector2(-1, 0);
                currentDirection = Direction.Left;
            }
            if (keyState.IsKeyDown(Keys.S) || keyState.IsKeyDown(Keys.Down))
            {
                translation += new Vector2(0, +1);

                currentDirection = Direction.Front;
            }
            transform.Translate(translation * GameWorld.Instance.deltaTime * player.Speed);
            animator.PlayAnimation("Walk" + currentDirection);
        }
    }
}