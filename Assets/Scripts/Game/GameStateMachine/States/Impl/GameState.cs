using System.Collections.Generic;
using Game.Player;
using Game.Services.Pause;
using Game.Services.Score;
using Game.Services.Spawner;
using Game.Services.WaveTimer;
using Game.Ui.GameplayWindow;
using Game.Utils;
using KoboldUi.Utils;
using Services.Input;
using Services.Level;
using Services.Settings;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.GameStateMachine.States.Impl
{
    public class GameState : AState
    {
        private readonly IPlayerController _player;
        private readonly IScoreService _scoreService;
        private readonly ILevelsService _levelsService;
        private readonly ISpawnService _spawnService;
        private readonly IWaveTimerService _waveTimerService;
        private readonly IInputService _inputService;
        private readonly SignalBus _signalBus;
        private readonly IPauseService _pauseService;
        private readonly ISettingsStorageService _settingsStorageService;
        private readonly List<IGameStateListener> _gameStateListeners;

        private int _nextWaveIndex;

        private LevelSceneData CurrentLevelData => _levelsService.CurrentLevelData;

        public GameState(
            IPlayerController player,
            IScoreService scoreService,
            ILevelsService levelsService,
            ISpawnService spawnService,
            IWaveTimerService waveTimerService,
            List<IGameStateListener> gameStateListeners,
            IInputService inputService,
            SignalBus signalBus,
            IPauseService pauseService,
            ISettingsStorageService settingsStorageService
        )
        {
            _player = player;
            _scoreService = scoreService;
            _levelsService = levelsService;
            _spawnService = spawnService;
            _waveTimerService = waveTimerService;
            _gameStateListeners = gameStateListeners;
            _inputService = inputService;
            _signalBus = signalBus;
            _pauseService = pauseService;
            _settingsStorageService = settingsStorageService;
        }

        protected override void HandleEnter()
        {
            _player.IsDead.Subscribe(OnIsDead).AddTo(ActiveDisposable);
            _scoreService.NeedScoreAchieved.Subscribe(OnNeedScoreAchieved).AddTo(ActiveDisposable);
            _waveTimerService.OnTimerEnd.Subscribe(_ => StartNewWave()).AddTo(ActiveDisposable);
            _inputService.PausePressed.Subscribe(_ => OnPauseButtonPressed()).AddTo(ActiveDisposable);
            _inputService.RestartLevelPressed.Subscribe(_ => OnRestartLevelPressed()).AddTo(ActiveDisposable);
            _pauseService.IsPaused.Subscribe(OnPause).AddTo(ActiveDisposable);

            if (!_waveTimerService.IsTimerRunning) 
                StartNewWave();

            _inputService.SwitchToGameInput();
            
            _signalBus.OpenWindow<GameplayWindow>();
        }

        private void OnRestartLevelPressed()
        {
            if(!_levelsService.IsLoadingCompleted.Value)
                return;
            
            ActiveDisposable?.Dispose();
            StopSpawnersAndEnemiesLogic(false);
            _levelsService.ReloadLevel();
        }

        protected override void HandleExit()
        {
            ActiveDisposable?.Dispose();
        }

        private void OnPause(bool isPaused)
        {
            if(!isPaused)
                return;
            
            GameStateMachine.Enter<PauseState>();
        }

        private void OnPauseButtonPressed() => _pauseService.EnablePause();

        private void OnNeedScoreAchieved(bool isNeedScoreAchieved)
        {
            if(!isNeedScoreAchieved)
                return;
            
            StopSpawnersAndEnemiesLogic(true);
            GameStateMachine.Enter<WinState>();
        }

        private void OnIsDead(bool isDead)
        {
            if (!isDead)
                return;

            StopSpawnersAndEnemiesLogic(false);
            GameStateMachine.Enter<LoseState>();
        }

        private void StartNewWave()
        {
            var allWaves = CurrentLevelData.WavesParameters.WavesInfo;
            if (allWaves.Count <= _nextWaveIndex)
                return;
            
            Debug.Log($"@@@ _settingsStorageService.IsEasyModeEnabled.Value: {_settingsStorageService.IsEasyModeEnabled.Value} ");
            if(_settingsStorageService.IsEasyModeEnabled.Value)
                _player.ResetHealth();

            var wave = allWaves[_nextWaveIndex];

            _spawnService.SpawnWave(wave);
            
            _nextWaveIndex++;
            if(allWaves.Count > _nextWaveIndex)
                _waveTimerService.StartTimer(wave);
        }

        private void StopSpawnersAndEnemiesLogic(bool isPlayerWin)
        {
            _spawnService.ForceStopSpawning();
            foreach (var gameStateListener in _gameStateListeners)
            {
                gameStateListener.OnGameEnd(isPlayerWin);
            }
        }
    }
}