using System;
using FinalTitles;
using Services.Level;
using Services.Sound;
using UniRx;
using Zenject;

namespace Services
{
    public class FinalTitlesService : IInitializable, IDisposable
    {
        private readonly IBackgroundMusicService _backgroundMusicService;
        private readonly IFinalTitlesVideoPlayerController _finalTitlesVideoPlayerController;
        private readonly ILevelsService _levelsService;

        private readonly CompositeDisposable _compositeDisposable = new();

        public FinalTitlesService(
            IBackgroundMusicService backgroundMusicService,
            IFinalTitlesVideoPlayerController finalTitlesVideoPlayerController,
            ILevelsService levelsService
        )
        {
            _backgroundMusicService = backgroundMusicService;
            _finalTitlesVideoPlayerController = finalTitlesVideoPlayerController;
            _levelsService = levelsService;
        }

        public void Initialize()
        {
            _backgroundMusicService.Stop();
            _finalTitlesVideoPlayerController.Play();
            
            _finalTitlesVideoPlayerController.VideoEnded.Subscribe(_ => _levelsService.LoadMainMenu())
                .AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}