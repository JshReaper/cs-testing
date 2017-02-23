using Microsoft.Xna.Framework;

namespace Game1
{
    class GameObjectDirector
    {
        private IGameObjectBuilder builder;
        /// <summary>
        /// sets the builder.
        /// </summary>
        /// <param name="builder"></param>
        public GameObjectDirector(IGameObjectBuilder builder)
        {
            this.builder = builder;
        }
        /// <summary>
        /// returns the builders gameobject
        /// </summary>
        /// <returns></returns>
        public GameObject Construct()
        {
            
            return builder.GetResult();
        }
    }
}