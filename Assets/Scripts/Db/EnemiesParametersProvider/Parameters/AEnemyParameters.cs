using System;
using UnityEngine;

namespace Db.EnemiesParametersProvider.Parameters
{
    [Serializable]
    public class AEnemyParameters : IEnemyParametersBase
    {
        [Header("Character")]
        [SerializeField] [Min(1)] private int health;
        
        [Header("Movement")]
        [SerializeField] private float acceleration;
        [SerializeField] private float maxSpeed;
        
        [Header("Visual")]
        [SerializeField] private float animatorMovingVelocityThreshold;

        public float Acceleration => acceleration;
        public float MaxSpeed => maxSpeed;
        public int Health => health;
        public float AnimatorMovingVelocityThreshold => animatorMovingVelocityThreshold;
    }
}