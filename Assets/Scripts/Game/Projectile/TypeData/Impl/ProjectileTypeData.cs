using Game.Projectile.Controller.Impl;
using Game.Projectile.Data;
using UnityEngine;
using Utils;

namespace Game.Projectile.TypeData.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Projectiles + nameof(ProjectileTypeData), fileName = nameof(ProjectileTypeData))]
    public class ProjectileTypeData : ScriptableObject, IProjectileTypeData
    {
        [SerializeField] private ProjectileController prefab;
        [SerializeField] private ProjectileData projectileData;

        public int InstanceID => GetInstanceID();
        public ProjectileController Prefab => prefab;
        public ProjectileData ProjectileData => projectileData;

        public bool Equals(IProjectileType other)
        {
            return other != null && InstanceID.Equals(other.InstanceID);
        }
    }
}