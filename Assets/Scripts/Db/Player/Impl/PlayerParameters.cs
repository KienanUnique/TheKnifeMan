using UnityEngine;
using Utils;

namespace Db.Player.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Parameters + nameof(PlayerParameters),
        fileName = nameof(PlayerParameters))]
    public class PlayerParameters : ScriptableObject, IPlayerParameters
    {
        [Header("Character")]
        [SerializeField] private int health;
        
        [Header("Movement")]
        [SerializeField] private float movementSpeed;
        [SerializeField] private float dashSpeed;
        [SerializeField] private float dashDurationSeconds;

        [Header("Visual")]
        [SerializeField] private float animatorMovingSqrVelocityThreshold;
        [SerializeField] private int lowHealthThreshold;

        public int Health => health;
        public float MovementSpeed => movementSpeed;
        public float DashSpeed => dashSpeed;
        public float DashDurationSeconds => dashDurationSeconds;
        public float AnimatorMovingVelocityThreshold => animatorMovingSqrVelocityThreshold;
        public int LowHealthThreshold => lowHealthThreshold;
    }
}