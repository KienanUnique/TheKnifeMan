using Game.Object.Part;

namespace Game.Enemy.Parts
{
    public interface IEnemyPoolPart : IObjectPart
    {
        void Enable();
        void DisableAndReset();
    }
}