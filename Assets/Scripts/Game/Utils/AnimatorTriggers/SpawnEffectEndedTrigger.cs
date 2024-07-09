using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Game.Utils.AnimatorTriggers
{
    public class SpawnEffectEndedTrigger : ObservableStateMachineTrigger
    {
        private readonly ReactiveCommand _spawnEffectEnded = new();
        
        public IObservable<Unit> SpawnEffectEnded => _spawnEffectEnded;

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            _spawnEffectEnded.ForceExecute();
        }
    }
}