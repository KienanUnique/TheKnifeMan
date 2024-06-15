using Game.Object;
using Game.Player;
using UnityEngine;
using Zenject;

namespace Game.Camera
{
    public class CameraController : AObjectController<CameraData>
    {
        [SerializeField] private CameraData data;

        [Inject] private IPlayerInformation _playerInformation;

        protected override CameraData Data => data;

        protected override void HandleInitialize()
        {
            var playerTransform = _playerInformation.Transform;

            data.VirtualCamera.Follow = playerTransform;
            data.VirtualCamera.LookAt = playerTransform;
        }
    }
}