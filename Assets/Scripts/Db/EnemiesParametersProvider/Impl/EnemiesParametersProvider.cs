using Db.EnemiesParametersProvider.Parameters.Impl;
using UnityEngine;
using Utils;

namespace Db.EnemiesParametersProvider.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Parameters + nameof(EnemiesParametersProvider),
        fileName = nameof(EnemiesParametersProvider))]
    public class EnemiesParametersProvider : ScriptableObject, IEnemiesParametersProvider
    {
        [SerializeField] private SimpleEnemyParameters simpleEnemyParameters;

        public ISimpleEnemyParameters SimpleEnemyParameters => simpleEnemyParameters;
    }
}