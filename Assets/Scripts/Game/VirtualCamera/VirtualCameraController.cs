using Game.Object;
using Game.Player;
using UnityEngine;
using Zenject;

namespace Game.VirtualCamera
{
    public class VirtualCameraController : AObjectController<VirtualCameraData>
    {
        [SerializeField] private VirtualCameraData data;

        [Inject] private IPlayerInformation _playerInformation;

        protected override VirtualCameraData Data => data;

        protected override void HandleInitialize()
        {
            var playerTransform = _playerInformation.Transform;

            data.VirtualCamera.Follow = playerTransform;
            data.VirtualCamera.LookAt = playerTransform;
        }
    }
}