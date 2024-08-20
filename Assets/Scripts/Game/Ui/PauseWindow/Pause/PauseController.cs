using Game.Services.Pause;
using KoboldUi.Element.Controller;
using UniRx;
using UnityEngine;

namespace Game.Ui.PauseWindow.Pause
{
    public class PauseController : AUiController<PauseView>
    {
        private readonly IPauseService _pauseService;

        public PauseController(
            IPauseService pauseService
        )
        {
            _pauseService = pauseService;
        }

        public override void Initialize()
        {
            View.continueButton.OnClickAsObservable().Subscribe(_ => OnContinueButtonPressed()).AddTo(View);
            View.exitButton.OnClickAsObservable().Subscribe(_ => OnExitButtonPressed()).AddTo(View);
        }

        private void OnExitButtonPressed() => Application.Quit();
        private void OnContinueButtonPressed() => _pauseService.DisablePause();
    }
}