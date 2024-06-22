using System.Collections.Generic;
using Game.Projectile.TypeData;
using Game.Projectile.TypeData.Impl;
using UnityEngine;

namespace Game.Projectile.Pattern
{
    public abstract class AProjectilesPattern : ScriptableObject, IProjectilesPattern
    {
        [SerializeField] private ProjectileTypeData projectileType;
        
        public IProjectileType ProjectileType => projectileType;
        public abstract IReadOnlyList<Vector2> Directions { get; }
    }
}