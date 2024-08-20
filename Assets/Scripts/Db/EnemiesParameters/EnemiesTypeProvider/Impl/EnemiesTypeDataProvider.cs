using System.Collections.Generic;
using System.Linq;
using Alchemy.Inspector;
using Db.EnemiesParameters.TypeData;
using UnityEditor;
using UnityEngine;
using Utils;

namespace Db.EnemiesParameters.EnemiesTypeProvider.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Parameters + nameof(EnemiesTypeDataProvider),
        fileName = nameof(EnemiesTypeDataProvider))]
    public class EnemiesTypeDataProvider : ScriptableObject, IEnemiesTypeDataProvider
    {
        [SerializeField] private List<AEnemyTypeData> allEnemiesTypes;

        public IReadOnlyList<IEnemyTypeData> AllEnemiesTypes => allEnemiesTypes;

#if UNITY_EDITOR
        [Button]
        public void AutoFill()
        {
            allEnemiesTypes.Clear();
            var enemiesTypesAsObjects = LoadAssetsOfType<AEnemyTypeData>();
            allEnemiesTypes.AddRange(enemiesTypesAsObjects);
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