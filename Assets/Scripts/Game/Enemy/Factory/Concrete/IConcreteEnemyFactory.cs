using System;
using UnityEngine;

namespace Game.Enemy.Factory.Concrete
{
    public interface IConcreteEnemyFactory : IDisposable
    {
        void Initialize();
        void Create(Vector3 position);
    }
}