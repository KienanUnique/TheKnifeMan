using Game.Services.Pause;
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
        private readonly IPauseService _pauseService;

        public PauseState(
            IInputService inputService, 
            SignalBus signalBus,
            IPauseService pauseService
        )
        {
            _inputService = inputService;
            _signalBus = signalBus;
            _pauseService = pauseService;
        }

        protected override void HandleEnter()
        {
            _inputService.SwitchToUiInput();
            _signalBus.OpenWindow<PauseWindow>();

            Time.timeScale = 0f;

            _inputService.CloseWindowPressed.Subscribe(_ => OnCloseWindowPressed()).AddTo(ActiveDisposable);
            _pauseService.IsPaused.Subscribe(OnPause).AddTo(ActiveDisposable);
        }

        protected override void HandleExit()
        {
            _signalBus.CloseWindow();
            Time.timeScale = 1f;
        }

        private void OnCloseWindowPressed() => _pauseService.DisablePause();

        private void OnPause(bool isPaused)
        {
            if(isPaused)
                return;
            
            GameStateMachine.Enter<GameState>();
        }
    }
}