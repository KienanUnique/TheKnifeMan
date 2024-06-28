using System.Collections.Generic;
using Game.Utils.Spawner;

namespace Game.Utils.Waves
{
    public interface IWavesParameters
    {
        List<WaveData> WavesInfo { get; }
    }
}