using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class PlayerBuilder:IGameObjectBuilder
    {
        private GameObject gameObject;
        public GameObject GetResult()
        {
            return gameObject;
        }

        public void BuildGameObject(Vector2 posistion, GraphicsDevice gd,float layerDepth,float animationFps)
        {
            gameObject = new GameObject(posistion,gd);
            gameObject.AddComponent(new SpriteRenderer(gameObject,"HeroSheet",layerDepth));
            gameObject.AddComponent(new Animator(gameObject, animationFps));
            gameObject.AddComponent(new Player(gameObject, 80));
            gameObject.AddComponent(new Collider(gameObject));
        }
    }
}