using Game.Utils;
using UniRx;

namespace Services.Level
{
    public interface ILevelsService
    {
        IReactiveProperty<float> LoadingProgress { get; }
        IReactiveProperty<bool> IsLoadingCompleted { get; }
        LevelSceneData CurrentLevelData { get; }
        
        void LoadNextLevel();
        void ReloadLevel();
        void LoadMainMenu();
        void LoadFirstLevel();
    }
}