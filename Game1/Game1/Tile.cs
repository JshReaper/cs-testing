using System.Threading;
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

    public class Map : ILoadable, IDrawAble
    {
        public Tile[,] Tiles { get; }

        public Map(int sizeX, int sizeY)
        {
            Tiles = new Tile[sizeX, sizeY];

            for (int x = 0; x < Tiles.GetLength(0); x++)
            {
                for (int y = 0; y < Tiles.GetLength(1); y++)
                {
                    Tiles[x, y] = new Tile(new Vector2(x * 32, y * 32), Color.White);
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var tile in Tiles)
            {
                tile.Draw(spriteBatch);
            }
        }
        public void LoadContent(ContentManager content)
        {
            foreach (var tile in Tiles)
            {
                tile.LoadContent(content);

            }
        }
    }

    public class AI
    {
        private static AI instance = null;
        private Thread enMove;
        public static AI Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AI();
                }
                return instance;
            }
        }

        private AI()
        {
        }

        public void Start()
        {
            enMove = new Thread(EnemyMoveMent) { IsBackground = true };
            enMove.Start();
        }
        private void EnemyMoveMent()
        {
            while (GameWorld.Instance.Running)
            {
                foreach (var o in GameWorld.Instance.GameObjects)
                {
                    Enemy en = o.GetComponent("Enemy") as Enemy;
                    en?.UpdateMoveMent();
                }
            }
        }
    }
}
