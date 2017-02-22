using Microsoft.Xna.Framework;

namespace Game1
{
    class GameObjectDirector
    {
        private IGameObjectBuilder builder;

        public GameObjectDirector(IGameObjectBuilder builder)
        {
            this.builder = builder;
        }

        public GameObject Construct()
        {
            
            return builder.GetResult();
        }
    }
}