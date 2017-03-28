using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Game1
{
    public class Enemy : Component,IUpdateAble, ILoadable, IAnimateable
    {
        private Animator animator;
        Vector2 direction = Vector2.Zero;
        private List<Point> myPath;
        int myXTile;
        int myYTile;
        private int myID;
        Tile[,] mapTiles;
        AstarThreadWorker astarThreadWorkerTemp, astarThreadWorker;
        List<Vector2> WayPointsList;
        private bool firstCheck = true;
        private int savedY;
        WayPoint wayPoint;
        /// <summary>
        /// sets a reference to the attached gameobjects animator and sets DoColCheck to true on the collider
        /// </summary>
        /// <param name="gameObject"></param>
        public Enemy(GameObject gameObject, int myId) : base(gameObject)
        {
            myID = myId;
            animator = (Animator)gameObject.GetComponent("Animator");
            WayPointsList = new List<Vector2>();

            wayPoint = new WayPoint();
            var collider = GameObject.GetComponent("Collider") as Collider;
            if (collider != null)
                collider.DoCollisionChecks = true;
        }
        void Astar(GameTime gameTime, Map map, int enemyID, List<Enemy> enemies)
        {
            if (savedY != myYTile)
            {
                astarThreadWorker = null;
                AstarManager.AddNewThreadWorker(new Node(new Vector2(myXTile, myYTile)),
                    new Node(new Vector2(map.sizeX-1, myYTile)), map, true, enemyID);
                savedY = myYTile;
            }

            AstarManager.AstarThreadWorkerResults.TryPeek(out astarThreadWorkerTemp);
            
            if (astarThreadWorkerTemp != null)
                if (astarThreadWorkerTemp.WorkerIDNumber == enemyID)
                {
                    AstarManager.AstarThreadWorkerResults.TryDequeue(out astarThreadWorker);

                    if (astarThreadWorker != null)
                    {
                        wayPoint = new WayPoint();

                        WayPointsList = astarThreadWorker.astar.GetFinalPath();

                        for (int i = 0; i < WayPointsList.Count; i++)
                            WayPointsList[i] = new Vector2(WayPointsList[i].X * 32, WayPointsList[i].Y * 32);
                    }
                }

            if (WayPointsList.Count > 0)
            {
              //  Avoidence(gameTime, enemies, UnitID);
                wayPoint.MoveTo(gameTime, this, WayPointsList, 0.1f);
            }
        }

        /// <summary>
        /// Movement and such to be added !
        /// </summary>
        public void Update(GameTime gameTime)
        {
            gameTime = GameWorld.Instance.upGameTime;
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
                        if (mapTiles[x, y].Pos.X <= GameObject.Transform.Position.X 
                            && mapTiles[x, y].Pos.X + 32 > GameObject.Transform.Position.X)
                        {
                            myXTile = x;
                        }
                        if (mapTiles[x, y].Pos.Y -1 <= GameObject.Transform.Position.Y
                            && mapTiles[x, y].Pos.Y + 32 > GameObject.Transform.Position.Y)
                        {
                            myYTile = y;
                        }
                    }
                }
            }

            Astar(gameTime, GameWorld.Instance.Map, myID, GameWorld.Instance.EnemyPool.Enemies);
            GameObject.Transform.Position += direction;
           
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
            
                //right
                direction = new Vector2(0, 1);
            
                //left
                direction = new Vector2(0, -1);
           
                //down
                direction = new Vector2(1, 0);
            
                //up
                direction = new Vector2(-1, 0);
            
            
        }
    }
}