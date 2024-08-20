using System;
using UnityEngine;

namespace Db.EnemiesParameters.Parameters.Impl
{
    [Serializable]
    public class MeleeEnemyParameters : AEnemyParameters, IMeleeEnemyParameters
    {
        [Header("Attack")] [SerializeField] private int damage = 1;
        [SerializeField] private float comboReloadDurationSeconds = 4f;
        [SerializeField] private int countOfAttacksInCombo = 2;
        [SerializeField] private float delayBetweenMeleeAttacks = 1f;

        public int MeleeDamage => damage;
        public float ComboReloadDurationSeconds => comboReloadDurationSeconds;
        public int CountOfAttacksInCombo => countOfAttacksInCombo;
        public float DelayBetweenMeleeAttacks => delayBetweenMeleeAttacks;
    }
}