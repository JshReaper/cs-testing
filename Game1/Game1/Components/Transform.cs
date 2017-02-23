using Microsoft.Xna.Framework;

namespace Game1
{
    public class Transform:Component
    {
        private Vector2 posistion;
        public Vector2 Posistion { get { return posistion; } set { posistion = value; } }
        public float Scale { get; set; }
        public Vector2 Origin { get; set; }
        public float Rotation { get; set; }

        public Transform(GameObject gameObject, Vector2 posistion) : base(gameObject)
        {
            this.posistion = posistion;
            Scale = 1;
            Origin = Vector2.Zero;
            Rotation = 0;
        }

        public void Translate(Vector2 translation)
        {
            posistion += translation;
        }

    }
}