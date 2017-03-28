using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public class Map : ILoadable, IDrawAble, IUpdateAble
    {
        public Tile[,] Tiles { get;}
        public int sizeX { get; private set; }
        public int sizeY { get; private set; }

        public Map(int sizeX, int sizeY)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
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

        public void Update(GameTime gameTime)
        {
            
        }
    }
}