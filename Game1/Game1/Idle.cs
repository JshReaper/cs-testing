namespace Game1
{
    class Idle : IPlayerBehavior
    {

        private Animator animator;

        public Idle(Animator animator)
        {
            this.animator = animator;
        }

        public void Execute(Direction currentDirection)
        {
            animator.PlayAnimation("Idle" + currentDirection);
        }
    }
}