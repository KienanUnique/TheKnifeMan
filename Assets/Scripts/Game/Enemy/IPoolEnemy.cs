using System;
using Zenject;

namespace Game.Enemy
{
    public interface IPoolEnemy : IInitializable
    {
        IObservable<IPoolEnemy> OnDead { get; }

        void HandleEnable();
        void HandleDisableAndReset();
        void HandleGameEnd();
    }
}