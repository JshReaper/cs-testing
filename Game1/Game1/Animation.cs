using Microsoft.Xna.Framework;

namespace Game1
{
    class Animation
    {
        private float fps;
        private Vector2 offset;
        private Rectangle[] rectangles;
        public float Fps
        {
            get
            {
                return fps;
            }
        }

        public Vector2 Offset
        {
            get
            {
                return offset;
            }
        }

        public Rectangle[] Rectangles { get { return rectangles; } }

        public Animation(int frames,int yPos,int xStartFrame,int width,int height, float fps,Vector2 offset)
        {
            rectangles = new Rectangle[frames];
            this.offset = offset;
            this.fps = fps;
            for (int i = 0; i < frames; i++)
            {
                Rectangles[i] = new Rectangle((i + xStartFrame) * width, yPos, width, height);
            }

        }
    }
}