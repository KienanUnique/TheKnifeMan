using Db.EnemiesParameters.Parameters;
using UnityEngine;

namespace Game.Enemy.Parts.Visual.Impl
{
    public class LongRangeAttackEnemyVisualPart : AEnemyVisualPart, IProjectileEnemyVisualPart
    {
        protected LongRangeAttackEnemyVisualPart(IEnemyParametersBase parameters) : base(parameters)
        {
        }

        public void PlayAttackAnimation(Vector2 direction)
        {
        }

        public void RotateHandsTowardsAttackDirection(Vector2 direction)
        {
        }
    }
}