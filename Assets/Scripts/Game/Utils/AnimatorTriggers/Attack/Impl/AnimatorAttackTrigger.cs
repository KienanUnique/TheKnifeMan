using System;
using UniRx;
using UnityEngine;

namespace Game.Utils.AnimatorTriggers.Attack.Impl
{
    public class AnimatorAttackTrigger : MonoBehaviour, IAnimatorAttackTrigger
    {
        private readonly ReactiveCommand _onAttackFrame = new();
        
        public IObservable<Unit> AttackFramePlayed => _onAttackFrame;

        public void InvokeAttackTrigger()
        {
            _onAttackFrame.Execute();
        }
    }
}