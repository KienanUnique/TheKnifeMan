using System;
using UnityEngine;
using Zenject;

namespace Game.Enemy
{
    public interface IPoolEnemy : IInitializable
    {
        IObservable<IPoolEnemy> OnDead { get; }

        void HandleEnable(Vector3 position);
        void HandleDisableAndReset();
        void HandleGameEnd();
    }
}