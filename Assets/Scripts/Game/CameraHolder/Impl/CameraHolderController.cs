using Game.Object;
using UnityEngine;

namespace Game.CameraHolder.Impl
{
    public class CameraHolderController : AObjectController<CameraHolderData>, ICameraHolderController
    {
        [SerializeField] private CameraHolderData data;

        public Camera Camera => data.Camera;
        protected override CameraHolderData Data => data;
    }
}