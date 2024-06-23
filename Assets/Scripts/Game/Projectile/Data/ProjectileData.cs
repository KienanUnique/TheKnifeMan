using System;
using UnityEngine;

namespace Game.Projectile.Data
{
    [Serializable]
    public class ProjectileData
    {
        [SerializeField] private int damage = 1;
        [SerializeField] private float speed = 5f;
        [SerializeField] private float automaticDisappearDelaySeconds = 10f;
        [SerializeField] private bool ignoreEnemies = true;

        public int Damage => damage;
        public float Speed => speed;
        public float AutomaticDisappearDelaySeconds => automaticDisappearDelaySeconds;
        public bool IgnoreEnemies => ignoreEnemies;
    }
}