using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public class  GameObject : Component
    {
        protected Texture2D sprite;
        private Transform transform;
        public Transform Transform { get { return transform; } }
       
        protected Vector2 scale;
        protected GraphicsDevice gd;
        private List<Component> components;
        public GameObject(Vector2 posistion, GraphicsDevice gd)
        {
            components = new List<Component>();
            this.transform = new Transform(this, posistion);
            this.gd = gd;
        }

        public void AddComponent(Component component)
        {
            components.Add(component);
        }

        public void RemoveComponent(string component)
        {
            Component c = GetComponent(component);
            if (c != null)
            {
                components.Remove(c);
            }
        }
        public Component GetComponent(string component)
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].GetType().Name == component)
                {
                    return components[i];
                }
            }
            return null;
        }

        public virtual void LoadContent(ContentManager content)
        {
            


            foreach (Component component in components)
            {
                if (component is ILoadable)
                {
                    (component as ILoadable).LoadContent(content);
                }
            }
            SpriteRenderer s = (SpriteRenderer) GetComponent("SpriteRenderer");
            sprite = s.Sprite;
        }

        public virtual void Update()
        {
            foreach (Component component in components)
            {
                if (component is IUpdateAble)
                {
                    (component as IUpdateAble).Update();
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
              foreach (Component component in components)
            {
                if (component is IDrawAble)
                {
                    (component as IDrawAble).Draw(spriteBatch);
                }
            }
        }
        
        protected Rectangle CollisionBox
        {
            get
            {
                return new Rectangle((int)transform.Posistion.X,(int)transform.Posistion.Y,(int)(sprite.Width*scale.X), (int)(sprite.Height*scale.X));
                
            }
            set { CollisionBox = value; }
        }
        public bool IsCollidingWith(GameObject other)
        {
            return CollisionBox.Intersects(other.CollisionBox);
        }
        public void CheckCollision()
        {
            foreach (GameObject go in GameWorld.GameObjects)
            {
                if (go != this)
                {
                    if (this.IsCollidingWith(go))
                    {
                        OnCollision(go);
                    }
                }
            }
        }
        protected virtual void OnCollision(GameObject other)
        {

        }

        public void OnAnimationDone(string animationName)
        {
            foreach (Component component in components)
            {
                if (component is IAnimateable) //Checks if any components are IAnimateable
                {
                    //If a component is IAnimateable we call the local implementation of the method
                    (component as IAnimateable).OnAnimationDone(animationName);
                }
            }
        }
    }
}
