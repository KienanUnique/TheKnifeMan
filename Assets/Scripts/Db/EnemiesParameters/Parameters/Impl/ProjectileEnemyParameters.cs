using System;
using Game.Projectile.Pattern;
using Game.Projectile.TypeData;
using Game.Projectile.TypeData.Impl;
using UnityEngine;

namespace Db.EnemiesParameters.Parameters.Impl
{
    [Serializable]
    public class ProjectileEnemyParameters : AEnemyParameters, IProjectileEnemyParameters
    {
        [SerializeField] private float reloadDurationSeconds = 5f;
        [SerializeField] private int countOfAttacksInClip = 3;
        [SerializeField] private float delayBetweenAttacks = 2f;
        [SerializeField] private AProjectilesPattern pattern;
        [SerializeField] private ProjectileTypeData type;

        public float ReloadDurationSeconds => reloadDurationSeconds;
        public int CountOfAttacksInClip => countOfAttacksInClip;
        public float DelayBetweenShootAttacks => delayBetweenAttacks;
        public IProjectilesPattern Pattern => pattern;
        public IProjectileType Type => type;
    }
}