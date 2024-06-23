using System;
using System.Collections.Generic;
using Db.EnemiesParameters.EnemiesTypeProvider;
using Db.EnemiesParameters.TypeData;
using Game.Enemy.Factory.Concrete;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Enemy.Factory.Impl
{
    public class EnemyFactory : IInitializable, IDisposable, IEnemyFactory
    {
        private const string EnemiesRootName = "Enemies";

        private readonly Dictionary<IEnemyType, IConcreteEnemyFactory> _factories = new();
        private readonly CompositeDisposable _compositeDisposable = new();

        private readonly DiContainer _diContainer;
        private readonly IEnemiesTypeDataProvider _enemiesTypeDataProvider;

        public EnemyFactory(
            DiContainer diContainer,
            IEnemiesTypeDataProvider enemiesTypeDataProvider
        )
        {
            _diContainer = diContainer;
            _enemiesTypeDataProvider = enemiesTypeDataProvider;
        }

        public void Initialize()
        {
            var rootTransform = _diContainer.CreateEmptyGameObject(EnemiesRootName).transform;

            var allEnemiesTypes = _enemiesTypeDataProvider.AllEnemiesTypes;
            
            foreach (var enemyType in allEnemiesTypes)
            {
                var factoryExtraArgs = new object[] {rootTransform, enemyType};
                _factories.Add(enemyType, _diContainer.Instantiate<ConcreteEnemyFactory>(factoryExtraArgs));
            }

            foreach (var (_, concreteEnemyFactory) in _factories)
            {
                _compositeDisposable.Add(concreteEnemyFactory);
                concreteEnemyFactory.Initialize();
            }
        }

        public void Create(IEnemyType typeData, Vector3 position)
        {
            _factories[typeData].Create(position);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}