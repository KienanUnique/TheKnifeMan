using System;
using System.Linq;
using Alchemy.Inspector;
using UnityEngine;

namespace Game.Enemy.Data.Impl
{
    [Serializable]
    public class UniversalEnemyData : AEnemyData, IMeleeEnemyData, IProjectileEnemyData
    {
        [Header("Damage colliders")] [SerializeField]
        private BoxCollider2D damageColliderUp;

        [SerializeField] private BoxCollider2D damageColliderDown;
        [SerializeField] private BoxCollider2D damageColliderLeft;
        [SerializeField] private BoxCollider2D damageColliderRight;
        
        
        [Header("Projectiles points")]
        [SerializeField] private Transform projectilesSpawnPointLeft;
        [SerializeField] private Transform projectilesSpawnPointRight;
        
        public Transform ProjectilesSpawnPointLeft => projectilesSpawnPointLeft;
        public Transform ProjectilesSpawnPointRight => projectilesSpawnPointRight;
        public BoxCollider2D DamageColliderUp => damageColliderUp;
        public BoxCollider2D DamageColliderDown => damageColliderDown;
        public BoxCollider2D DamageColliderLeft => damageColliderLeft;
        public BoxCollider2D DamageColliderRight => damageColliderRight;

        [Button]
        public override void AutoFill()
        {
            base.AutoFill();

            var colliders = rootTransform.GetComponentsInChildren<BoxCollider2D>().ToList();
            damageColliderUp = colliders.FirstOrDefault(collider => collider.name.Contains("Up"));
            damageColliderDown = colliders.FirstOrDefault(collider => collider.name.Contains("Down"));
            damageColliderLeft = colliders.FirstOrDefault(collider => collider.name.Contains("Left"));
            damageColliderRight = colliders.FirstOrDefault(collider => collider.name.Contains("Right"));
            
            var allChildTransform = RootTransform.GetComponentsInChildren<Transform>();
            projectilesSpawnPointLeft = allChildTransform.First(transform => transform.name.Contains("Left"));
            projectilesSpawnPointRight = allChildTransform.First(transform => transform.name.Contains("Right"));
        }
    }
}