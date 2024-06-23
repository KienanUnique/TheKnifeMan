using System.Collections.Generic;
using Game.Utils;
using UnityEngine;
using Utils;

namespace Db.Scenes.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Parameters + nameof(ScenesParameters), fileName = nameof(ScenesParameters))]
    public class ScenesParameters : ScriptableObject, IScenesParameters
    {
        [SerializeField] private string mainMenuSceneName;
        [SerializeField] private List<LevelSceneData> levels;

        public string MainMenuSceneName => mainMenuSceneName;
        public IReadOnlyList<LevelSceneData> Levels => levels;
    }
}