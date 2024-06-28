using System;
using DG.Tweening;
using KoboldUi.Element.Controller;
using Services.Input;
using UniRx;
using Zenject;

namespace Game.Ui.Abstract.AnyKey
{
    public abstract class APressAnyKeyController<TView> : AUiController<TView> where TView : APressAnyKeyView
    {
        [Inject] private IInputService _inputService;

        private Sequence _blinkAnimation;
        private bool _wasDelayPassed = false;
        private bool _wasButtonPressed = false;
        private IDisposable _timerDisposable;

        private bool CanHandleInput => _wasDelayPassed && IsOpened && !_wasButtonPressed;

        public override void Initialize()
        {
            _inputService.AnyKeyPressed.Subscribe(_ =>
            {
                if (!CanHandleInput)
                    return;
                
                _wasButtonPressed = true;
                OnAnyKeyPressed();
            }).AddTo(View);
        }

        protected abstract void OnAnyKeyPressed();

        protected override void OnOpen()
        {
            View.textContainer.gameObject.SetActive(false);

            _wasDelayPassed = false;
            
            _timerDisposable?.Dispose();
            _timerDisposable = Observable.Timer(TimeSpan.FromSeconds(View.appearDelay)).Subscribe(_ =>
            {
                _wasDelayPassed = true;
                View.textContainer.gameObject.SetActive(true);
                ShowBlinkAnimation();
            }).AddTo(View);
        }

        protected override void OnClose()
        {
            _timerDisposable?.Dispose();
            _blinkAnimation?.Kill();
            _wasDelayPassed = false;
        }

        private void ShowBlinkAnimation()
        {
            _blinkAnimation?.Kill();
            
            _blinkAnimation = DOTween.Sequence();
            _blinkAnimation
                .Append(View.textContainer.DOPunchScale(View.punchScale, View.punchDurationSeconds, 0, 0).SetEase(Ease.Linear))
                .SetLoops(-1, LoopType.Restart)
                .SetLink(View.textContainer.gameObject);
        }
    }
}