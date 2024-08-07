using KoboldUi.Element.Controller;
using KoboldUi.Utils;
using UniRx;
using Zenject;

namespace Game.Ui.PauseWindow.Pause
{
    public class PauseController : AUiController<PauseView>
    {
        private readonly SignalBus _signalBus;
        
        public override void Initialize()
        {
            View.continueButton.OnClickAsObservable().Subscribe(_ => OnContinueButtonPressed()).AddTo(View);
        }

        private void OnContinueButtonPressed()
        {
            _signalBus.CloseWindow();
        }
    }
}