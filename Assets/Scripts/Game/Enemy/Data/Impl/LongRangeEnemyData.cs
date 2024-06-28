using System;
using System.Linq;
using Alchemy.Inspector;
using UnityEngine;

namespace Game.Enemy.Data.Impl
{
    [Serializable]
    public class LongRangeEnemyData : AEnemyData, IProjectileEnemyData
    {
        [SerializeField] private Transform projectilesSpawnPoint;
            
        public Transform ProjectilesSpawnPoint => projectilesSpawnPoint;

        [Button]
        public override void AutoFill()
        {
            base.AutoFill();

            var allChildTransform = RootTransform.GetComponentsInChildren<Transform>();
            projectilesSpawnPoint = allChildTransform.First(transform => transform.name.Contains("Projectile"));
        }
    }
}