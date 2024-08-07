using Game.Ui.PauseWindow;
using KoboldUi.Utils;
using Services.Input;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.GameStateMachine.States.Impl
{
    public class PauseState : AState
    {
        private readonly IInputService _inputService;
        private readonly SignalBus _signalBus;

        public PauseState(
            IInputService inputService, 
            SignalBus signalBus
        )
        {
            _inputService = inputService;
            _signalBus = signalBus;
        }

        protected override void HandleEnter()
        {
            _inputService.SwitchToUiInput();
            _signalBus.OpenWindow<PauseWindow>();

            Time.timeScale = 0f;

            _inputService.CloseWindowPressed.Subscribe(_ => OnCloseWindowPressed()).AddTo(ActiveDisposable);
        }

        private void OnCloseWindowPressed()
        {
            GameStateMachine.Enter<GameState>();
        }

        protected override void HandleExit()
        {
            Time.timeScale = 1f;
        }
    }
}