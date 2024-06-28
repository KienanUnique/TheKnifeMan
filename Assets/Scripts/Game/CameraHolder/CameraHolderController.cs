using Game.Object;
using UnityEngine;

namespace Game.CameraHolder
{
    public class CameraHolderController : AObjectController<CameraHolderData>
    {
        [SerializeField] private CameraHolderData data;
        protected override CameraHolderData Data => data;
    }
}