using KoboldUi.Utils;
using Zenject;

namespace Ui.MainMenu
{
    public class MainMenuOpener : IInitializable
    {
        private readonly SignalBus _signalBus;

        public MainMenuOpener(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.OpenWindow<MainMenuWindow>();
        }
    }
}