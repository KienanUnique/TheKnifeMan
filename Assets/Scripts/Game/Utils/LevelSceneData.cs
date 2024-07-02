using Game.Utils.Waves;
using Game.Utils.Waves.Impl;
using UnityEngine;

namespace Game.Utils
{
    [CreateAssetMenu(menuName = nameof(LevelSceneData), fileName = nameof(LevelSceneData))]
    public class LevelSceneData : ScriptableObject
    {
        [SerializeField] private string levelName;
        [SerializeField] private WavesParameters wavesParameters;
        [SerializeField] private int targetScore;

        public string LevelName => levelName;
        public int TargetScore => targetScore;
        public IWavesParameters WavesParameters => wavesParameters;
    }
}