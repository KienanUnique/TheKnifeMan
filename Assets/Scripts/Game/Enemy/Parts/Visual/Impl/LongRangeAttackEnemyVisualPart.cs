using Db.EnemiesParameters.Parameters;
using Game.Utils;
using Game.Utils.Directions;

namespace Game.Enemy.Parts.Visual.Impl
{
    public class LongRangeAttackEnemyVisualPart : AEnemyVisualPart, IProjectileEnemyVisualPart
    {
        protected LongRangeAttackEnemyVisualPart(IEnemyParametersBase parameters) : base(parameters)
        {
        }

        public void PlayAttackAnimation(EDirection1D direction)
        {
            Animator.SetInteger(AnimationKeys.AttackDirection, (int) direction);
            Animator.SetTrigger(AnimationKeys.ShootAttackTrigger);
        }
    }
}