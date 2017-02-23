namespace Game1
{
    class Attack : IPlayerBehaviorStrategy
    {
        private Animator animator;

        public Attack(Animator animator)
        {
            this.animator = animator;
        }
        public void Execute(Direction currentDirection)
        {

            animator.PlayAnimation("Attack"+ currentDirection);
        }
    }
}