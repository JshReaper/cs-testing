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
        static List<Vector2> waypoints = new List<Vector2>();

        private static Vector2 spawnPoint = new Vector2(0,(
            GameWorld.Instance.GraphicsDevice.PresentationParameters.Bounds.Height / 2) + 16);

        static Vector2 target = new Vector2(GameWorld.Instance.GraphicsDevice.PresentationParameters.Bounds.Right, (
            GameWorld.Instance.GraphicsDevice.PresentationParameters.Bounds.Height / 2) + 16);


        public static List<Vector2> WayPoints { get { return waypoints; } }
        public static Vector2 SpawnPoint { get { return spawnPoint; } }
        public static Vector2 Target { get { return target; } }

        public static void GenerateWayPoints()
        {
            waypoints.Clear();
            foreach (var to in GameWorld.Instance.towerPool.Objects)
            {
                if (to.Transform.Posistion.Y >= target.Y && to.Transform.Posistion.Y <= target.Y +32)
                {
                    waypoints.Add(new Vector2(to.Transform.Posistion.X,to.Transform.Posistion.Y+32));
                }
            }
        }
    }
}
