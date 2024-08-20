using System;
using Db.EnemiesParameters.TypeData;
using UnityEngine;

namespace Game.Utils.Spawner
{
    [Serializable]
    public class EnemyWaveSpawnData
    {
        [SerializeField] private AEnemyTypeData enemy;
        [SerializeField] [Min(1)] private int count;

        public IEnemyType Enemy => enemy;
        public int Count => count;
    }
}