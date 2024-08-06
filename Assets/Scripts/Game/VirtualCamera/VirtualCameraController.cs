using System;
using Cinemachine;
using Db.Player;
using Game.Object;
using Game.Player;
using Game.Utils;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.VirtualCamera
{
    public class VirtualCameraController : AObjectController<VirtualCameraData>
    {
        [SerializeField] private VirtualCameraData data;

        [Inject] private IPlayerInformation _playerInformation;
        [Inject] private IPlayerParameters _playerParameters;

        private CinemachineBasicMultiChannelPerlin _cinemachinePerlin;
        private IDisposable _animationDisposable;

        protected override VirtualCameraData Data => data;

        protected override void HandleInitialize()
        {
            var playerTransform = _playerInformation.Transform;

            data.VirtualCamera.Follow = playerTransform;
            data.VirtualCamera.LookAt = playerTransform;

            _cinemachinePerlin = data.VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            _playerInformation.Health.Subscribe(OnHealthChange).AddTo(this);
            _playerInformation.EnemyDamaged.Subscribe(_ => OnEnemyDamaged()).AddTo(this);
        }

        private void OnHealthChange(int newHealth)
        {
            var isHealthMax = _playerParameters.Health == newHealth;
            if (isHealthMax)
                return;
            
            PlayShakeAnimation(data.PlayerTakeDamageEffect);
        }
        
        private void OnEnemyDamaged()
        {
            PlayShakeAnimation(data.EnemyTakeDamageEffect);
        }

        private void PlayShakeAnimation(CameraShakeParameters parameters)
        {
            _animationDisposable?.Dispose();
            
            _cinemachinePerlin.m_AmplitudeGain = parameters.AmplitudeGain;
            _cinemachinePerlin.m_FrequencyGain = parameters.FrequencyGain;

            _animationDisposable = Observable.Timer(TimeSpan.FromSeconds(parameters.DurationSeconds))
                .Subscribe(_ => ResetShaking()).AddTo(this);
        }

        private void ResetShaking()
        {
            _cinemachinePerlin.m_AmplitudeGain = 0f;
            _cinemachinePerlin.m_FrequencyGain = 0f;
        }
    }
}