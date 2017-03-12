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

        private static Vector2 spawnPoint = new Vector2(0,(
            GameWorld.Instance.GraphicsDevice.PresentationParameters.Bounds.Height / 2) + 16);

        static Vector2 target = new Vector2(GameWorld.Instance.GraphicsDevice.PresentationParameters.Bounds.Right, (
            GameWorld.Instance.GraphicsDevice.PresentationParameters.Bounds.Height / 2) + 16);

        
        public static Vector2 SpawnPoint { get { return spawnPoint; } }
        public static Vector2 Target { get { return target; } }

        public static void GenerateWayPoints( float x, float y)
        {
            Vector2[,] tiles = GameWorld.Instance.Map.Tiles;
            if (notPasableArea == null)
            {
                notPasableArea = new bool[GameWorld.Instance.Map.Tiles.GetLength(0), GameWorld.Instance.Map.Tiles.GetLength(1)];
                for (int i = 0; i < notPasableArea.GetLength(0); i++)
                {
                    notPasableArea[i, 0] = true;
                    notPasableArea[i, notPasableArea.GetLength(1) -1] = true;
                }
            }
            for (int tileX = 0; tileX < GameWorld.Instance.Map.Tiles.GetLength(0); tileX++)
            {
                for (int tileY = 0; tileY < GameWorld.Instance.Map.Tiles.GetLength(1); tileY++)
                {
                    if (tiles[tileX, tileY].X == (int) x && tiles[tileX, tileY].Y == (int)y)
                    {
                        notPasableArea[tileX, tileY] = true;
                    }
                }
            }
        }
    }
}
