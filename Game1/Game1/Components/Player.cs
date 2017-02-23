using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    class Player : Component,IUpdateAble,ILoadable,IAnimateable,ICollisionStay,ICollisionExit,ICollisionEnter
    {
        private float speed;
        private Animator animator;
        private IPlayerBehaviorStrategy playerBehavior;
        private Direction direction;
        private bool canMove = true;
        /// <summary>
        /// sets the speed, reference to the attached animator, sets DocolCheck to true to the attached Collider on the gameobject
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="speed"></param>
        public Player(GameObject gameObject, float speed) : base(gameObject)
        {
            this.speed = speed;
            animator =(Animator) gameObject.GetComponent("Animator");

            var collider = GameObject.GetComponent("Collider") as Collider;
            if (collider != null)
                collider.DoCollisionChecks = true;
        }
        /// <summary>
        /// gets the speed of the player
        /// </summary>
        public float Speed { get { return speed; } }
        /// <summary>
        /// Player movement and other behaviors gets updated based on input by the user
        /// </summary>
        public void Update()
        {
            KeyboardState keyState = Keyboard.GetState();
            if (canMove)
            {
                if (keyState.IsKeyDown(Keys.W) || keyState.IsKeyDown(Keys.S) || keyState.IsKeyDown(Keys.A) ||
                    keyState.IsKeyDown(Keys.D))
                {
                    if (!(playerBehavior is Walk))
                    {
                        playerBehavior = new Walk(GameObject.Transform,animator,this);
                        
                    }
                }
                else
                {
                    playerBehavior = new Idle(animator);
                }
                if (keyState.IsKeyDown(Keys.Space))
                {
                    playerBehavior = new Attack(animator);
                    canMove = false;
                }
            }
            playerBehavior.Execute(direction);
        }
        /// <summary>
        /// creates all animations
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            CreateAnimations();

        }
        /// <summary>
        /// adds all the animations and sets a default animation
        /// </summary>
        void CreateAnimations()
        {
            animator.CreateAnimation("IdleFront", new Animation(4, 0, 0, 90, 150, 6, Vector2.Zero));
            animator.CreateAnimation("IdleBack", new Animation(4, 0, 4, 90, 150, 6, Vector2.Zero));
            animator.CreateAnimation("IdleLeft", new Animation(4, 0, 8, 90, 150, 6, Vector2.Zero));
            animator.CreateAnimation("IdleRight", new Animation(4, 0, 12, 90, 150, 6, Vector2.Zero));
            animator.CreateAnimation("WalkFront", new Animation(4, 150, 0, 90, 150, 6, Vector2.Zero));
            animator.CreateAnimation("WalkBack", new Animation(4, 150, 4, 90, 150, 6, Vector2.Zero));
            animator.CreateAnimation("WalkLeft", new Animation(4, 150, 8, 90, 150, 6, Vector2.Zero));
            animator.CreateAnimation("WalkRight", new Animation(4, 150, 12, 90, 150, 6, Vector2.Zero));
            animator.CreateAnimation("AttackFront", new Animation(4, 300, 0, 145, 160, 8, new Vector2(-50, 0)));
            animator.CreateAnimation("AttackBack", new Animation(4, 465, 0, 170, 155, 8, new Vector2(-20, 0)));
            animator.CreateAnimation("AttackRight", new Animation(4, 620, 0, 150, 150, 8, Vector2.Zero));
            animator.CreateAnimation("AttackLeft", new Animation(4, 770, 0, 150, 150, 8, new Vector2(-60, 0)));
            animator.CreateAnimation("DieFront", new Animation(3, 920, 0, 150, 150, 5, Vector2.Zero));
            animator.CreateAnimation("DieBack", new Animation(3, 920, 3, 150, 150, 5, Vector2.Zero));
            animator.CreateAnimation("DieLeft", new Animation(3, 1070, 0, 150, 150, 5, Vector2.Zero));
            animator.CreateAnimation("DieRight", new Animation(3, 1070, 3, 150, 150, 5, Vector2.Zero));
            animator.PlayAnimation("IdleFront");
        }
        /// <summary>
        /// ensure that the player can not move while attacking
        /// </summary>
        /// <param name="animationName"></param>
        public void OnAnimationDone(string animationName)
        {
            if (animationName.Contains("Attack"))
            {
                canMove = true;
            }

        }
        /// <summary>
        /// do something while colliding
        /// </summary>
        /// <param name="other"></param>
        public void OnCollisionStay(Collider other)
        {
            
        }
        /// <summary>
        /// turns the color of the colliding object to red when entering the collision
        /// </summary>
        /// <param name="other"></param>
        public void OnCollisionEnter(Collider other)
        {
            var spriteRenderer = other.GameObject.GetComponent("SpriteRenderer") as SpriteRenderer;
            if (spriteRenderer != null)
                spriteRenderer.Color = Color.Red;
        }
        /// <summary>
        /// turns the color back to the original when the player stops colliding of the object being collided with
        /// </summary>
        /// <param name="other"></param>
        public void OnCollisionExit(Collider other)
        {
            var spriteRenderer = other.GameObject.GetComponent("SpriteRenderer") as SpriteRenderer;
            if (spriteRenderer != null)
                spriteRenderer.Color = Color.White;
        }
    }
}
