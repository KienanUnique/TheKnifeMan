using UnityEngine;
using Utils.Sounds;

namespace Services.Sound
{
    public interface IGameSoundFxService
    {
        void Play(EGameSoundFxType soundFxType);
        void Play(EGameSoundFxType soundFxType, Vector3 position);
        void Play(EGameSoundFxType soundFxType, Transform parent);
    }
}