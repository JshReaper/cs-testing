using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game1
{
    public static class AI
    {
        //public static bool[,] notPasableArea { get; private set; }

        private static Vector2 spawnPoint = new Vector2(0,
            (GameWorld.Instance.GraphicsDevice.PresentationParameters.Bounds.Height / 2) + 16);

        static Vector2 target = new Vector2(GameWorld.Instance.GraphicsDevice.PresentationParameters.Bounds.Right,
            (GameWorld.Instance.GraphicsDevice.PresentationParameters.Bounds.Height / 2) + 16);

        static Tile[,] tiles;
        public static Vector2 SpawnPoint
        {
            get { return spawnPoint; }
        }

        public static Vector2 Target
        {
            get { return target; }
        }

        public static void GenerateNotPasableArea(float x, float y)
        {
            if (GameWorld.Instance.Map != null)
            {
                tiles = GameWorld.Instance.Map.Tiles;
                for (int i = 0; i < GameWorld.Instance.Map.Tiles.GetLength(0); i++)
                {
                    tiles[i, 0].type = TileType.Notpasable;
                    tiles[i, tiles.GetLength(1) - 1].type = TileType.Notpasable;
                }

                for (int tileX = 0; tileX < tiles.GetLength(0); tileX++)
                {
                    for (int tileY = 0; tileY < tiles.GetLength(1); tileY++)
                    {
                        if (tiles[tileX, tileY].pos.X == (int) x && tiles[tileX, tileY].pos.Y == (int) y)
                        {
                            tiles[tileX, tileY].type = TileType.Notpasable;
                        }
                    }
                }
            }
        }

        public static WayPoint[,] WayPoints(float enX,float enY)
        {
            WayPoint[,] points;
            int amountX = 0;
            int amountY = 0;



            points = new WayPoint[amountX, amountY];
            return points;
        } 
        public static Direction ChoseDirection(int enX, int enY,ref bool towerToRight,ref int savedX,ref int savedY)
        {
             

            int currentTileX = 0;
            int currentTileY = 0;
            Direction direction = Direction.Right;
            //for (int x = 0; x < tiles.GetLength(0); x++)
            //{
            //    for (int y = 0; y < tiles.GetLength(1); y++)
            //    {
            //        if (tiles[x, y].X <= enX && tiles[x,y].X +32 >= enX && tiles[x, y].Y <= enY && tiles[x, y].Y +32 >= enY)
            //        {
            //            currentTileX = x;
            //            currentTileY = y;
            //        }
            //    }
            //}


            //for (int x = currentTileX; x < tiles.GetLength(0); x++)
            //{
            //    for (int y = 0; y < tiles.GetLength(1); y++)
            //    {
            //        if (currentTileX != 0 || currentTileY != 0)
            //        if (currentTileX != tiles.GetLength(0) - 1 ||currentTileY != tiles.GetLength(1) - 1)
            //            if (notPasableArea != null)
            //            {
            //                    if (notPasableArea[x, currentTileY])
            //                    {
            //                        towerToRight = true;
            //                    }
            //            }
            //    }
            //}
            
            return direction;
        }
    }
}
