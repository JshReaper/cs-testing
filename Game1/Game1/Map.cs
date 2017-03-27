using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public class Map : ILoadable, IDrawAble, IUpdateAble
    {
        public Tile[,] Tiles { get;}
        private static Tile[,] sTiles;
        private static int[,] intTiles;

        public int GetMap(int x, int y)
        {
            if ((x < 0) || (x > 9))
                return (-1);
            if ((y < 0) || (y > 9))
                return (-1);
            if(intTiles != null)
            return (intTiles[y, x]);
            return -1;
        }
        public Map(int sizeX, int sizeY)
        {
            Tiles = new Tile[sizeX, sizeY];
            intTiles = new int[sizeX,sizeY];
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

        public void Update()
        {
            if (sTiles == null)
            {
                sTiles = Tiles;
            }
        }
    }
}