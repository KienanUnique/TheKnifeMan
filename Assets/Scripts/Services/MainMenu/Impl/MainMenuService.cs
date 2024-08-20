using KoboldUi.Utils;
using Services.Sound;
using Ui.MainMenu;
using Zenject;

namespace Services.MainMenu.Impl
{
    public class MainMenuService : IInitializable
    {
        private readonly SignalBus _signalBus;
        private readonly IBackgroundMusicService _backgroundMusicService;

        public MainMenuService(
            SignalBus signalBus,
            IBackgroundMusicService backgroundMusicService
        )
        {
            _signalBus = signalBus;
            _backgroundMusicService = backgroundMusicService;
        }

        public void Initialize()
        {
            _backgroundMusicService.Play();
            _signalBus.OpenWindow<MainMenuWindow>();
        }
    }
}