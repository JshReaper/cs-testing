using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public class SpriteRenderer: Component,IDrawAble, ILoadable
    {
        private Rectangle rectangle;
        private Texture2D sprite;
        string spriteName;
        private float layerDepth;
        public Vector2 Offset { get; set; }
        public Rectangle Rectangle { get {return rectangle; } set { rectangle = value; } }
        public Texture2D Sprite { get { return sprite; } }

        public SpriteRenderer(GameObject gameObject, string spriteName, float layerDepth) : base(gameObject)
        {
            this.spriteName = spriteName;
            this.layerDepth = layerDepth;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, GameObject.Transform.Posistion+Offset,rectangle,Color.White,GameObject.Transform.Rotation,GameObject.Transform.Origin,GameObject.Transform.Scale,SpriteEffects.None,layerDepth);
            
        }

        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>(spriteName);
            

        }
    }
}