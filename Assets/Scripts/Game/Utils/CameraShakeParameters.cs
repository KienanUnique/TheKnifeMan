using System;
using UnityEngine;

namespace Game.Utils
{
    [Serializable]
    public class CameraShakeParameters
    {
        [SerializeField] private float _amplitudeGain;
        [SerializeField] private float _frequencyGain;
        [SerializeField] private float _durationSeconds;

        public float AmplitudeGain => _amplitudeGain;
        public float FrequencyGain => _frequencyGain;
        public float DurationSeconds => _durationSeconds;
    }
}