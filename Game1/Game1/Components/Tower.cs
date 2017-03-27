using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Game1
{
    internal class Tower : Component, IUpdateAble, ILoadable, IAnimateable
    {
        private Animator animator;
        /// <summary>
        /// sets a reference to the attached gameobjects animator and sets DoColCheck to true on the collider
        /// </summary>
        /// <param name="gameObject"></param>
        public Tower(GameObject gameObject) : base(gameObject)
        {
            animator = (Animator)gameObject.GetComponent("Animator");

            var collider = GameObject.GetComponent("Collider") as Collider;
            if (collider != null)
                collider.DoCollisionChecks = true;
        }

        /// <summary>
        /// Movement and such to be added !
        /// </summary>
        public void Update()
        {
            
        }
        /// <summary>
        /// creates animations
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            CreateAnimations();

        }
        /// <summary>
        /// adds all the animations required and sets a default
        /// </summary>
        void CreateAnimations()
        {
            animator.CreateAnimation("IdleFront", new Animation(1, 0, 0, 32, 32, 0, Vector2.Zero));
            animator.PlayAnimation("IdleFront");
        }
        /// <summary>
        /// TO BE ADDED, anything that should accur right after an animation
        /// </summary>
        /// <param name="animationName"></param>
        public void OnAnimationDone(string animationName)
        {

        }

    }
}