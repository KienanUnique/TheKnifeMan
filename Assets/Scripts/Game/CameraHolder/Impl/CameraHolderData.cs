using System;
using Game.Object;
using UnityEngine;

namespace Game.CameraHolder.Impl
{
    [Serializable]
    public class CameraHolderData : AObjectData
    {
        [SerializeField] private Camera camera;

        public Camera Camera => camera;
    }
}