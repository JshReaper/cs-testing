﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Game1
{
    internal class Enemy : Component,IUpdateAble, ILoadable, IAnimateable
    {
        private IStrategy strategy;
        private Animator animator;
        private Direction direction;
        Vector2 target = new Vector2(GameWorld.Instance.GraphicsDevice.PresentationParameters.Bounds.Right, GameWorld.Instance.GraphicsDevice.PresentationParameters.Bounds.Height / 2);

        /// <summary>
        /// sets a reference to the attached gameobjects animator and sets DoColCheck to true on the collider
        /// </summary>
        /// <param name="gameObject"></param>
        public Enemy(GameObject gameObject) : base(gameObject)
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
            if (GameObject.Transform.Posistion.X < target.X)
            {
                direction = Direction.Right;
            }
            if (GameObject.Transform.Posistion.Y > target.Y && clearPath)
            {
                direction = Direction.Back;
            }
           if (GameObject.Transform.Posistion.X > target.X)
            {

                direction = Direction.Left;
           }
            
            if (GameObject.Transform.Posistion.Y < target.Y && clearPath)
            {
                direction = Direction.Front;
            }
            clearPath = true;
            foreach (var o in GameWorld.GameObjects)
            {
                if (o != GameObject)
                {
                    if (o.Transform.Posistion.X +32 > GameObject.Transform.Posistion.X) { 
                        clearPath = false;
                        if (o.Transform.Posistion.Y <= GameObject.Transform.Posistion.Y + 32)
                            direction = Direction.Back;
                    }
                    
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