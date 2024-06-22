using System;
using UnityEngine;

namespace Game.Projectile.Data
{
    [Serializable]
    public class ProjectileData
    {
        [SerializeField] private int damage;
        [SerializeField] private float speed;
        [SerializeField] private float automaticDisappearDelaySeconds;
        [SerializeField] private bool canDamageEnemies;

        public int Damage => damage;
        public float Speed => speed;
        public float AutomaticDisappearDelaySeconds => automaticDisappearDelaySeconds;
        public bool CanDamageEnemies => canDamageEnemies;
    }
}