﻿using Db.Sounds;
using UniRx;
using Zenject;

namespace Services.Settings.Impl
{
    public class SettingsStorageService : ISettingsStorageService, IInitializable
    {
        private readonly IGameDefaultParameters _gameDefaultParameters;
        
        private readonly ReactiveProperty<float> _soundsVolume = new(1f);
        private readonly ReactiveProperty<float> _musicVolume = new(1f);
        private readonly ReactiveProperty<bool> _isSoundsEnabled = new(true);
        private readonly ReactiveProperty<bool> _isMusicEnabled = new(true);
        private readonly ReactiveProperty<bool> _isEasyModeEnabled = new();

        public IReactiveProperty<float> SoundsVolume => _soundsVolume;
        public IReactiveProperty<float> MusicVolume => _musicVolume;
        public IReactiveProperty<bool> IsSoundsEnabled => _isSoundsEnabled;
        public IReactiveProperty<bool> IsMusicEnabled => _isMusicEnabled;
        public IReactiveProperty<bool> IsEasyModeEnabled => _isEasyModeEnabled;

        public SettingsStorageService(IGameDefaultParameters gameDefaultParameters)
        {
            _gameDefaultParameters = gameDefaultParameters;
        }
        
        public void Initialize()
        {
            _soundsVolume.Value = _gameDefaultParameters.SoundsVolume;
            _musicVolume.Value = _gameDefaultParameters.MusicVolume;
            _isEasyModeEnabled.Value = _gameDefaultParameters.IsEasyModeEnabled;
        }

        public void SetSoundsVolume(float newSoundVolume) => _soundsVolume.Value = newSoundVolume;
        public void SetMusicVolume(float newSoundVolume) => _musicVolume.Value = newSoundVolume;
        public void SetIsSoundsEnabled(bool isSoundsEnabled) => _isSoundsEnabled.Value = isSoundsEnabled;
        public void SetIsMusicEnabled(bool isMusicEnabled) => _isMusicEnabled.Value = isMusicEnabled;
        public void SetIsEasyModeEnabled(bool isMusicEnabled) =>_isEasyModeEnabled.Value = isMusicEnabled;
    }
}