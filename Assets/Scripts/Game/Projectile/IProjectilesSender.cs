using System;

namespace Game.Projectile
{
    public interface IProjectilesSender : IEquatable<IProjectilesSender>
    {
        int InstanceId { get; }
    }
}