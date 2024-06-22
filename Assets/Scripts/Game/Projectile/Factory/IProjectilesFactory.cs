using Game.Projectile.Pattern;
using UnityEngine;

namespace Game.Projectile.Factory
{
    public interface IProjectilesFactory
    {
        void Create(IProjectilesPattern pattern, Vector3 position, Quaternion rotation);
    }
}