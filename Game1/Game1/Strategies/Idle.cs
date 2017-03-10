namespace Game1
{
    class Idle : IStrategy
    {

        private Animator animator;
        /// <summary>
        /// sets the animator
        /// </summary>
        /// <param name="animator"></param>
        public Idle(Animator animator)
        {
            this.animator = animator;
        }
        /// <summary>
        /// plays the idle animation
        /// </summary>
        /// <param name="currentDirection"></param>
        public void Execute(Direction currentDirection)
        {
            animator.PlayAnimation("Idle" + currentDirection);
        }
    }
}