﻿using UnityEngine;

namespace Game.Utils
{
    public static class AnimationKeys
    {
        public static readonly int IsMoving = Animator.StringToHash("IsMoving");
        public static readonly int IsInjured = Animator.StringToHash("IsInjured");
        public static readonly int Dead = Animator.StringToHash("Dead");
        public static readonly int AttackDirection = Animator.StringToHash("AttackDirection");
        public static readonly int AttackTrigger = Animator.StringToHash("Attack");
        public static readonly int MeleeAttackTrigger = Animator.StringToHash("MeleeAttack");
        public static readonly int ShootAttackTrigger = Animator.StringToHash("ShootAttack");
        public static readonly int IsDashing = Animator.StringToHash("IsDashing");
        public static readonly int PlaySpawnAnimation = Animator.StringToHash("PlaySpawnAnimation");
    }
}