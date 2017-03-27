using System.Threading;

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
                foreach (var o in GameWorld.Instance.GameObjects)
                {
                    Enemy en = o.GetComponent("Enemy") as Enemy;
                    en?.UpdateMoveMent();
                }
            }
        }
    }
}