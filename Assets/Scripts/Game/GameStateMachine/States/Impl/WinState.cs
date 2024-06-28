using Game.Ui.WinWindow;
using KoboldUi.Utils;
using Services.Input;
using Zenject;

namespace Game.GameStateMachine.States.Impl
{
    public class WinState : AState
    {
        private readonly SignalBus _signalBus;
        private readonly IInputService _inputService;

        public WinState(
            SignalBus signalBus,
            IInputService inputService
        )
        {
            _signalBus = signalBus;
            _inputService = inputService;
        }

        protected override void HandleEnter()
        {
            _inputService.SwitchToAnyKeyInput();
            _signalBus.OpenWindow<WinWindow>();
        }

        protected override void HandleExit()
        {
            _inputService.SwitchToUiInput();
        }
    }
}