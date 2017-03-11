using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    class Walk : IStrategy
    {

        private Animator animator;
        private Transform transform;
        /// <summary>
        /// sets the animator transform and player
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="animator"></param>
        /// <param name="player"></param>
        public Walk(Transform transform,Animator animator)
        {
            this.transform = transform;
            this.animator = animator;
        }
        /// <summary>
        /// moves the player plays the animation based on that movement
        /// </summary>
        /// <param name="currentDirection"></param>
        public void Execute(Direction currentDirection)
        {
            Vector2 translation = Vector2.Zero;
            
            if (currentDirection == Direction.Back)
            {
                translation += new Vector2(0, -1);
            }
            if (
                currentDirection == Direction.Right)
            {
                translation += new Vector2(1, 0);

            }
            if (
                currentDirection == Direction.Left)
                {
                translation += new Vector2(-1, 0);
            }
            if (
                currentDirection == Direction.Front)
            {
                translation += new Vector2(0, +1);

            }
            transform.Translate(translation * GameWorld.Instance.deltaTime * 10);
            animator.PlayAnimation("Walk" + currentDirection);
        }
    }
}