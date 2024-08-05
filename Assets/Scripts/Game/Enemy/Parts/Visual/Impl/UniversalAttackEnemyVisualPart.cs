using Db.EnemiesParameters.Parameters;
using Game.Utils;
using Game.Utils.Directions;

namespace Game.Enemy.Parts.Visual.Impl
{
    public class UniversalAttackEnemyVisualPart : AEnemyVisualPart, IUniversalEnemyVisualPart
    {
        protected UniversalAttackEnemyVisualPart(IEnemyParametersBase parameters) : base(parameters)
        {
        }

        public void PlayAttackAnimation(EDirection1D direction)
        {
            Animator.SetInteger(AnimationKeys.AttackDirection, (int) direction);
            Animator.SetTrigger(AnimationKeys.ShootAttackTrigger);
        }

        public void PlayAttackAnimation(EDirection2D direction2D)
        {
            Animator.SetInteger(AnimationKeys.AttackDirection, (int) direction2D);
            Animator.SetTrigger(AnimationKeys.MeleeAttackTrigger);
        }
    }
}