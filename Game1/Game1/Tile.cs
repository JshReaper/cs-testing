using Game1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public class Tile : ILoadable, IDrawAble
    {
        private Vector2 pos;
        private Texture2D sprite;
        private Rectangle rectangle;
        private Color color;

        public Tile(Vector2 p,Color c)
        {
            pos = p;
            color = c;
        }

        public Vector2 Pos { get { return pos; } set { pos = value; } }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, pos, rectangle,color, 0, Vector2.Zero, 1, SpriteEffects.None, 0.1f);
        }
        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("tile");
            rectangle = new Rectangle(0, 0, sprite.Width, sprite.Height);
        }
    }
}
