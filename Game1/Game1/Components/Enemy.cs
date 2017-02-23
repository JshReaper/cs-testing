using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Game1
{
    internal class Enemy : Component,IUpdateAble, ILoadable, IAnimateable,ICollisionStay, ICollisionExit, ICollisionEnter
    {

        private Animator animator;

        public Enemy(GameObject gameObject) : base(gameObject)
        {
            animator = (Animator)gameObject.GetComponent("Animator");

            var collider = GameObject.GetComponent("Collider") as Collider;
            if (collider != null)
                collider.DoCollisionChecks = true;
        }


        public void Update()
        {
            
        }

        public void LoadContent(ContentManager content)
        {
            CreateAnimations();

        }

        void CreateAnimations()
        {
            animator.CreateAnimation("IdleFront", new Animation(1, 0, 0, 100, 100, 0, Vector2.Zero));
            animator.CreateAnimation("IdleLeft", new Animation(1, 0, 1, 100, 100, 0, Vector2.Zero));
            animator.CreateAnimation("IdleRight", new Animation(1, 0, 2, 100, 100, 0, Vector2.Zero));
            animator.CreateAnimation("IdleBack", new Animation(1, 0, 3, 100, 100, 0, Vector2.Zero));
            animator.CreateAnimation("WalkFront", new Animation(4, 100, 0, 100, 100, 5, Vector2.Zero));
            animator.CreateAnimation("WalkBack", new Animation(4, 100, 4, 100, 100, 5, Vector2.Zero));
            animator.CreateAnimation("WalkLeft", new Animation(4, 200, 0, 100, 100, 5, Vector2.Zero));
            animator.CreateAnimation("WalkRight", new Animation(4, 200, 4, 100, 100, 5, Vector2.Zero));
            animator.CreateAnimation("DieBack", new Animation(4, 300, 0, 100, 100, 5, Vector2.Zero));
            animator.CreateAnimation("DieFront", new Animation(4, 300, 4, 100, 100, 5, Vector2.Zero));
            animator.CreateAnimation("DieLeft", new Animation(4, 400, 0, 100, 100, 5, Vector2.Zero));
            animator.CreateAnimation("DieRight", new Animation(4, 400, 4, 100, 100, 5, Vector2.Zero));
            animator.PlayAnimation("IdleFront");
        }

        public void OnAnimationDone(string animationName)
        {
            


        }

        public void OnCollisionStay(Collider other)
        {
            //SpriteRenderer s = (SpriteRenderer) GameObject.GetComponent("SpriteRenderer");
            //if(other.GameObject.GetComponent("Player") != null)
            //s.Color = Color.Red;
        }

        public void OnCollisionExit(Collider other)
        {
            
        }

        public void OnCollisionEnter(Collider other)
        {

        }
    }
}