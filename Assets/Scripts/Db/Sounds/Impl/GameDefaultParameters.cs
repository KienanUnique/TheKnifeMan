using UnityEngine;
using Utils;

namespace Db.Sounds.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Parameters + nameof(GameDefaultParameters), fileName = nameof(GameDefaultParameters))]
    public class GameDefaultParameters : ScriptableObject, IGameDefaultParameters
    {
        [SerializeField] private float soundsVolume = 0.3f;
        [SerializeField] private float musicVolume = 0.1f;
        [SerializeField] private bool isEasyModeEnabled = false;

        public float SoundsVolume => soundsVolume;
        public float MusicVolume => musicVolume;
        public bool IsEasyModeEnabled => isEasyModeEnabled;
    }
}