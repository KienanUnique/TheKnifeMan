using System;
using Alchemy.Inspector;
using Game.Object;
using Game.Utils.AnimatorTriggers.Attack;
using Game.Utils.AnimatorTriggers.Attack.Impl;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Character
{
    [Serializable]
    public abstract class ACharacterData : AObjectData, ICharacterData
    {
        [SerializeField] protected Transform rootTransform;
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer mainSprite;
        [SerializeField] private AnimatorAttackTrigger animatorAttackTrigger;

        public Transform RootTransform => rootTransform;
        public Animator Animator => animator;
        public SpriteRenderer MainSprite => mainSprite;
        public IAnimatorAttackTrigger AttackTrigger => animatorAttackTrigger;

        public bool IsRootTransformFilled => rootTransform != null;

        [HelpBox("No automatic saving! Make manual change and then save prefab!", HelpBoxMessageType.Warning)]
        [ShowIf(nameof(IsRootTransformFilled))]
        public virtual void AutoFill()
        {
            animator = rootTransform.GetComponentInChildren<Animator>();
            mainSprite = rootTransform.GetComponentInChildren<SpriteRenderer>();
            animatorAttackTrigger = rootTransform.GetComponentInChildren<AnimatorAttackTrigger>();
        }
    }
}