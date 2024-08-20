using Game.Utils.AnimatorTriggers.Attack;
using UnityEngine;

namespace Game.Character
{
    public interface ICharacterData
    {
        Transform RootTransform { get; }
        Animator Animator { get; }
        SpriteRenderer MainSprite { get; }
        IAnimatorAttackTrigger AttackTrigger { get; }
        bool IsRootTransformFilled { get; }
    }
}