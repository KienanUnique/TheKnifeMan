using Db.EnemiesParameters.Parameters;
using Db.EnemiesParameters.Parameters.Impl;
using Game.Enemy.Controller.Impl;
using Game.Utils;
using UnityEngine;
using Utils;

namespace Db.EnemiesParameters.TypeData.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Enemies + nameof(LongRangeEnemyTypeData), fileName = nameof(LongRangeEnemyTypeData))]
    public class LongRangeEnemyTypeData : AEnemyTypeData
    {
        [SerializeField] private ProjectileEnemyParameters parameters;
        [SerializeField] private LongRangeEnemyController prefab;
        private EEnemyType _type;

        public override Object Prefab => prefab;
        public override IEnemyParametersBase Parameters => parameters;
        public override EEnemyType Type => EEnemyType.LongRange;
    }
}