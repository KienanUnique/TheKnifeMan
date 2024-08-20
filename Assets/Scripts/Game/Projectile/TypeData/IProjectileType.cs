using System;

namespace Game.Projectile.TypeData
{
    public interface IProjectileType : IEquatable<IProjectileType>
    {
        int InstanceID { get; }
    }
}