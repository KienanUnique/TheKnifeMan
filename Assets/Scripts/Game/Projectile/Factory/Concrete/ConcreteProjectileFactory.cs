﻿using System;
using System.Collections.Generic;
using Game.Projectile.Controller;
using Game.Projectile.Controller.Impl;
using Game.Projectile.TypeData.Impl;
using ModestTree;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Projectile.Factory.Concrete
{
    public class ConcreteProjectileFactory : IInitializable, IDisposable
    {
        private readonly DiContainer _diContainer;
        private readonly Transform _rootTransform;
        private readonly ProjectileTypeData _projectileTypeData;

        private readonly Queue<IPoolProjectile> _availableProjectiles = new();
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly List<IPoolProjectile> _busyProjectiles = new();

        public ConcreteProjectileFactory(
            DiContainer diContainer, 
            Transform rootTransform, 
            ProjectileTypeData projectileTypeData
        )
        {
            _diContainer = diContainer;
            _rootTransform = rootTransform;
            _projectileTypeData = projectileTypeData;
        }

        public void Initialize()
        {
            for (int i = 0; i < _projectileTypeData.StartPoolCount; i++)
            {
                _availableProjectiles.Enqueue(Instantiate(Vector3.zero));
            }
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

        public void Create(Vector3 position, Vector2 direction, IProjectilesSender sender)
        {
            var projectile = _availableProjectiles.IsEmpty() ? Instantiate(position) : _availableProjectiles.Dequeue();

            projectile.Appear(direction, position, sender);
            _busyProjectiles.Add(projectile);
        }

        public void ForceDisable()
        {
            foreach (var busyProjectile in _busyProjectiles)
            {
                busyProjectile.DisableAndReset();
            }
        }

        private IPoolProjectile Instantiate(Vector3 position)
        {
            var newProjectile = _diContainer.InstantiatePrefabForComponent<ProjectileController>(_projectileTypeData.Prefab,
                position, Quaternion.identity, _rootTransform, new[] {_projectileTypeData.ProjectileData});
            
            newProjectile.Disappeared.Subscribe(OnProjectileDisappear).AddTo(_compositeDisposable);
            newProjectile.DisableAndReset();

            return newProjectile;
        }

        private void OnProjectileDisappear(IPoolProjectile poolProjectile)
        {
            poolProjectile.DisableAndReset();
            _availableProjectiles.Enqueue(poolProjectile);
            _busyProjectiles.Remove(poolProjectile);
        }
    }
}