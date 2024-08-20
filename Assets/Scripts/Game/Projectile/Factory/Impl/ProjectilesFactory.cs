using System.Collections.Generic;
using Db.Projectiles;
using Game.Projectile.Factory.Concrete;
using Game.Projectile.Pattern;
using Game.Projectile.TypeData;
using Game.Utils;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Projectile.Factory.Impl
{
    public class ProjectilesFactory : IProjectilesFactory, INeedWaitInitializable, IGameStateListener
    {
        private const string RootName = "Projectiles";

        private readonly DiContainer _diContainer;
        private readonly IProjectilesParameters _projectilesParameters;

        private readonly Dictionary<IProjectileType, ConcreteProjectileFactory> _factories = new();
        private readonly ReactiveProperty<bool> _isInitilized = new();

        public IReactiveProperty<bool> IsInitilized => _isInitilized;

        public ProjectilesFactory(
            DiContainer diContainer,
            IProjectilesParameters projectilesParameters
        )
        {
            _diContainer = diContainer;
            _projectilesParameters = projectilesParameters;
        }

        public void Initialize()
        {
            var rootTransform = _diContainer.CreateEmptyGameObject(RootName).transform;

            var projectilesTypes = _projectilesParameters.ProjectilesTypes;

            foreach (var projectileTypeData in projectilesTypes)
            {
                var factory =
                    _diContainer.Instantiate<ConcreteProjectileFactory>(
                        new object[] {rootTransform, projectileTypeData});
                factory.Initialize();
                _factories.Add(projectileTypeData, factory);
            }

            _isInitilized.Value = true;
        }

        public void Create(IProjectilesPattern pattern, IProjectileType projectileType, IProjectilesSender sender,
            Vector3 position, Quaternion rotation)
        {
            var factory = _factories[projectileType];

            foreach (var patternDirection in pattern.Directions)
            {
                var needDirection = rotation * patternDirection;
                factory.Create(position, needDirection, sender);
            }
        }

        public void OnGameEnd(bool isPlayerWin)
        {
            foreach (var (_, factory) in _factories)
            {
                factory.ForceDisable();
            }
        }
    }
}