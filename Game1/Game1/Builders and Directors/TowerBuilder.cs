using Microsoft.Xna.Framework;

namespace Game1
{
    class TowerBuilder : IGameObjectBuilder
    {
        private GameObject gameObject;
        /// <summary>
        /// returns the created gameobject
        /// </summary>
        /// <returns></returns>
        public GameObject GetResult()
        {
            return gameObject;
        }

        /// <summary>
        /// builds a enemy
        /// </summary>
        /// <param name="posistion"></param>
        /// <param name="layerDepth"></param>
        /// <param name="animationFps"></param>
        /// <param name="scale"></param>
        public void BuildGameObject(Vector2 posistion, float layerDepth, float animationFps, float scale)
        {
            gameObject = new GameObject(posistion);
            gameObject.AddComponent(new SpriteRenderer(gameObject, "tower", layerDepth, scale));
            gameObject.AddComponent(new Animator(gameObject, animationFps));
            gameObject.AddComponent(new Tower(gameObject));
        }
    }
}