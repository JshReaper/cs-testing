using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class PlayerBuilder:IGameObjectBuilder
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
        /// builds a player
        /// </summary>
        /// <param name="posistion"></param>
        /// <param name="gd"></param>
        /// <param name="layerDepth"></param>
        /// <param name="animationFps"></param>
        public void BuildGameObject(Vector2 posistion, float layerDepth,float animationFps,float scale)
        {
            gameObject = new GameObject(posistion);
            gameObject.AddComponent(new SpriteRenderer(gameObject, "greenMan", layerDepth,scale));
            gameObject.AddComponent(new Animator(gameObject, animationFps));
            gameObject.AddComponent(new Collider(gameObject));
            gameObject.AddComponent(new Player(gameObject, 80));
        }
    }
}