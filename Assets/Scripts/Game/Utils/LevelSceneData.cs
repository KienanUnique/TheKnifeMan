using System;
using Game.Utils.Waves;
using Game.Utils.Waves.Impl;
using UnityEngine;

namespace Game.Utils
{
    [Serializable]
    public class LevelSceneData
    {
        [SerializeField] private string levelName;
        [SerializeField] private WavesParameters wavesParameters;
        [SerializeField] private int targetScore;

        public string LevelName => levelName;
        public int TargetScore => targetScore;
        public IWavesParameters WavesParameters => wavesParameters;
    }
}