using Game.Utils.Spawner;

namespace Game.Services.Spawner
{
    public interface ISpawnService
    {
        void SpawnWave(WaveData wave);
    }
}