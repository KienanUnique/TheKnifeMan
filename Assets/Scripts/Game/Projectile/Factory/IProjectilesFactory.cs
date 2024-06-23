using Game.Projectile.Pattern;
using Game.Projectile.TypeData;
using UnityEngine;

namespace Game.Projectile.Factory
{
    public interface IProjectilesFactory
    {
        void Create(IProjectilesPattern pattern, IProjectileType projectileType, IProjectilesSender sender,
            Vector3 position, Quaternion rotation);
    }
}