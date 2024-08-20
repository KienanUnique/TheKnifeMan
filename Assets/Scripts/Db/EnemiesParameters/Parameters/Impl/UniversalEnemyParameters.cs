using System;
using Game.Projectile.Pattern;
using Game.Projectile.TypeData;
using Game.Projectile.TypeData.Impl;
using UnityEngine;

namespace Db.EnemiesParameters.Parameters.Impl
{
    [Serializable]
    public class UniversalEnemyParameters : AEnemyParameters, IMeleeEnemyParameters, IProjectileEnemyParameters
    {
        [Header("Melee attacks")]
        [SerializeField] private int meleeDamage = 1;
        [SerializeField] private float comboReloadDurationSeconds = 4f;
        [SerializeField] private int countOfAttacksInCombo = 2;
        [SerializeField] private float delayBetweenMeleeAttacks = 1f;

        [Header("Long range attacks")]
        [SerializeField] private float reloadDurationSeconds = 5f;
        [SerializeField] private int countOfAttacksInClip = 3;
        [SerializeField] private float delayBetweenShootAttacks = 2f;
        [SerializeField] private AProjectilesPattern pattern;
        [SerializeField] private ProjectileTypeData type;

        public float ReloadDurationSeconds => reloadDurationSeconds;
        public int CountOfAttacksInClip => countOfAttacksInClip;
        public float DelayBetweenShootAttacks => delayBetweenShootAttacks;
        public IProjectilesPattern Pattern => pattern;
        public IProjectileType Type => type;
        
        public int MeleeDamage => meleeDamage;
        public float ComboReloadDurationSeconds => comboReloadDurationSeconds;
        public int CountOfAttacksInCombo => countOfAttacksInCombo;
        public float DelayBetweenMeleeAttacks => delayBetweenMeleeAttacks;
    }
}