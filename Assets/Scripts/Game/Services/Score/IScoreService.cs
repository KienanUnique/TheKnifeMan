using UniRx;

namespace Game.Services.Score
{
    public interface IScoreService
    {
        IReactiveProperty<int> CurrentScore { get; }
        IReactiveProperty<bool> NeedScoreAchieved { get; }
        int NeedScore { get; }
        
        void IncreaseScore(int pointsToAdd);
    }
}