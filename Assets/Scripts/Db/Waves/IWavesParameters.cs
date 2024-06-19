using System.Collections.Generic;
using Game.Utils.Spawner;

namespace Db.Waves
{
    public interface IWavesParameters
    {
        List<WaveData> WavesInfo { get; }
    }
}