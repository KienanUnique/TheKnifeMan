using UniRx;

namespace Services.Settings
{
    public interface ISettingsStorageService
    {
        IReactiveProperty<float> SoundsVolume { get; }
        IReactiveProperty<float> MusicVolume { get; }
        IReactiveProperty<bool> IsSoundsEnabled { get; }
        IReactiveProperty<bool> IsMusicEnabled { get; }
        
        void SetSoundsVolume(float newSoundVolume);
        void SetMusicVolume(float newSoundVolume);
        void SetIsSoundsEnabled(bool isSoundsEnabled);
        bool SetIsMusicEnabled(bool isMusicEnabled);
    }
}