using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class EnemyBuilder:IGameObjectBuilder
    {
        private GameObject gameObject;
        public GameObject GetResult()
        {
            return gameObject;
        }

        public void BuildGameObject(Vector2 posistion, GraphicsDevice gd, float layerDepth, float animationFps)
        {
            gameObject = new GameObject(posistion,gd);
            gameObject.AddComponent(new SpriteRenderer(gameObject,"SlimeSheet",layerDepth));
            gameObject.AddComponent(new Animator(gameObject, animationFps));
            gameObject.AddComponent(new Enemy(gameObject));
        }
    }
}