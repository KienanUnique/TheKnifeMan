﻿using Db.EnemiesParameters.Parameters;
using Game.Utils;
using Game.Utils.Directions;

namespace Game.Enemy.Parts.Visual.Impl
{
    public class MeleeAttackEnemyVisualPart : AEnemyVisualPart, IMeleeEnemyVisualPart
    {
        protected MeleeAttackEnemyVisualPart(IEnemyParametersBase parameters) : base(parameters)
        {
        }

        public void PlayAttackAnimation(EDirection2D direction2D)
        {
            Animator.SetInteger(AnimationKeys.AttackDirection, (int) direction2D);
            Animator.SetTrigger(AnimationKeys.MeleeAttackTrigger);
        }
        
        public override void PlayDeathAnimation()
        {
            Animator.ResetTrigger(AnimationKeys.MeleeAttackTrigger);
            base.PlayDeathAnimation();
        }
    }
}