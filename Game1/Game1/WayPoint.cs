using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Game1
{
    class WayPoint
    {
        public int WayPointIndex;
        public bool ReachedDestination;

        public void MoveTo(GameTime gameTime, Enemy enemy, List<Vector2> DestinationWaypoint, float Speed)
        {
            if (DestinationWaypoint.Count > 0)
            {
                if (!ReachedDestination)
                {
                    float Distance = Vector2.Distance(enemy.GameObject.Transform.Position, DestinationWaypoint[WayPointIndex]);
                    Vector2 Direction = DestinationWaypoint[WayPointIndex] - enemy.GameObject.Transform.Position;
                    Direction.Normalize();

                    if (Distance > Direction.Length())
                        enemy.GameObject.Transform.Position += Direction * (float)(Speed * gameTime.ElapsedGameTime.TotalMilliseconds);
                    else
                    {
                        if (WayPointIndex >= DestinationWaypoint.Count - 1)
                        {
                            enemy.GameObject.Transform.Position += Direction;
                            ReachedDestination = true;
                        }
                        else
                            WayPointIndex++;
                    }
                }
            }
        }
    }
}