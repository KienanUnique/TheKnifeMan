using System.Collections.Generic;
using System.Linq;
using Alchemy.Inspector;
using Db.EnemiesParameters.TypeData;
using Game.Projectile.TypeData;
using Game.Projectile.TypeData.Impl;
using UnityEditor;
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

#if UNITY_EDITOR
        [Button]
        public void AutoFill()
        {
            projectilesTypes.Clear();
            var enemiesTypesAsObjects = LoadAssetsOfType<ProjectileTypeData>();
            projectilesTypes.AddRange(enemiesTypesAsObjects);
        }

        private static T[] LoadAssetsOfType<T>() where T : Object
        {
            return AssetDatabase
                .FindAssets($"t:{typeof(T).Name}")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<T>)
                .ToArray();
        }
#endif
    }
}