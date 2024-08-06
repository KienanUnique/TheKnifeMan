using Db.EnemiesParameters.Parameters;
using Db.EnemiesParameters.Parameters.Impl;
using Game.Enemy.Controller.Impl;
using Game.Utils;
using UnityEngine;
using Utils;

namespace Db.EnemiesParameters.TypeData.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Enemies + nameof(UniversalEnemyTypeData), fileName = nameof(UniversalEnemyTypeData))]
    public class UniversalEnemyTypeData : AEnemyTypeData
    {
        [SerializeField] private UniversalEnemyParameters parameters;
        [SerializeField] private UniversalEnemyController prefab;

        public override Object Prefab => prefab;
        public override IEnemyParametersBase Parameters => parameters;
        public override EEnemyType Type => EEnemyType.Universal;
    }
}