using System;
using Game.Services.Level;
using UniRx;

namespace Game.Services.Score.Impl
{
    public class ScoreService : IScoreService
    {
        private readonly ILevelsService _levelsService;
        
        private readonly ReactiveProperty<int> _currentScore = new(0);
        private readonly ReactiveCommand _needScoreAchieved = new();

        public IReactiveProperty<int> CurrentScore => _currentScore;
        public IObservable<Unit> NeedScoreAchieved => _needScoreAchieved;

        public ScoreService(ILevelsService levelsService)
        {
            var pause = new BoolReactiveProperty(); // create UniRx 
            _levelsService = levelsService;
        }

        public void IncreaseScore(int pointsToAdd)
        {
            _currentScore.Value += pointsToAdd;
            
            if (_currentScore.Value >= _levelsService.CurrentLevelData.TargetScore) 
                _needScoreAchieved.Execute();
        }
    }
}