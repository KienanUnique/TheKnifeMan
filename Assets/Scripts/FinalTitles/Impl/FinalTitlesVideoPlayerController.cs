using System;
using Game.CameraHolder;
using Game.Object;
using Services.Settings;
using UniRx;
using UnityEngine;
using UnityEngine.Video;
using Zenject;

namespace FinalTitles.Impl
{
    public class FinalTitlesVideoPlayerController : AObjectController<FinalTitlesVideoPlayerData>,
        IFinalTitlesVideoPlayerController
    {
        private readonly ReactiveCommand _videoEnded = new();

        [SerializeField] private FinalTitlesVideoPlayerData data;

        [Inject] private ISettingsStorageService _settingsStorage;
        [Inject] private ICameraHolderController _cameraHolderController;

        public IObservable<Unit> VideoEnded => _videoEnded;
        protected override FinalTitlesVideoPlayerData Data => data;

        protected override void HandleInitialize()
        {
            var videoPlayer = data.VideoPlayer;
            videoPlayer.targetCamera = _cameraHolderController.Camera;
        }

        public void Play()
        {
            var videoPlayer = data.VideoPlayer;
            
            videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, data.VideoName);

            var needVolume = _settingsStorage.SoundsVolume.Value;
            for (ushort i = 0; i < videoPlayer.controlledAudioTrackCount; i++)
                videoPlayer.SetDirectAudioVolume(i, needVolume);

            videoPlayer.Play();
        }

        private void OnEnable()
        {
            var videoPlayer = data.VideoPlayer;
            videoPlayer.loopPointReached += OnVideoEnded;
        }

        private void OnDisable()
        {
            var videoPlayer = data.VideoPlayer;
            videoPlayer.loopPointReached -= OnVideoEnded;
        }
        
        private void OnVideoEnded(VideoPlayer videoPlayer)
        {
            _videoEnded.Execute();
        }
    }
}