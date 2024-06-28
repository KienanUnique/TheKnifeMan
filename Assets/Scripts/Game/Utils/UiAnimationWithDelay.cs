using System;
using KoboldUi.Element.Animations;
using UniRx;
using UnityEngine;

namespace Game.Utils
{
    public class UiAnimationWithDelay : AUiAnimationBase
    {
        [SerializeField] private AUiAnimationBase animationBase;
        [SerializeField] private float appearDelay;
        [SerializeField] private float focusReturnDelay;
        [SerializeField] private float focusRemovedDelay;
        [SerializeField] private float disappearDelay;

        private IDisposable _waitDisposable;
        
        public override void Appear()
        {
            DoDelayAction(appearDelay, () => animationBase.Appear());
        }

        public override void AnimateFocusReturn()
        {
            DoDelayAction(focusReturnDelay, () => animationBase.AnimateFocusReturn());
        }

        public override void AnimateFocusRemoved()
        {
            DoDelayAction(focusRemovedDelay, () => animationBase.AnimateFocusRemoved());
        }

        public override void Disappear()
        {
            DoDelayAction(disappearDelay, () => animationBase.Disappear());
        }

        private void DoDelayAction(float delay, Action delayAction)
        {
            _waitDisposable?.Dispose();
            _waitDisposable = Observable.Timer(TimeSpan.FromSeconds(delay)).Subscribe(_ => delayAction.Invoke())
                .AddTo(this);
        }
    }
}