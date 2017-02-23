using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public  abstract class Component
    {
        private GameObject gameObject;

        public GameObject GameObject { get { return gameObject; } }

        protected Component(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        protected Component()
        {
            
        }
    }
}
