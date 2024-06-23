using Game.Utils.Directions;

namespace Game.Enemy.Parts.Visual
{
    public interface IMeleeEnemyVisualPart : IEnemyVisualPartBase
    {
        void PlayAttackAnimation(EDirection2D direction2D);
    }
}