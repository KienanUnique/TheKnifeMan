using System;
using System.Collections.Generic;
using Db.Sounds;
using ModestTree;
using Services.Settings;
using UniRx;
using UnityEngine;
using Utils.Sounds;
using Zenject;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Services.Sound.Impl
{
    public class BackgroundMusicService : IInitializable, IDisposable, IBackgroundMusicService
    {
        private readonly ISoundFxBase _soundFxBase;
        private readonly ISettingsStorageService _settingsStorageService;
        private readonly IAudioClipRepository _audioClipRepository;

        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly List<AudioClipVo> _availableMusic = new();

        private AudioSource _audioSource;
        private AudioClipVo _currentPlayingClipVo;
        private bool _needPlay;

        private bool IsMusicEnabled => _settingsStorageService.IsMusicEnabled.Value && _needPlay;

        public BackgroundMusicService(
            ISoundFxBase soundFxBase,
            ISettingsStorageService settingsStorageService,
            IAudioClipRepository audioClipRepository
        )
        {
            _soundFxBase = soundFxBase;
            _settingsStorageService = settingsStorageService;
            _audioClipRepository = audioClipRepository;
        }

        public void Initialize()
        {
            _audioSource = CreateAudioSource();

            _settingsStorageService.MusicVolume.Subscribe(OnMusicVolume).AddTo(_compositeDisposable);
            _settingsStorageService.IsMusicEnabled.Subscribe(OnIsMusicEnabled).AddTo(_compositeDisposable);

            OnMusicVolume(_settingsStorageService.MusicVolume.Value);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
        
        public void Play()
        {
            _needPlay = true;
            
            if(!_audioSource.isPlaying)
                PlayNextTrack();
        }

        public void Stop()
        {
            _needPlay = false;
            if(_audioSource.isPlaying)
                _audioSource.Stop();
        }

        private void OnMusicVolume(float newVolume)
        {
            if (!_audioSource.isPlaying)
                return;

            ApplyGeneralVolume(newVolume);
        }

        private void OnIsMusicEnabled(bool isMusicEnabled)
        {
            if (_audioSource.isPlaying && !IsMusicEnabled)
                _audioSource.Stop();
            else if (!_audioSource.isPlaying && IsMusicEnabled) 
                PlayNextTrack();
        }

        private AudioSource CreateAudioSource()
        {
            var gameObject = new GameObject("AudioSource");
            var source = gameObject.AddComponent<AudioSource>();
            Object.DontDestroyOnLoad(gameObject);
            return source;
        }

        private void ApplyGeneralVolume(float generalVolume)
        {
            if(!_audioSource.isPlaying)
                return;
            
            var volume = IsMusicEnabled ? _currentPlayingClipVo.volume * generalVolume : 0;
            _audioSource.volume = volume;
        }

        private void PlayNextTrack()
        {
            var generalVolume = _settingsStorageService.SoundsVolume.Value;

            if (_availableMusic.IsEmpty())
            {
                ResetPlaylist();
            }
            
            var nextTrackIndex = Random.Range(0, _availableMusic.Count);
            var nextTrackVo = _availableMusic[nextTrackIndex];
            _availableMusic.RemoveAt(nextTrackIndex);

            var nextTrackAudioClip = _audioClipRepository.GetClipByName(nextTrackVo.path);

            _audioSource.volume = generalVolume * nextTrackVo.volume;
            _audioSource.clip = nextTrackAudioClip;

            _currentPlayingClipVo = nextTrackVo; 

            _audioSource.Play();
            
            Observable.EveryUpdate()
                .Where(_ => !_audioSource.isPlaying)
                .Take(1)
                .Subscribe(_ =>
                {
                    if (IsMusicEnabled)
                    {
                        PlayNextTrack();
                    }
                })
                .AddTo(_compositeDisposable, _audioSource);
        }

        private void ResetPlaylist()
        {
            _availableMusic.AddRange(_soundFxBase.GetBackgroundMusic());
        }
    }
}