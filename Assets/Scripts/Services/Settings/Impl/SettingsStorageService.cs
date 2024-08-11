using Db.Sounds;
using UniRx;
using Zenject;

namespace Services.Settings.Impl
{
    public class SettingsStorageService : ISettingsStorageService, IInitializable
    {
        private readonly IStartAudioVolumeParameters _startAudioVolumeParameters;
        
        private readonly ReactiveProperty<float> _soundsVolume = new(1f);
        private readonly ReactiveProperty<float> _musicVolume = new(1f);
        private readonly ReactiveProperty<bool> _isSoundsEnabled = new(true);
        private readonly ReactiveProperty<bool> _isMusicEnabled = new(true);

        public IReactiveProperty<float> SoundsVolume => _soundsVolume;
        public IReactiveProperty<float> MusicVolume => _musicVolume;
        public IReactiveProperty<bool> IsSoundsEnabled => _isSoundsEnabled;
        public IReactiveProperty<bool> IsMusicEnabled => _isMusicEnabled;

        public SettingsStorageService(IStartAudioVolumeParameters startAudioVolumeParameters)
        {
            _startAudioVolumeParameters = startAudioVolumeParameters;
        }
        
        public void Initialize()
        {
            _soundsVolume.Value = _startAudioVolumeParameters.SoundsVolume;
            _musicVolume.Value = _startAudioVolumeParameters.MusicVolume;
        }

        public void SetSoundsVolume(float newSoundVolume) => _soundsVolume.Value = newSoundVolume;
        public void SetMusicVolume(float newSoundVolume) => _musicVolume.Value = newSoundVolume;
        public void SetIsSoundsEnabled(bool isSoundsEnabled) => _isSoundsEnabled.Value = isSoundsEnabled;
        public bool SetIsMusicEnabled(bool isMusicEnabled) => _isMusicEnabled.Value = isMusicEnabled;
    }
}