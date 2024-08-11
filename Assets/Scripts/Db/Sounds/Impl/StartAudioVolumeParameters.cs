using UnityEngine;
using Utils;

namespace Db.Sounds.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Parameters + nameof(StartAudioVolumeParameters), fileName = nameof(StartAudioVolumeParameters))]
    public class StartAudioVolumeParameters : ScriptableObject, IStartAudioVolumeParameters
    {
        [SerializeField] private float soundsVolume;
        [SerializeField] private float musicVolume;

        public float SoundsVolume => soundsVolume;
        public float MusicVolume => musicVolume;
    }
}