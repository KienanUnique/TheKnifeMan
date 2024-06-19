using System;
using UniRx;

namespace Game.Utils.AnimatorTriggers.Attack
{
    public interface IAnimatorAttackTrigger
    {
        public IObservable<Unit> AttackFramePlayed { get; }
    }
}