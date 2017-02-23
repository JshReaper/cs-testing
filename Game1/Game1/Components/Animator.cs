using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Game1
{
    class Animator:Component,IUpdateAble

    {
        private SpriteRenderer spriteRenderer;
        private int currentIndex;
        private float timeElapsed;
        private float fps;
        private Rectangle[] rectangles;
        private string animationName;
        /// <summary>
        /// gets the animation name
        /// </summary>
        public string AnimationName { get { return animationName; } set { animationName = value; } }
        private Dictionary<string, Animation> animations;
        /// <summary>
        /// sets the animations to an empty Dictinorary and refers to the spriterenderer of the gameobject
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="fps"></param>
        public Animator( GameObject gameObject,float fps): base(gameObject)
        {

            animations = new Dictionary<string, Animation>();
            this.fps = fps;
            this.spriteRenderer = (SpriteRenderer)gameObject.GetComponent("SpriteRenderer");
            
        }
        /// <summary>
        /// updates the animations
        /// </summary>
        public void Update()
        {
            timeElapsed += GameWorld.Instance.deltaTime;
            currentIndex = (int) (timeElapsed * fps);
             if(animationName != null) { 
            if (currentIndex > rectangles.Length - 1)
            {
                GameObject.OnAnimationDone(animationName);
                timeElapsed = 0;
                currentIndex = 0;
            }
            spriteRenderer.Rectangle = rectangles[currentIndex];
            }

        }
        /// <summary>
        /// creates a new animation
        /// </summary>
        /// <param name="name"></param>
        /// <param name="animation"></param>
        public void CreateAnimation(string name, Animation animation)
        {
            animations.Add(name, animation);
        }
        /// <summary>
        /// plays a animation
        /// </summary>
        /// <param name="animationName"></param>
        public void PlayAnimation(string animationName)
        {
            if (this.animationName != animationName)
            {
                this.AnimationName = animationName;
                //Sets the rectangles
                this.rectangles = animations[animationName].Rectangles;
                //Resets the rectangle
                this.spriteRenderer.Rectangle = rectangles[0];
                //Sets the offset
                this.spriteRenderer.Offset = animations[animationName].Offset;
                //Sets the animation name
                this.animationName = animationName;
                //Sets the fps
                this.fps = animations[animationName].AnimationSpeed;
                //Resets the animation
                timeElapsed = 0;
                currentIndex = 0;
            }

        }
    }
}