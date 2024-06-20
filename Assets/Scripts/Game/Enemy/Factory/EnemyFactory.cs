using System;
using System.Collections.Generic;
using Game.Enemy.Factory.Concrete;
using Game.Enemy.Factory.Concrete.Impl;
using Game.Utils;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Enemy.Factory
{
    public class EnemyFactory : IInitializable, IDisposable, IEnemyFactory
    {
        private const string EnemiesRootName = "Enemies";

        private readonly Dictionary<EEnemyType, IConcreteEnemyFactory> _factories = new();
        private readonly CompositeDisposable _compositeDisposable = new();

        private readonly DiContainer _diContainer;

        public EnemyFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void Initialize()
        {
            var rootTransform = _diContainer.CreateEmptyGameObject(EnemiesRootName).transform;

            var factoryExtraArgs = new[] {rootTransform};
            _factories.Add(EEnemyType.Simple, _diContainer.Instantiate<SimpleEnemyFactory>(factoryExtraArgs));

            foreach (var (_, concreteEnemyFactory) in _factories)
            {
                _compositeDisposable.Add(concreteEnemyFactory);
                concreteEnemyFactory.Initialize();
            }
        }

        public void Create(EEnemyType enemyType, Vector3 position)
        {
            _factories[enemyType].Create(position);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}