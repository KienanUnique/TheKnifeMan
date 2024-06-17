using System.Collections.Generic;
using Game.Utils.Spawner;
using UnityEngine;
using Utils;

namespace Db.Waves.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Parameters + nameof(WavesParameters),
        fileName = nameof(WavesParameters))]
    public class WavesParameters : ScriptableObject, IWavesParameters
    {
        [SerializeField] private List<WaveData> wavesInfo;

        public List<WaveData> WavesInfo => wavesInfo;
    }
}