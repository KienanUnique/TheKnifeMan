using Utils.Sounds;

namespace Services.Sound
{
    public interface IUiSoundFxService
    {
        void Play(EUiSoundFxType soundFxType);
    }
}