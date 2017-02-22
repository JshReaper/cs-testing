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
    class Player : Component,IUpdateAble,ILoadable,IAnimateable
    {
        private float speed;
        private Animator animator;
        private IPlayerBehavior playerBehavior;
        private Direction direction;
        private bool canMove = true;

        public Player(GameObject gameObject, float speed) : base(gameObject)
        {
            this.speed = speed;
            animator =(Animator) gameObject.GetComponent("Animator");
        }

        public float Speed { get { return speed; } }

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
                        if (keyState.IsKeyDown(Keys.W))
                        {
                            direction = Direction.Back;
                        }
                        if (keyState.IsKeyDown(Keys.S))
                        {
                            direction = Direction.Front;
                        }
                        if (keyState.IsKeyDown(Keys.A))
                        {
                            direction = Direction.Left;
                        }
                        if (keyState.IsKeyDown(Keys.D))
                        {
                            direction = Direction.Right;
                        }
                        
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
      //      Move();
        }

        public void LoadContent(ContentManager content)
        {
            CreateAnimations();

        }

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

        public void OnAnimationDone(string animationName)
        {
            if (animationName.Contains("Attack"))
            {
                canMove = true;
            }

        }
    }
}
