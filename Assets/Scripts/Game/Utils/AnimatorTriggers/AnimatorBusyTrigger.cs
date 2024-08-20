using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Game.Utils.AnimatorTriggers
{
    public class AnimatorBusyTrigger : ObservableStateMachineTrigger
    {
        private readonly ReactiveCommand<bool> _animatorBusy = new();

        public IObservable<bool> OnAnimatorBusy => _animatorBusy;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _animatorBusy.Execute(true);
            base.OnStateEnter(animator, stateInfo, layerIndex);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            _animatorBusy.Execute(false);
        }
    }
}