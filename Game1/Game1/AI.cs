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
        private static void EnemyMoveMent()
        {
            while (GameWorld.Instance.Running)
            {
                if (GameWorld.Instance.towerPool.Objects.Count != Enemy.savedTowerA)
                {
                   Enemy. mapChanged = true;
                    Enemy.savedTowerA = GameWorld.Instance.towerPool.Objects.Count;
                    Thread.Sleep(500);
                    Enemy.mapChanged = false;
                }
                for (int i = 0; i < GameWorld.Instance.GameObjects.Count; i++)
                {
                    var o = GameWorld.Instance.GameObjects[i];
                    Enemy en = o.GetComponent("Enemy") as Enemy;
                    en?.UpdateMoveMent(GameWorld.Instance.upGameTime);
                    
                }
               Thread.Sleep(10);
            }
        }
    }

    
}