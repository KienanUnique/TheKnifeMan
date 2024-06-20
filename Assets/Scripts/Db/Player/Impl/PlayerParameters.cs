using UnityEngine;
using Utils;

namespace Db.Player.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Parameters + nameof(PlayerParameters),
        fileName = nameof(PlayerParameters))]
    public class PlayerParameters : ScriptableObject, IPlayerParameters
    {
        [Header("Character")] [SerializeField] [Min(1)]
        private int health;

        [SerializeField] [Min(0)] private int damage;

        [Header("Movement")] [SerializeField] private float movementSpeed;
        [SerializeField] private float dashSpeed;
        [SerializeField] private float dashDurationSeconds;

        [Header("Visual")] [SerializeField] private float animatorMovingSqrVelocityThreshold;
        [SerializeField] private int lowHealthThreshold;

        public int Health => health;
        public int Damage => damage;

        public float MovementSpeed => movementSpeed;
        public float DashSpeed => dashSpeed;
        public float DashDurationSeconds => dashDurationSeconds;
        public float AnimatorMovingVelocityThreshold => animatorMovingSqrVelocityThreshold;
        public int LowHealthThreshold => lowHealthThreshold;
    }
}