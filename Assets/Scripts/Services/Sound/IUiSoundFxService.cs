using System;
using Utils.Sounds;

namespace Services.Sound
{
    public interface IUiSoundFxService
    {
        void Play(EUiSoundFxType soundFxType);
        void Play(EUiSoundFxType soundFxType, Action onCompleteCallBack);
    }
}