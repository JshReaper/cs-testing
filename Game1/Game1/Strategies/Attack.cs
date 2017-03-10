namespace Game1
{
    class Attack : IStrategy
    {
        private Animator animator;
        /// <summary>
        /// sets the animator
        /// </summary>
        /// <param name="animator"></param>
        public Attack(Animator animator)
        {
            this.animator = animator;
        }
        /// <summary>
        /// runs the attack animation
        /// </summary>
        /// <param name="currentDirection"></param>
        public void Execute(Direction currentDirection)
        {

            animator.PlayAnimation("Attack"+ currentDirection);
        }
    }
}