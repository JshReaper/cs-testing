using Microsoft.Xna.Framework;

namespace Game1
{
    public class Transform:Component
    {
        private Vector2 posistion;
        /// <summary>
        /// gets or sets the current posistion of the gameobject attached to the transform
        /// </summary>
        public Vector2 Posistion { get { return posistion; } set { posistion = value; } }
        /// <summary>
        /// gets or sets the current scale of the gameobject attached to the transform
        /// </summary>
        public float Scale { get; set; }
        /// <summary>
        /// gets or sets the current origin of the gameobject attached to the transform
        /// </summary>
        public Vector2 Origin { get; set; }
        /// <summary>
        /// gets or sets the current Rotation of the gameobject attached to the transform
        /// </summary>
        public float Rotation { get; set; }
        /// <summary>
        /// set the posistion scale origin and rotation
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="posistion"></param>
        public Transform(GameObject gameObject, Vector2 posistion) : base(gameObject)
        {
            this.posistion = posistion;
            Scale = 1;
            Origin = Vector2.Zero;
            Rotation = 0;
        }
        /// <summary>
        /// Alter the posistion based on the translation 
        /// </summary>
        /// <param name="translation"></param>
        public void Translate(Vector2 translation)
        {
            posistion += translation;
        }

    }
}