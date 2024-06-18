using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Utils.Spawner
{
    [Serializable]
    public class WaveData
    {
        [SerializeField] private List<EnemyWaveSpawnData> enemies;
        [SerializeField] private int delayBeforeSpawnSeconds;

        public List<EnemyWaveSpawnData> Enemies => enemies;
        public int DelayBeforeSpawnSeconds => delayBeforeSpawnSeconds;
    }
}