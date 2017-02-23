using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class EnemyPool
    {
        private List<GameObject> activeGameObjects;
        private List<GameObject> inactiveGameObjects;

        public EnemyPool()
        {
            activeGameObjects = new List<GameObject>();
            inactiveGameObjects = new List<GameObject>();
        }
        public GameObject Create(Vector2 posistion, GraphicsDevice graphicsDevice,float layerDepth,float animationFps)
        {
            if (inactiveGameObjects.Count > 0)
            {
                //add pos laydepth and such back

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

        public void ReleaseObject(GameObject gameObject)
        {
            CleanUp(gameObject);
            inactiveGameObjects.Add(gameObject);
            activeGameObjects.Remove(gameObject);
        }

        void CleanUp(GameObject gameObject)
        {
            gameObject.Transform.Posistion = Vector2.Zero;

        }
    }
}