using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Game1
{
    public class EnemyPool
    {
        private List<GameObject> activeGameObjects;
        private List<GameObject> inactiveGameObjects;
        public List<Enemy> Enemies { get; }

        /// <summary>
        /// sets the active and inactive gameobject lists
        /// </summary>
        public EnemyPool()
        {
            activeGameObjects = new List<GameObject>();
            inactiveGameObjects = new List<GameObject>();
            Enemies = new List<Enemy>(); 
        }
        /// <summary>
        /// creates a enemy either pulls one from the inactive list or creates a new one
        /// </summary>
        /// <param name="posistion"></param>
        /// <param name="graphicsDevice"></param>
        /// <param name="layerDepth"></param>
        /// <param name="animationFps"></param>
        /// <returns></returns>
        public GameObject Create(Vector2 posistion,float layerDepth,float animationFps,float scale)
        {
            if (inactiveGameObjects.Count > 0)
            {
                activeGameObjects.Add(inactiveGameObjects[0]);
                inactiveGameObjects.RemoveAt(0);
                return activeGameObjects[activeGameObjects.Count - 1];
            }
            else
            {
                GameObjectDirector gameObjectDirector = new GameObjectDirector(new EnemyBuilder());
                GameObject go = gameObjectDirector.Construct(posistion, layerDepth, animationFps, scale);
                activeGameObjects.Add(go);
                Enemies.Add(go.GetComponent("Enemy") as Enemy);
                return go; 
            }
        }
        /// <summary>
        /// add the gameobject to the inactive list and removes from the active list
        /// </summary>
        /// <param name="gameObject"></param>
        public void ReleaseObject(GameObject gameObject)
        {
            CleanUp(gameObject);
            inactiveGameObjects.Add(gameObject);
            activeGameObjects.Remove(gameObject);
        }
        /// <summary>
        /// resets all data and removes all references
        /// </summary>
        /// <param name="gameObject"></param>
        void CleanUp(GameObject gameObject)
        {
            gameObject.Transform.Position = new Vector2(0,GameWorld.Instance.GraphicsDevice.PresentationParameters.Bounds.Height/2);

            if (gameObject.GetComponent("Collider") as Collider != null)
                (gameObject.GetComponent("Collider") as Collider).DoCollisionChecks = false;

        }
    }
}