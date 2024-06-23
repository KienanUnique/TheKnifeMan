using System;
using UnityEngine;

namespace Db.EnemiesParameters.Parameters
{
    [Serializable]
    public class AEnemyParameters : IEnemyParametersBase
    {
        [Header("Character")] [SerializeField] [Min(1)]
        private int health = 1;

        [Header("Movement")] [SerializeField] private float acceleration = 5;
        [SerializeField] private float maxSpeed = 5;

        [Header("Visual")] [SerializeField] private float animatorMovingVelocityThreshold = 0.01f;

        public float Acceleration => acceleration;
        public float MaxSpeed => maxSpeed;
        public int Health => health;
        public float AnimatorMovingVelocityThreshold => animatorMovingVelocityThreshold;
    }
}