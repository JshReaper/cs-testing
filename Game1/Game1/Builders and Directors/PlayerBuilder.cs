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
        public void BuildGameObject(Vector2 posistion, GraphicsDevice gd,float layerDepth,float animationFps)
        {
            gameObject = new GameObject(posistion,gd);
            gameObject.AddComponent(new SpriteRenderer(gameObject,"HeroSheet",layerDepth));
            gameObject.AddComponent(new Animator(gameObject, animationFps));
            gameObject.AddComponent(new Collider(gameObject));
            gameObject.AddComponent(new Player(gameObject, 80));
        }
    }
}