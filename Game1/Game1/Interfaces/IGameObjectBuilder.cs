using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    interface IGameObjectBuilder
    {
        GameObject GetResult();
        void BuildGameObject(Vector2 posistion, float layerDepth,float animationFps,float scale);
    }
}