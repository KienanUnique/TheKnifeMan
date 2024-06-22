using System.Collections.Generic;
using Game.Projectile.TypeData;
using Game.Projectile.TypeData.Impl;
using UnityEngine;
using Utils;

namespace Db.Projectiles.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Parameters + nameof(ProjectilesParameters),
        fileName = nameof(ProjectilesParameters))]
    public class ProjectilesParameters : ScriptableObject, IProjectilesParameters
    {
        [SerializeField] private List<ProjectileTypeData> projectilesTypes;

        public IReadOnlyList<IProjectileTypeData> ProjectilesTypes => projectilesTypes;

        private void OnValidate()
        {
            var uniqueProjectilesTypes = new List<ProjectileTypeData>();
            
            foreach (var projectileType in projectilesTypes)
            {
                if (uniqueProjectilesTypes.Contains(projectileType)) 
                    continue;
                
                uniqueProjectilesTypes.Add(projectileType);
            }
            
            projectilesTypes.Clear();
            projectilesTypes = uniqueProjectilesTypes;
        }
    }
}