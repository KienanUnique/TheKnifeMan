using System.Collections.Generic;
using Game.Projectile.TypeData;
using UnityEngine;

namespace Game.Projectile.Pattern
{
    public interface IProjectilesPattern
    {
        IProjectileType ProjectileType { get; }
        IReadOnlyList<Vector2> Directions { get; }
    }   
}