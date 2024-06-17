using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Utils.Spawner
{
    [Serializable]
    public class WaveData
    {
        [SerializeField] private List<EnemyWaveSpawnData> enemies;
        [SerializeField] private float delayBeforeSpawnSeconds;

        public List<EnemyWaveSpawnData> Enemies => enemies;
        public float DelayBeforeSpawnSeconds => delayBeforeSpawnSeconds;
    }
}