using System;
using UniRx;

namespace Game.Services.Score
{
    public interface IScoreService
    {
        IReactiveProperty<int> CurrentScore { get; }
        IObservable<Unit> NeedScoreAchieved { get; }
        
        void IncreaseScore(int pointsToAdd);
    }
}