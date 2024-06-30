using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Db.Sounds;
using Services.Settings;
using UniRx;
using UnityEngine;
using Utils.Sounds;
using Zenject;

namespace Services.Sound
{
    public abstract class ASoundFxService<T> : IInitializable, IDisposable where T : Enum
    {
        private readonly ISettingsStorageService _settingsStorageService;
        private readonly IAudioSourcePool _audioSourcePool;
        private readonly IAudioClipRepository _audioClipRepository;

        private readonly Dictionary<AudioSource, AudioClipVo> _activeAudioSources = new();
        private readonly CompositeDisposable _compositeDisposable = new();
        
        private bool IsSoundsEnabled => _settingsStorageService.IsSoundsEnabled.Value;

        protected ASoundFxService(
            ISettingsStorageService settingsStorageService,
            IAudioSourcePool audioSourcePool,
            IAudioClipRepository audioClipRepository
        )
        {
            _settingsStorageService = settingsStorageService;
            _audioSourcePool = audioSourcePool;
            _audioClipRepository = audioClipRepository;
        }
        
        public void Initialize()
        {
            _settingsStorageService.IsSoundsEnabled.Subscribe(OnIsSoundsEnabled).AddTo(_compositeDisposable);
            _settingsStorageService.SoundsVolume.Subscribe(OnSoundsVolume).AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

        public void Play(T soundFxType)
        {
            var audioSource = GetAudioSourceWithSfx(soundFxType);
            audioSource.Play();
        }

        public void Play(T soundFxType, Vector3 position)
        {
            var audioSource = GetAudioSourceWithSfx(soundFxType);
            audioSource.transform.position = position;
            audioSource.Play();
        }

        public void Play(T soundFxType, Transform parent)
        {
            var audioSource = GetAudioSourceWithSfx(soundFxType);
            audioSource.transform.SetParent(parent);
            audioSource.Play();
        }

        protected abstract AudioClipVo GetSoundVoByType(T type);
        
        private void OnIsSoundsEnabled(bool isSoundsEnabled)
        {
            ApplyGeneralVolumeToAllSounds(isSoundsEnabled ? _settingsStorageService.SoundsVolume.Value : 0);
        }
        
        private void OnSoundsVolume(float volume)
        {
            ApplyGeneralVolumeToAllSounds(_settingsStorageService.SoundsVolume.Value);
        }
        
        private AudioSource GetAudioSourceWithSfx(T soundFxType)
        {
            var soundVo = GetSoundVoByType(soundFxType);

            var clip = _audioClipRepository.GetClipByName(soundVo.path);

            var source = _audioSourcePool.Get();
            source.clip = clip;

            ApplyGeneralVolume(source, soundVo);
            
            _activeAudioSources.Add(source, soundVo);
            
            UniTask.WaitWhile(() => source.isPlaying)
                .ContinueWith(() =>
                {
                    _activeAudioSources.Remove(source);
                    _audioSourcePool.Return(source);
                }).Forget();

            return source;
        }

        private void ApplyGeneralVolumeToAllSounds(float volume)
        {
            foreach (var (audioSource, audioClipVo) in _activeAudioSources)
            {
                ApplyGeneralVolume(volume, audioSource, audioClipVo);
            }
        }
        
        private void ApplyGeneralVolume(AudioSource audioSource, AudioClipVo audioClipVo)
        {
            var volume = IsSoundsEnabled ? audioClipVo.volume * _settingsStorageService.SoundsVolume.Value : 0;  
            audioSource.volume = volume;
        }
        
        private void ApplyGeneralVolume(float generalVolume, AudioSource audioSource, AudioClipVo audioClipVo)
        {
            var volume = IsSoundsEnabled ? audioClipVo.volume * generalVolume : 0;  
            audioSource.volume = volume;
        }
    }
}