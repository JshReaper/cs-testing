using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;

namespace Game1
{
    public class AI
    {
        private static AI instance = null;
        private Thread enMove;
        public static AI Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AI();
                }
                return instance;
            }
        }

        private AI()
        {
        }

        public void Start()
        {
            enMove = new Thread(EnemyMoveMent) { IsBackground = true };
            
            enMove.Start();
        }
        private void EnemyMoveMent()
        {
            while (GameWorld.Instance.Running)
            {
                
                for (int i = 0; i < GameWorld.Instance.GameObjects.Count; i++)
                {
                    var o = GameWorld.Instance.GameObjects[i];
                    Enemy en = o.GetComponent("Enemy") as Enemy;
                    en?.UpdateMoveMent(GameWorld.Instance.upGameTime);
                    
                }
               
            }
        }
    }

    
}