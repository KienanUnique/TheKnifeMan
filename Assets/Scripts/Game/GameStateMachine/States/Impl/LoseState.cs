using Game.Ui.LoseWindow;
using KoboldUi.Utils;
using Services.Input;
using Zenject;

namespace Game.GameStateMachine.States.Impl
{
    public class LoseState : AState
    {
        private readonly SignalBus _signalBus;
        private readonly IInputService _inputService;

        public LoseState(
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
            _signalBus.OpenWindow<LoseWindow>();
        }

        protected override void HandleExit()
        {
            _inputService.SwitchToUiInput();
        }
    }
}