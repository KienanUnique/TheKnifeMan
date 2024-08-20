using System;
using Db.Score;
using Game.Player;
using Services.Level;
using UniRx;
using Zenject;

namespace Game.Services.Score.Impl
{
    public class ScoreService : IScoreService, IInitializable, IDisposable
    {
        private readonly ILevelsService _levelsService;
        private readonly IPlayerInformation _playerInformation;
        private readonly IScoreParameters _scoreParameters;

        private readonly ReactiveProperty<int> _currentScore = new(0);
        private readonly ReactiveProperty<bool> _needScoreAchieved = new();
        private readonly CompositeDisposable _compositeDisposable = new();

        private float _currentScoreModifier = 1f;
        private int _lastPlayerHealth;

        public IReactiveProperty<int> CurrentScore => _currentScore;
        public IReactiveProperty<bool> NeedScoreAchieved => _needScoreAchieved;
        public int NeedScore => _levelsService.CurrentLevelData.TargetScore;

        public ScoreService(
            ILevelsService levelsService,
            IPlayerInformation playerInformation,
            IScoreParameters scoreParameters
        )
        {
            _levelsService = levelsService;
            _playerInformation = playerInformation;
            _scoreParameters = scoreParameters;
        }

        public void Initialize()
        {
            if(_playerInformation.IsInitilized.Value)
                SubscribePlayerHealth();
            else
                _playerInformation.IsInitilized.Subscribe(isInitilized =>
                {
                    if(!isInitilized)
                        return;
                    
                    SubscribePlayerHealth();
                }).AddTo(_compositeDisposable);
        }

        private void SubscribePlayerHealth()
        {
            _lastPlayerHealth = _playerInformation.Health.Value; 
            _playerInformation.Health.Subscribe(OnPlayerHealth).AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

        public void IncreaseScore(int pointsToAdd)
        {
            var totalPointsToAdd = (int) (pointsToAdd * _currentScoreModifier);
            _currentScore.Value += totalPointsToAdd;

            _currentScoreModifier += _scoreParameters.AdditionalRatioForEnemyKill;

            _needScoreAchieved.Value = _currentScore.Value >= _levelsService.CurrentLevelData.TargetScore;
        }

        private void OnPlayerHealth(int newHealthValue)
        {
            if (_lastPlayerHealth > newHealthValue) 
                _currentScoreModifier = 1f;

            _lastPlayerHealth = newHealthValue;
        }
    }
}