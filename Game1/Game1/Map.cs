using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    public class Map
    {
        private Texture2D sprite;
        private Vector2[,] tiles;
        private Rectangle rectangle;
        private int mapSizeX;
        private int mapSizeY;
        private int toPlaceAtX;
        private int toPlaceAtY;
        public Vector2[,] Tiles { get { return tiles; } }
        public Map()
        {
            mapSizeX = 25;
            mapSizeY = 15;
            tiles = new Vector2[mapSizeX,mapSizeY];
        }
        public void GenerateMap()
        {
            for (int x = 0; x < mapSizeX; x++)
            {
                toPlaceAtX = x * 32;
                for (int y = 0; y < mapSizeY; y++)
                {
                    toPlaceAtY = y * 32;
                    tiles[x,y] =( new Vector2(toPlaceAtX, toPlaceAtY));
                }
            }
        }
        public void PlaceTurret(float x, float y)
        {
            bool tileAvailable = true;
            bool canPlace = false;
            foreach (var t in tiles)
            {
                if (t.X + 32 >x && t.X < x)
                {
                    x = t.X;
                }
                if (t.Y + 32 >y && t.Y < y)
                {
                    y = t.Y;
                    canPlace = true;
                }
            }
            if (x < 32 || x >= GameWorld.Instance.GraphicsDevice.PresentationParameters.Bounds.Right -32)
            {
                canPlace = false;
            }
            if (y < 32 || y >= GameWorld.Instance.GraphicsDevice.PresentationParameters.Bounds.Bottom - 32)
            {
                canPlace = false;
            }
            foreach (var o in GameWorld.Instance.towerPool.Objects)
            {
                if (o.Transform.Posistion.X == x && o.Transform.Posistion.Y == y)
                {
                    tileAvailable = false;
                }
            }
            if (tileAvailable && canPlace)
            { 
                GameWorld.Instance.GameObjectsToAdd.Add(GameWorld.Instance.towerPool.Create(new Vector2(x,y), 1, 5, 0.5f));
                AI.GenerateNotPasableArea(x,y);
            }
        }
        /// <summary>
        /// draws the Object
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Vector2 t in tiles)
            {
                if (t.Y == 0)
                {

                    spriteBatch.Draw(sprite, t, rectangle, Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.1f);
                }
                else if(t.Y == GameWorld.Instance.GraphicsDevice.PresentationParameters.Bounds.Bottom-32)
                {

                    spriteBatch.Draw(sprite, t, rectangle, Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.1f);
                }
                else if (t.X == 0)
                {
                    spriteBatch.Draw(sprite, t, rectangle, Color.Blue, 0, Vector2.Zero, 1, SpriteEffects.None, 0.1f);
                }
                else if (t.X == GameWorld.Instance.GraphicsDevice.PresentationParameters.Bounds.Right -32)
                {
                    spriteBatch.Draw(sprite, t, rectangle, Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, 0.1f);
                }
                else
                {
                    spriteBatch.Draw(sprite, t, rectangle, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.1f);
                }
            }
        }
        /// <summary>
        /// sets the sprite equal to the spritename
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("tile");
            rectangle = new Rectangle(0, 0, sprite.Width, sprite.Height);
        }
    }
}
