using Game.Projectile.Controller.Impl;
using Game.Projectile.Data;

namespace Game.Projectile.TypeData
{
    public interface IProjectileTypeData : IProjectileType
    {
        ProjectileController Prefab { get; }
        ProjectileData ProjectileData { get; }
    }
}