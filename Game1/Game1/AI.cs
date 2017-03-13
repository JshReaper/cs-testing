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
        public static bool[,] notPasableArea { get; private set; }

        private static Vector2 spawnPoint = new Vector2(0,
            (GameWorld.Instance.GraphicsDevice.PresentationParameters.Bounds.Height / 2) + 16);

        static Vector2 target = new Vector2(GameWorld.Instance.GraphicsDevice.PresentationParameters.Bounds.Right,
            (GameWorld.Instance.GraphicsDevice.PresentationParameters.Bounds.Height / 2) + 16);

        static Vector2[,] tiles = GameWorld.Instance.Map.Tiles;
        public static Vector2 SpawnPoint
        {
            get { return spawnPoint; }
        }

        public static Vector2 Target
        {
            get { return target; }
        }

        public static void GenerateWayPoints(float x, float y)
        {
            if (notPasableArea == null)
            {
                notPasableArea =
                    new bool[GameWorld.Instance.Map.Tiles.GetLength(0), GameWorld.Instance.Map.Tiles.GetLength(1)];
                for (int i = 0; i < notPasableArea.GetLength(0); i++)
                {
                    notPasableArea[i, 0] = true;
                    notPasableArea[i, notPasableArea.GetLength(1) - 1] = true;
                }
            }
            for (int tileX = 0; tileX < GameWorld.Instance.Map.Tiles.GetLength(0); tileX++)
            {
                for (int tileY = 0; tileY < GameWorld.Instance.Map.Tiles.GetLength(1); tileY++)
                {
                    if (tiles[tileX, tileY].X == (int) x && tiles[tileX, tileY].Y == (int) y)
                    {
                        notPasableArea[tileX, tileY] = true;
                    }
                }
            }
        }

        public static Direction ChoseDirection(int enX, int enY,ref bool towerToRight,ref int savedX,ref int savedY)
        {
             

            int currentTileX = 0;
            int currentTileY = 0;
            Direction direction = Direction.Right;
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    if (tiles[x, y].X <= enX && tiles[x,y].X +32 >= enX && tiles[x, y].Y <= enY && tiles[x, y].Y +32 >= enY)
                    {
                        currentTileX = x;
                        currentTileY = y;
                    }
                }
            }


            for (int x = currentTileX; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    if (currentTileX != 0 || currentTileY != 0)
                    if (currentTileX != tiles.GetLength(0) - 1 ||currentTileY != tiles.GetLength(1) - 1)
                        if (notPasableArea != null)
                        {
                                if (notPasableArea[x, currentTileY])
                                {
                                    //select shortes rute
                                }
                        }

                }
            }



            if (currentTileX == 0 || currentTileY == 0) return direction;
            if (currentTileX == tiles.GetLength(0) - 1 || currentTileY == tiles.GetLength(1) - 1) return direction;
            if (notPasableArea == null) return direction;
            if (notPasableArea[currentTileX +1, currentTileY])
            {
                towerToRight = true;
                savedY =(int) tiles[currentTileX, currentTileY].Y;
                savedX = (int)tiles[currentTileX, currentTileY].X;
            }
            if (towerToRight)
            {
                int towersUp = 0;
                int towersDown = 1;
                if (towersDown < towersUp)
                {
                    direction = Direction.Front;
                }
                else
                {
                    direction = Direction.Back;
                }
                if (notPasableArea[currentTileX, currentTileY - 1])
                {
                    direction = Direction.Front;
                }
                else if (notPasableArea[currentTileX, currentTileY + 1])
                {
                    direction = Direction.Back;
                }
            }
            return direction;
        }
    }
}
