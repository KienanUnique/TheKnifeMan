using System;
using UnityEngine;

namespace Db.EnemiesParameters.Parameters.Impl
{
    [Serializable]
    public class ProjectileEnemyParameters : AEnemyParameters, IProjectileEnemyParameters
    {
        [SerializeField] private float reloadDurationSeconds = 2f;

        public float ReloadDurationSeconds => reloadDurationSeconds;
    }
}