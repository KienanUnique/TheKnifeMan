using System;
using UniRx;

namespace Game.Enemy
{
    public interface IPoolEnemy
    {
        IObservable<Unit> OnDead { get; }
        IObservable<Unit> OnReadyToUse { get; }
        
        void HandleEnable();
        void HandleDisableAndReset();
    }
}