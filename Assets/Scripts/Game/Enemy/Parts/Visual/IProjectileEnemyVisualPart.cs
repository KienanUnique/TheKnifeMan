using Game.Utils.Directions;

namespace Game.Enemy.Parts.Visual
{
    public interface IProjectileEnemyVisualPart : IEnemyVisualPartBase
    {
        void PlayAttackAnimation(EDirection1D direction);
    }
}