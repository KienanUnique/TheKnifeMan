using System;
using UnityEngine;

namespace Db.Enemy
{
    [Serializable]
    public class AEnemyParameters : IEnemyParametersBase
    {
        [Header("Character")]
        [SerializeField] [Min(1)] private int health;
        
        [Header("Movement")]
        [SerializeField] private float acceleration;
        [SerializeField] private float maxSpeed;

        public float Acceleration => acceleration;
        public float MaxSpeed => maxSpeed;
        public int Health => health;
    }
}