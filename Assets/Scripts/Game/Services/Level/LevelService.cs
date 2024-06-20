using System;
using Db.Waves;
using Game.Player;
using Game.Services.Spawner;
using Game.Services.WaveTimer;
using UniRx;
using Zenject;

namespace Game.Services.Level
{
    public class LevelService : IInitializable, IDisposable
    {
        private readonly IWaveTimerService _waveTimerService;
        private readonly ISpawnService _spawnService;
        private readonly IWavesParameters _wavesParameters;
        private readonly IPlayerInformation _playerInformation;

        private readonly CompositeDisposable _compositeDisposable = new();

        private int _nextWaveIndex = 0;

        public LevelService(
            IWaveTimerService waveTimerService,
            ISpawnService spawnService,
            IWavesParameters wavesParameters,
            IPlayerInformation playerInformation
        )
        {
            _waveTimerService = waveTimerService;
            _spawnService = spawnService;
            _wavesParameters = wavesParameters;
            _playerInformation = playerInformation;
        }

        public void Initialize()
        {
            _waveTimerService.OnTimerEnd.Subscribe(_ => StartNewWave()).AddTo(_compositeDisposable);

            StartNewWave();
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

        private void StartNewWave()
        {
            var allWaves = _wavesParameters.WavesInfo;
            if (allWaves.Count <= _nextWaveIndex)
                return;

            var wave = allWaves[_nextWaveIndex];

            _spawnService.SpawnWave(wave);
            _waveTimerService.StartTimer(wave);

            _nextWaveIndex++;
        }
    }
}