using System;
using System.Collections.Generic;
using Db.EnemiesParameters.TypeData;
using Db.EnemyFactory;
using Game.Enemy.PartsFactory.Impl;
using Game.Object.PartsFactory;
using Game.Utils;
using ModestTree;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Enemy.Factory.Concrete
{
    public class ConcreteEnemyFactory : IConcreteEnemyFactory
    {
        private readonly DiContainer _rootDiContainer;
        private readonly Transform _rootTransform;
        private readonly IEnemyTypeData _typeData;
        private readonly IEnemyFactoryParameters _parameters;
        
        private readonly Queue<IPoolEnemy> _availableEnemies = new();
        private readonly List<IPoolEnemy> _busyEnemies = new();
        private readonly CompositeDisposable _compositeDisposable = new();
        
        private DiContainer _diContainer;

        protected ConcreteEnemyFactory(
            DiContainer diContainer,
            Transform rootTransform,
            IEnemyFactoryParameters parameters,
            IEnemyTypeData typeData
        )
        {
            _rootDiContainer = diContainer;
            _rootTransform = rootTransform;
            _typeData = typeData;
            _parameters = parameters;
        }
        
        public void Initialize()
        {
            _diContainer = _rootDiContainer.CreateSubContainer();
            var concreteParametersType = _typeData.Parameters.GetType();
            _diContainer.BindInterfacesAndSelfTo(concreteParametersType).FromInstance(_typeData.Parameters).AsSingle();
            
            var partsFactory = GetPartsFactoryByType(_typeData.Type);
            _diContainer.Bind<IPartsFactory>().FromInstance(partsFactory).AsSingle();

            for (int i = 0; i < _typeData.StartPoolCount; i++)
            {
                _availableEnemies.Enqueue(Instantiate(Vector3.zero));
            }
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

        public void HandleGameEnd()
        {
            foreach (var busyEnemy in _busyEnemies)
            {
                busyEnemy.HandleGameEnd();
            }
        }

        private IPoolEnemy Instantiate(Vector3 position)
        {
            var poolEnemyGameObject = _diContainer.InstantiatePrefab(_typeData.Prefab, position, Quaternion.identity, _rootTransform);
            var poolEnemy = poolEnemyGameObject.GetComponent<IPoolEnemy>();
            
            poolEnemy.Initialize();
            poolEnemy.HandleDisableAndReset();
            
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

        private IPartsFactory GetPartsFactoryByType(EEnemyType enemyType)
        {
            return enemyType switch
            {
                EEnemyType.Melee => _diContainer.Instantiate<MeleeEnemyPartsFactory>(),
                EEnemyType.LongRange => _diContainer.Instantiate<LongRangeEnemyPartsFactory>(),
                _ => throw new ArgumentOutOfRangeException(nameof(enemyType), enemyType, null)
            };
        }
    }
}