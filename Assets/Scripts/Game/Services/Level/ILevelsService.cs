using Game.Utils;

namespace Game.Services.Level
{
    public interface ILevelsService
    {
        LevelSceneData CurrentLevelData { get; }
    }
}