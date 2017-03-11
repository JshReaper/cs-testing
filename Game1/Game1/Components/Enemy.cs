using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Game1
{
    internal class Enemy : Component,IUpdateAble, ILoadable, IAnimateable
    {
        private IStrategy strategy;
        private Animator animator;
        private Direction direction;
        private Vector2 target = AI.Target;
        private List<bool> myWaypointChecks;
        private int wayPointSize;
        /// <summary>
        /// sets a reference to the attached gameobjects animator and sets DoColCheck to true on the collider
        /// </summary>
        /// <param name="gameObject"></param>
        public Enemy(GameObject gameObject) : base(gameObject)
        {
            myWaypointChecks = new List<bool>();
            for (int i = 0; i < AI.WayPoints.Count; i++)
            {
                myWaypointChecks.Add(false);
                wayPointSize++;
            }
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
            if (wayPointSize < AI.WayPoints.Count)
            {
                myWaypointChecks.Add(false);
                wayPointSize++;
            }
            if (GameObject.Transform.Posistion.X < target.X)
            {
                direction = Direction.Right;
            }
            
            for (int i = 0; i < AI.WayPoints.Count; i++)
            {
                if (!myWaypointChecks[i])
                {
                    var waypoint = AI.WayPoints[i];
                    if (waypoint.Y >= GameObject.Transform.Posistion.Y)
                    {
                        direction = Direction.Front;
                    }
                    if(waypoint.X +32 <= GameObject.Transform.Posistion.X)
                    {
                        myWaypointChecks[i] = true;
                    }
                    clearPath = myWaypointChecks[i];
                }
            }
            strategy = new Walk(GameObject.Transform,animator);
            strategy.Execute(direction);
        }

        private bool clearPath;
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
            animator.CreateAnimation("IdleLeft", new Animation(1, 0, 0, 32, 32, 0, Vector2.Zero));
            animator.CreateAnimation("IdleRight", new Animation(1, 0, 0, 32, 32, 0, Vector2.Zero));
            animator.CreateAnimation("IdleBack", new Animation(1, 0, 0, 32, 32, 0, Vector2.Zero));
            animator.CreateAnimation("WalkFront", new Animation(3, 0, 0, 32, 32, 5, Vector2.Zero));
            animator.CreateAnimation("WalkBack", new Animation(3, 0, 0, 32, 32, 5, Vector2.Zero));
            animator.CreateAnimation("WalkLeft", new Animation(3, 0, 0, 32, 32, 5, Vector2.Zero));
            animator.CreateAnimation("WalkRight", new Animation(3, 0, 0, 32, 32, 5, Vector2.Zero));
            animator.CreateAnimation("DieBack", new Animation(3, 0, 0, 32, 32, 5, Vector2.Zero));
            animator.CreateAnimation("DieFront", new Animation(3, 0, 0, 32, 32, 5, Vector2.Zero));
            animator.CreateAnimation("DieLeft", new Animation(3, 0, 0, 32, 32, 5, Vector2.Zero));
            animator.CreateAnimation("DieRight", new Animation(3, 0, 0, 32, 32, 5, Vector2.Zero));
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