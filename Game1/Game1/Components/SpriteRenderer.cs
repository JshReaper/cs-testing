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
        private float scale;
        /// <summary>
        /// sets and gets the current offset
        /// </summary>
        public Vector2 Offset { get; set; }
        /// <summary>
        /// gets and sets the current rectangle 
        /// </summary>
        public Rectangle Rectangle { get {return rectangle; } set { rectangle = value; } }
        /// <summary>
        /// gets the current sprite
        /// </summary>
        public Texture2D Sprite { get { return sprite; } }
        /// <summary>
        /// gets and sets the current color
        /// </summary>
        public Color Color { get { return color; } set { color = value; } }

        public float Scale { get { return scale; } }

        private Color color;
        /// <summary>
        /// sets the color to a default white, sets the spritename and laydepth 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="spriteName"></param>
        /// <param name="layerDepth"></param>
        public SpriteRenderer(GameObject gameObject, string spriteName, float layerDepth, float scale) : base(gameObject)
        {
            color = Color.White;
            this.spriteName = spriteName;
            this.layerDepth = layerDepth;
            this.scale = scale;
        }
        /// <summary>
        /// draws the Object
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, GameObject.Transform.Posistion+Offset,rectangle,color,GameObject.Transform.Rotation,GameObject.Transform.Origin,scale,SpriteEffects.None,layerDepth);
            
        }
        /// <summary>
        /// sets the sprite equal to the spritename
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>(spriteName);
            

        }
    }
}