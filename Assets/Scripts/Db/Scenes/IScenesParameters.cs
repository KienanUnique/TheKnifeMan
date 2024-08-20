using System.Collections.Generic;
using Game.Utils;

namespace Db.Scenes
{
    public interface IScenesParameters
    {
        string MainMenuSceneName { get; }
        string FinalTitlesSceneName { get; }
        IReadOnlyList<LevelSceneData> Levels { get; }
    }
}