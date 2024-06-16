using Game.Object.Part;

namespace Game.Enemy.Parts.Visual
{
    public interface IEnemyVisualPartBase : IEnemyPoolPart, IObjectPart
    {
        void PlayDeathAnimation();
    }
}