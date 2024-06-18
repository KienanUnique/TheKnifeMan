using System;
using Game.Object;
using Game.Utils.AnimatorTriggers.Attack;
using Game.Utils.AnimatorTriggers.Attack.Impl;
using UnityEngine;

namespace Game.Character
{
    [Serializable]
    public abstract class ACharacterData : AObjectData
    {
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer mainSprite;
        [SerializeField] private AnimatorAttackTrigger animatorAttackTrigger;
        
        public Animator Animator => animator;
        public SpriteRenderer MainSprite => mainSprite;
        public IAnimatorAttackTrigger AttackTrigger => animatorAttackTrigger;
    }
}