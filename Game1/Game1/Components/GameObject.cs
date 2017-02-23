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
        /// <summary>
        /// gets the transform attached to the game object
        /// </summary>
        public Transform Transform { get { return transform; } }
       
        protected Vector2 scale;
        protected GraphicsDevice gd;
        private List<Component> components;
        private bool isLoaded = false;
        /// <summary>
        /// sets the compenent list transform and graphics device
        /// </summary>
        /// <param name="posistion"></param>
        /// <param name="gd"></param>
        public GameObject(Vector2 posistion, GraphicsDevice gd)
        {
            components = new List<Component>();
            this.transform = new Transform(this, posistion);
            this.gd = gd;
        }
        /// <summary>
        /// adds the speficic component to the components list
        /// </summary>
        /// <param name="component"></param>
        public void AddComponent(Component component)
        {
            components.Add(component);
        }
        /// <summary>
        /// removes the speficic component to the components list
        /// </summary>
        /// <param name="component"></param>
        public void RemoveComponent(string component)
        {
            Component c = GetComponent(component);
            if (c != null)
            {
                components.Remove(c);
            }
        }
        /// <summary>
        /// gets the speficic component to the components list
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
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
        /// <summary>
        /// loads all the components which is loadable
        /// </summary>
        /// <param name="content"></param>
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
        /// <summary>
        /// Updates all the components which is Updateable
        /// </summary>
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
        /// <summary>
        /// Draws all the components which is Drawable
        /// </summary>
        /// <param name="spriteBatch"></param>
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

        /// <summary>
        /// Animates all the components which is Animateable
        /// </summary>
        /// <param name="animationName"></param>
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
        /// <summary>
        /// while a collision happens do something on all components that is CollisionStay
        /// </summary>
        /// <param name="other"></param>
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
        /// <summary>
        /// when a collision happens do something on all components that is CollisionEnter
        /// </summary>
        /// <param name="other"></param>
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
        /// <summary>
        /// when a collision ends do something on all components that is CollisionExit
        /// </summary>
        /// <param name="other"></param>
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
}
