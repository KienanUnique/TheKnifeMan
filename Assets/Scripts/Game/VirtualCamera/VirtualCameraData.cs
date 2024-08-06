using System;
using Cinemachine;
using Game.Object;
using Game.Utils;
using UnityEngine;

namespace Game.VirtualCamera
{
    [Serializable]
    public class VirtualCameraData : AObjectData
    {
        [Header("Links")]
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        
        [Header("Animations")]
        [SerializeField] private CameraShakeParameters enemyTakeDamageEffect;
        [SerializeField] private CameraShakeParameters playerTakeDamageEffect;

        public CinemachineVirtualCamera VirtualCamera => virtualCamera;
        public CameraShakeParameters EnemyTakeDamageEffect => enemyTakeDamageEffect;
        public CameraShakeParameters PlayerTakeDamageEffect => playerTakeDamageEffect;
    }
}