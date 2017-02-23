using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public class Collider:Component,IDrawAble,ILoadable, IUpdateAble
    {
        private SpriteRenderer spriteRenderer;
        private Texture2D texture2D;
        private bool doCollisionChecks = false;
        private List<Collider> otherColliders;
        public Rectangle CollisionBox
        {
            get
            {
                return new Rectangle((int) (GameObject.Transform.Posistion.X + spriteRenderer.Offset.X),
                    (int) (GameObject.Transform.Posistion.Y + spriteRenderer.Offset.Y), spriteRenderer.Rectangle.Width,
                    spriteRenderer.Rectangle.Height);
            }
        }

        public bool DoCollisionChecks { set { doCollisionChecks = value; } }
        public Collider(GameObject gameObject):base(gameObject)
        {
            GameWorld.Instance.Colliders.Add(this);
            otherColliders = new List<Collider>();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle topLine = new Rectangle(CollisionBox.X, CollisionBox.Y, CollisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(CollisionBox.X, CollisionBox.Y + CollisionBox.Height, CollisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(CollisionBox.X + CollisionBox.Width, CollisionBox.Y, 1, CollisionBox.Height);
            Rectangle leftLine = new Rectangle(CollisionBox.X, CollisionBox.Y, 1, CollisionBox.Height);
            spriteBatch.Draw(texture2D, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture2D, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture2D, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture2D, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);

        }

        public void LoadContent(ContentManager content)
        {
            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            texture2D = content.Load<Texture2D>("CollisionTexture");
        }

        public void Update()
        {
            CheckCollision();
        }

        

        private void CheckCollision()
        {
            if (doCollisionChecks)
            {
                foreach (Collider other in GameWorld.Instance.Colliders)
                {
                    if (other != this)
                    {
                        if (CollisionBox.Intersects(other.CollisionBox))
                        {
                            GameObject.OnCollisionStay(other);

                            if (!otherColliders.Contains(other))
                            {
                                otherColliders.Add(other);
                                GameObject.OnCollisionEnter(other);
                            }

                        }
                        else if (otherColliders.Contains(other))
                        {
                            otherColliders.Remove(other);
                            GameObject.OnCollisionExit(other);
                        }
                    }
                }
            }
        }
    }
}