using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Game1
{
    internal class Tower : Component, IUpdateAble, ILoadable, IAnimateable
    {
        private Animator animator;
        private Tile[,] mapTiles;

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

        public int MyXTile { get; private set; }
        public int MyYTile { get; private set; }

        /// <summary>
        /// Movement and such to be added !
        /// </summary>
        public void Update(GameTime gameTime)
        {
            if (GameWorld.Instance.Map != null)
            {
                mapTiles = GameWorld.Instance.Map.Tiles;
            }
            if (mapTiles != null)
            {
                for (int x = 0; x < mapTiles.GetLength(0); x++)
                {
                    for (int y = 0; y < mapTiles.GetLength(1); y++)
                    {
                        if (mapTiles[x, y].Pos.X <= GameObject.Transform.Position.X
                            && mapTiles[x, y].Pos.X + 32 >= GameObject.Transform.Position.X
                            && mapTiles[x, y].Pos.Y <= GameObject.Transform.Position.Y
                            && mapTiles[x, y].Pos.Y + 32 >= GameObject.Transform.Position.Y)
                        {
                            MyYTile = x;
                            MyXTile = y;
                        }
                    }
                }
            }

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