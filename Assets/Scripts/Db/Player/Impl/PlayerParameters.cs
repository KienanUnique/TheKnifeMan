using Alchemy.Inspector;
using UnityEngine;
using Utils;

namespace Db.Player.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Parameters + nameof(PlayerParameters),
        fileName = nameof(PlayerParameters))]
    public class PlayerParameters : ScriptableObject, IPlayerParameters
    {
        [Header("Character")] [SerializeField] [Min(1)] private int health;

        [SerializeField] [Min(0)] private int damage = 1;

        [Header("Movement")] 
        [SerializeField] [Min(0f)] private float movementSpeed = 6.3f;
        [SerializeField] [Min(0f)] private float dashForce = 50f;
        [SerializeField] [Min(0f)] private float dashDurationSeconds = 0.2f;
        [SerializeField] [Min(0f)] private float dashCooldownSeconds = 0.2f;
        [SerializeField] [Min(0f)] private float dashDrag = 10f;

        [Header("Visual")] 
        [SerializeField] [Min(0f)] private float animatorMovingSqrVelocityThreshold = 0.001f;
        [SerializeField] [Min(0)] private int lowHealthThreshold = 1;
        
        [ValidateInput(nameof(IsDashEndAnimationDurationSecondsValid), "Must be less than dash cooldown")]
        [Min(0.001f)] [SerializeField] private float dashEndAnimationDurationSeconds = 0.15f;

        public int Health => health;
        public int Damage => damage;

        public float MovementSpeed => movementSpeed;
        public float DashForce => dashForce;
        public float DashDurationSeconds => dashDurationSeconds;
        public float DashCooldownSeconds => dashCooldownSeconds;
        public float DashEndAnimationDurationSeconds => dashEndAnimationDurationSeconds;
        public float DashDrag => dashDrag;
        public float AnimatorMovingVelocityThreshold => animatorMovingSqrVelocityThreshold;
        public int LowHealthThreshold => lowHealthThreshold;

        public bool IsDashEndAnimationDurationSecondsValid(float value) => value <= dashDurationSeconds;
    }
}