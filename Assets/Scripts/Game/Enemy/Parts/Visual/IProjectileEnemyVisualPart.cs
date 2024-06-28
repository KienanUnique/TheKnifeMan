using UnityEngine;

namespace Game.Enemy.Parts.Visual
{
    public interface IProjectileEnemyVisualPart : IEnemyVisualPartBase
    {
        void PlayAttackAnimation(Vector2 direction);
        void RotateHandsTowardsAttackDirection(Vector2 direction);
    }
}