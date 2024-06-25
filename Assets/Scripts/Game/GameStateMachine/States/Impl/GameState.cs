using System.Collections.Generic;
using Game.Player;
using Game.Services.Score;
using Game.Services.Spawner;
using Game.Services.WaveTimer;
using Game.Ui.GameplayWindow;
using Game.Utils;
using KoboldUi.Utils;
using Services.Input;
using Services.Level;
using UniRx;
using Zenject;

namespace Game.GameStateMachine.States.Impl
{
    public class GameState : AState
    {
        private readonly IPlayerInformation _playerInformation;
        private readonly IScoreService _scoreService;
        private readonly ILevelsService _levelsService;
        private readonly ISpawnService _spawnService;
        private readonly IWaveTimerService _waveTimerService;
        private readonly IInputService _inputService;
        private readonly SignalBus _signalBus;
        private readonly List<IGameStateListener> _gameStateListeners;

        private int _nextWaveIndex = 0;

        private LevelSceneData CurrentLevelData => _levelsService.CurrentLevelData;

        public GameState(
            IPlayerInformation playerInformation,
            IScoreService scoreService,
            ILevelsService levelsService,
            ISpawnService spawnService,
            IWaveTimerService waveTimerService,
            List<IGameStateListener> gameStateListeners,
            IInputService inputService,
            SignalBus signalBus
        )
        {
            _playerInformation = playerInformation;
            _scoreService = scoreService;
            _levelsService = levelsService;
            _spawnService = spawnService;
            _waveTimerService = waveTimerService;
            _gameStateListeners = gameStateListeners;
            _inputService = inputService;
            _signalBus = signalBus;
        }

        protected override void HandleEnter()
        {
            _nextWaveIndex = 0;

            _playerInformation.IsDead.Subscribe(OnIsDead).AddTo(ActiveDisposable);
            _scoreService.NeedScoreAchieved.Subscribe(_ => OnNeedScoreAchieved()).AddTo(ActiveDisposable);
            _waveTimerService.OnTimerEnd.Subscribe(_ => StartNewWave()).AddTo(ActiveDisposable);

            StartNewWave();
            
            _inputService.SwitchToGameInput();
            
            _signalBus.OpenWindow<GameplayWindow>();
        }

        protected override void HandleExit()
        {
            _spawnService.ForceStopSpawning();
            foreach (var gameStateListener in _gameStateListeners)
            {
                gameStateListener.OnGameEnd();
            }
            
            _inputService.SwitchToUiInput();
        }

        private void OnNeedScoreAchieved()
        {
            GameStateMachine.Enter<WinState>();
        }

        private void OnIsDead(bool isDead)
        {
            if (!isDead)
                return;

            GameStateMachine.Enter<LoseState>();
        }

        private void StartNewWave()
        {
            var allWaves = CurrentLevelData.WavesParameters.WavesInfo;
            if (allWaves.Count <= _nextWaveIndex)
                return;

            var wave = allWaves[_nextWaveIndex];

            _spawnService.SpawnWave(wave);
            _waveTimerService.StartTimer(wave);

            _nextWaveIndex++;
        }
    }
}