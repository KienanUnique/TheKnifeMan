using UnityEngine;
using Utils;

namespace Db.Player.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Parameters + nameof(PlayerParameters),
        fileName = nameof(PlayerParameters))]
    public class PlayerParameters : ScriptableObject, IPlayerParameters
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private float dashSpeed;
        [SerializeField] private float dashDurationSeconds;

        public float MovementSpeed => movementSpeed;
        public float DashSpeed => dashSpeed;
        public float DashDurationSeconds => dashDurationSeconds;
    }
}