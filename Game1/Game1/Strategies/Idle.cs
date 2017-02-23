namespace Game1
{
    class Idle : IPlayerBehaviorStrategy
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