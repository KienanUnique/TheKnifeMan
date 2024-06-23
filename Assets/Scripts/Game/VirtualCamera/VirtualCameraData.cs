using System;
using Cinemachine;
using Game.Object;
using UnityEngine;

namespace Game.VirtualCamera
{
    [Serializable]
    public class VirtualCameraData : AObjectData
    {
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        public CinemachineVirtualCamera VirtualCamera => virtualCamera;
    }
}