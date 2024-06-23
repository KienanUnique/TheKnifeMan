using Db.Scenes;
using Game.Utils;

namespace Game.Services.Level.Impl
{
    // TODO: implement service logic
    public class LevelsService : ILevelsService
    {
        private readonly IScenesParameters _scenesParameters;

        public LevelsService(IScenesParameters scenesParameters)
        {
            _scenesParameters = scenesParameters;
        }

        public LevelSceneData CurrentLevelData => _scenesParameters.Levels[0];
    }
}