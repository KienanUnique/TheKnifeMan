using System.Collections.Generic;
using Db.Projectiles;
using Game.Projectile.Factory.Concrete;
using Game.Projectile.Pattern;
using Game.Projectile.TypeData;
using UnityEngine;
using Zenject;

namespace Game.Projectile.Factory.Impl
{
    public class ProjectilesFactory : IProjectilesFactory, IInitializable
    {
        private const string RootName = "Projectiles";

        private readonly DiContainer _diContainer;
        private readonly IProjectilesParameters _projectilesParameters;

        private readonly Dictionary<IProjectileType, ConcreteProjectileFactory> _factories = new();

        public ProjectilesFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void Initialize()
        {
            var projectilesTypes = _projectilesParameters.ProjectilesTypes;
            foreach (var projectileTypeData in projectilesTypes)
            {
                var factory = _diContainer.Instantiate<ConcreteProjectileFactory>(new[] {projectileTypeData});
                factory.Initialize();
                _factories.Add(projectileTypeData, factory);
            }
        }

        public void Create(IProjectilesPattern pattern, Vector3 position, Quaternion rotation)
        {
            var factory = _factories[pattern.ProjectileType];

            foreach (var patternDirection in pattern.Directions)
            {
                var needDirection = rotation * patternDirection;
                factory.Create(position, needDirection);
            }
        }
    }
}