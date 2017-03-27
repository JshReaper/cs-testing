using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Game1
{
    internal class Enemy : Component,IUpdateAble, ILoadable, IAnimateable
    {
        private Animator animator;
        Vector2 direction = Vector2.Zero;
        int myMapY;
        int myMapX;
        Tile[,] mapTiles;
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
            if(GameWorld.Instance.Map != null)
            {
                mapTiles = GameWorld.Instance.Map.Tiles;
            }
            if(mapTiles != null)
            {
                for (int x = 0; x < mapTiles.GetLength(0); x++)
                {
                    for (int y = 0; y < mapTiles.GetLength(1); y++)
                    {
                        if (mapTiles[x, y].Pos.X <= GameObject.Transform.Posistion.X && mapTiles[x, y].Pos.X + 32 >= GameObject.Transform.Posistion.X && mapTiles[x, y].Pos.Y <= GameObject.Transform.Posistion.Y && mapTiles[x, y].Pos.Y + 32 >= GameObject.Transform.Posistion.Y)
                        {
                            myMapX = x;
                            myMapY = y;
                        }
                    }
                }
            }
            GameObject.Transform.Posistion += direction;
           
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

        public void UpdateMoveMent()
        {
            switch (GameWorld.Instance.rnd.Next(1, 5))
            {
                case 1:
                    direction = new Vector2(0, 1);
                    break;
                case 2:

                //    direction = new Vector2(0, -1);
                    break;
                case 3:

                    direction = new Vector2(1,0);
                    break;
                case 4:
                 //   direction = new Vector2(-1, 0);
                    break;
            }
        }
    }
}