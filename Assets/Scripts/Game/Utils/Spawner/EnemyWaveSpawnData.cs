using System;
using UnityEngine;

namespace Game.Utils.Spawner
{
    [Serializable]
    public class EnemyWaveSpawnData
    {
        [SerializeField] private EEnemyType enemy;
        [SerializeField] [Min(1)] private int count;

        public EEnemyType Enemy => enemy;
        public int Count => count;
    }
}