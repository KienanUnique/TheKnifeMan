using System;
using UniRx;
using UnityEngine;

namespace Game.Utils.AnimatorTriggers
{
    public class SpawnMomentTrigger : MonoBehaviour
    {
        private readonly ReactiveCommand _onSpawnFrame = new();

        public IObservable<Unit> SpawnFramePlayed => _onSpawnFrame;

        public void InvokeTrigger()
        {
            _onSpawnFrame.Execute();
        }
    }
}