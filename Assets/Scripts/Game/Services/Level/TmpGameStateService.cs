using System;
using Game.Services.Spawner;
using Game.Services.WaveTimer;
using UniRx;
using Zenject;

namespace Game.Services.Level
{
    public class TmpGameStateService : IInitializable, IDisposable
    {
        private readonly IWaveTimerService _waveTimerService;
        private readonly ISpawnService _spawnService;
        private readonly ILevelsService _levelsService;

        private readonly CompositeDisposable _compositeDisposable = new();

        private int _nextWaveIndex = 0;

        public TmpGameStateService(
            IWaveTimerService waveTimerService,
            ISpawnService spawnService,
            ILevelsService levelsService
        )
        {
            _waveTimerService = waveTimerService;
            _spawnService = spawnService;
            _levelsService = levelsService;
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
            var allWaves = _levelsService.CurrentLevelData.WavesParameters.WavesInfo;
            if (allWaves.Count <= _nextWaveIndex)
                return;

            var wave = allWaves[_nextWaveIndex];

            _spawnService.SpawnWave(wave);
            _waveTimerService.StartTimer(wave);

            _nextWaveIndex++;
        }
    }
}