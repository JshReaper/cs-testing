using Microsoft.Xna.Framework;

namespace Game1
{
    class Animation
    {
        private float animationSpeed;
        private Vector2 offset;
        private Rectangle[] rectangles;
        /// <summary>
        /// gets the current animationspeed
        /// </summary>
        public float AnimationSpeed
        {
            get
            {
                return animationSpeed;
            }
        }
        //get the current offset
        public Vector2 Offset
        {
            get
            {
                return offset;
            }
        }
        //get an array of all the recancles used in the animation
        public Rectangle[] Rectangles { get { return rectangles; } }
        //sets the recancles used in the animation equal to the frames of the animation and add all the animations
        public Animation(int frames,int yPos,int xStartFrame,int width,int height, float fps,Vector2 offset)
        {
            rectangles = new Rectangle[frames];
            this.offset = offset;
            this.animationSpeed = fps;
            for (int i = 0; i < frames; i++)
            {
                Rectangles[i] = new Rectangle((i + xStartFrame) * width, yPos, width, height);
            }

        }
    }
}