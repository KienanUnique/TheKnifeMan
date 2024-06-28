using System;
using System.Collections.Generic;
using Game.Utils.Spawner;
using UnityEngine;

namespace Game.Utils.Waves.Impl
{
    [Serializable]
    public class WavesParameters : IWavesParameters
    {
        [SerializeField] private List<WaveData> wavesInfo;

        public List<WaveData> WavesInfo => wavesInfo;
    }
}