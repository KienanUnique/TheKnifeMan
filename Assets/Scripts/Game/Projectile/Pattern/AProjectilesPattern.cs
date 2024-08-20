using System.Collections.Generic;
using Game.Projectile.TypeData;
using Game.Projectile.TypeData.Impl;
using UnityEngine;

namespace Game.Projectile.Pattern
{
    public abstract class AProjectilesPattern : ScriptableObject, IProjectilesPattern
    {
        public abstract IReadOnlyList<Vector2> Directions { get; }
    }
}