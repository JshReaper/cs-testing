using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class EnemyPool
    {
        private List<GameObject> activeGameObjects;
        private List<GameObject> inactiveGameObjects;
        /// <summary>
        /// sets the active and inactive gameobject lists
        /// </summary>
        public EnemyPool()
        {
            activeGameObjects = new List<GameObject>();
            inactiveGameObjects = new List<GameObject>();
        }
        /// <summary>
        /// creates a enemy either pulls one from the inactive list or creates a new one
        /// </summary>
        /// <param name="posistion"></param>
        /// <param name="graphicsDevice"></param>
        /// <param name="layerDepth"></param>
        /// <param name="animationFps"></param>
        /// <returns></returns>
        public GameObject Create(Vector2 posistion, GraphicsDevice graphicsDevice,float layerDepth,float animationFps)
        {
            if (inactiveGameObjects.Count > 0)
            {
                //add pos laydepth and such back
                if((inactiveGameObjects[0].GetComponent("Collider") as Collider) != null)
                (inactiveGameObjects[0].GetComponent("Collider") as Collider).DoCollisionChecks = true;
                
                activeGameObjects.Add(inactiveGameObjects[0]);
                inactiveGameObjects.RemoveAt(0);
                return activeGameObjects[activeGameObjects.Count - 1];
            }
            else
            {
                EnemyBuilder en = new EnemyBuilder();
                en.BuildGameObject(posistion, graphicsDevice, layerDepth, animationFps);
                GameObjectDirector gameObjectDirector = new GameObjectDirector(en);
                activeGameObjects.Add(gameObjectDirector.Construct());
                return gameObjectDirector.Construct(); 
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
            gameObject.Transform.Posistion = Vector2.Zero;

            if ((gameObject.GetComponent("Collider") as Collider) != null)
                (gameObject.GetComponent("Collider") as Collider).DoCollisionChecks = false;

        }
    }
}