using System;
using System.Collections.Generic;
using Db.EnemiesParametersProvider;
using Db.EnemyFactory;
using ModestTree;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Enemy.Factory.Concrete
{
    public abstract class AConcreteEnemyFactory : IConcreteEnemyFactory
    {
        protected readonly IEnemiesParametersProvider ParametersProvider;

        private readonly DiContainer _diContainer;
        private readonly Transform _rootTransform;
        private readonly IEnemyFactoryParameters _parameters;
        
        private readonly Queue<IPoolEnemy> _availableEnemies = new();
        private readonly List<IPoolEnemy> _busyEnemies = new();
        private readonly CompositeDisposable _compositeDisposable = new();

        protected abstract string PrefabNameInResources { get; }

        protected AConcreteEnemyFactory(
            DiContainer diContainer,
            Transform rootTransform,
            IEnemyFactoryParameters parameters,
            IEnemiesParametersProvider parametersProvider
        )
        {
            _diContainer = diContainer.CreateSubContainer();
            _rootTransform = rootTransform;
            _parameters = parameters;
            ParametersProvider = parametersProvider;
        }

        public void Initialize()
        {
            InstallBindings(_diContainer);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

        public virtual void Create(Vector3 position)
        {
            var enemy = _availableEnemies.IsEmpty() ? Instantiate(position) : _availableEnemies.Dequeue();

            enemy.HandleEnable();
            _busyEnemies.Add(enemy);
        }

        protected abstract void InstallBindings(DiContainer container);

        protected virtual IPoolEnemy Instantiate(Vector3 position)
        {
            var poolEnemy = _diContainer.InstantiatePrefabResourceForComponent<IPoolEnemy>(PrefabNameInResources,
                position, Quaternion.identity, _rootTransform);

            poolEnemy.Initialize();

            poolEnemy.OnDead.Subscribe(OnEnemyDead).AddTo(_compositeDisposable);

            return poolEnemy;
        }

        private void OnEnemyDead(IPoolEnemy poolEnemy)
        {
            _busyEnemies.Remove(poolEnemy);

            var afterDeathDelay = _parameters.AfterDeathDelaySeconds;
            Observable.Timer(TimeSpan.FromSeconds(afterDeathDelay)).Subscribe(_ => ReturnEnemyToPool(poolEnemy))
                .AddTo(_compositeDisposable);
        }

        private void ReturnEnemyToPool(IPoolEnemy poolEnemy)
        {
            poolEnemy.HandleDisableAndReset();
            _availableEnemies.Enqueue(poolEnemy);
        }
    }
}