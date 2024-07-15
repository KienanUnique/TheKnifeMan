using System;
using System.Linq;
using Alchemy.Inspector;
using UnityEngine;

namespace Game.Enemy.Data.Impl
{
    [Serializable]
    public class LongRangeEnemyData : AEnemyData, IProjectileEnemyData
    {
        [SerializeField] private Transform projectilesSpawnPointLeft;
        [SerializeField] private Transform projectilesSpawnPointRight;
        
        public Transform ProjectilesSpawnPointLeft => projectilesSpawnPointLeft;
        public Transform ProjectilesSpawnPointRight => projectilesSpawnPointRight;

        [Button]
        public override void AutoFill()
        {
            base.AutoFill();

            var allChildTransform = RootTransform.GetComponentsInChildren<Transform>();
            projectilesSpawnPointLeft = allChildTransform.First(transform => transform.name.Contains("Left"));
            projectilesSpawnPointRight = allChildTransform.First(transform => transform.name.Contains("Right"));
        }
    }
}