namespace Game1
{
    public  abstract class Component
    {
        private GameObject gameObject;
        /// <summary>
        /// gets the gameobject which this component is attached to
        /// </summary>
        public GameObject GameObject { get { return gameObject; } }
        /// <summary>
        /// sets the gameobject which this component is attached to 
        /// </summary>
        /// <param name="gameObject"></param>
        protected Component(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
        /// <summary>
        /// empty constructor 
        /// </summary>
        protected Component()
        {
            
        }
    }
}
