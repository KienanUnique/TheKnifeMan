using System;
using UnityEngine;

namespace Db.EnemiesParameters.Parameters.Impl
{
    [Serializable]
    public class MeleeEnemyParameters : AEnemyParameters, IMeleeEnemyParameters
    {
        [Header("Attack")] [SerializeField] private int damage = 1;

        public int Damage => damage;
    }
}