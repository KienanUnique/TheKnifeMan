using System;
using UnityEngine;

namespace Db.EnemiesParametersProvider.Parameters.Impl
{
    [Serializable]
    public class SimpleEnemyParameters : AEnemyParameters, ISimpleEnemyParameters
    {
        [Header("Attack")] [SerializeField] private int damage = 1;

        public int Damage => damage;
    }
}