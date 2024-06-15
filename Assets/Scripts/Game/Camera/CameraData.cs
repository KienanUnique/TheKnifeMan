using System;
using Cinemachine;
using Game.Object;
using UnityEngine;

namespace Game.Camera
{
    [Serializable]
    public class CameraData : AObjectData
    {
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        public CinemachineVirtualCamera VirtualCamera => virtualCamera;
    }
}