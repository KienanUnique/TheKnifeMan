using Game.Utils.Directions;

namespace Game.Enemy.Parts.Visual
{
    public interface IEnemyVisualPartBase : IEnemyPoolPart
    {
        void PlayDeathAnimation();
        void ChangeLookDirection(EDirection1D direction1D);
    }
}