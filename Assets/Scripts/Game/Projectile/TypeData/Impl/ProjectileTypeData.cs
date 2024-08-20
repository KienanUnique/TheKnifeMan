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
        [SerializeField] private int _startPoolCount = 17;

        public int InstanceID => GetInstanceID();
        public ProjectileController Prefab => prefab;
        public ProjectileData ProjectileData => projectileData;
        public int StartPoolCount => _startPoolCount;

        public bool Equals(IProjectileType other)
        {
            return other != null && InstanceID.Equals(other.InstanceID);
        }
    }
}