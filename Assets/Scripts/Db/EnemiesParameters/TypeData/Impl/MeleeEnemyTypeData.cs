using Db.EnemiesParameters.Parameters;
using Db.EnemiesParameters.Parameters.Impl;
using Game.Enemy.Controller.Impl;
using Game.Utils;
using UnityEngine;
using Utils;

namespace Db.EnemiesParameters.TypeData.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Enemies + nameof(MeleeEnemyTypeData), fileName = nameof(MeleeEnemyTypeData))]
    public class MeleeEnemyTypeData : AEnemyTypeData
    {
        [SerializeField] private MeleeEnemyParameters parameters;
        [SerializeField] private MeleeEnemyController prefab;
        private EEnemyType _type;

        public override Object Prefab => prefab;
        public override IEnemyParametersBase Parameters => parameters;
        public override EEnemyType Type => EEnemyType.Melee;
    }
}