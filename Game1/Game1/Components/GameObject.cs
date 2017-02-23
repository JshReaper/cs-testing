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
        private bool isLoaded = false;

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


            if (!isLoaded)
            { 
                foreach (Component component in components)
            {
                if (component is ILoadable)
                {
                    (component as ILoadable).LoadContent(content);
                }
            }
                isLoaded = true;
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

        public void OnCollisionStay(Collider other)
        {
            foreach (Component component in components)
            {
                if (component is ICollisionStay) //Checks if any components are IAnimateable
                {
                    //If a component is IAnimateable we call the local implementation of the method
                    (component as ICollisionStay).OnCollisionStay(other);
                }
            }
        }

        public void OnCollisionEnter(Collider other)
        {
            foreach (Component component in components)
            {
                if (component is ICollisionEnter) //Checks if any components are IAnimateable
                {
                    //If a component is IAnimateable we call the local implementation of the method
                    (component as ICollisionEnter).OnCollisionEnter(other);
                }
            }
        }
        public void OnCollisionExit(Collider other)
        {
            foreach (Component component in components)
            {
                if (component is ICollisionExit) //Checks if any components are IAnimateable
                {
                    //If a component is IAnimateable we call the local implementation of the method
                    (component as ICollisionExit).OnCollisionExit(other);
                }
            }
        }
    }

    interface ICollisionEnter

    {
        void OnCollisionEnter(Collider other);
    }
    interface ICollisionExit

    {
        void OnCollisionExit(Collider other);
    }

}
