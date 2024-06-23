using System.Collections.Generic;
using System.Linq;
using Game.Utils;
using ModestTree;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.GameStateMachine.States.Impl
{
    public class StartState : AState
    {
        [Inject] private List<INeedWaitInitializable> _needWaitInitializables;

        protected override void HandleEnter()
        {
            var observablesToWait = new List<IReactiveProperty<bool>>();

            foreach (var needWaitInitializable in _needWaitInitializables)
            {
                if (needWaitInitializable.IsInitilized.Value)
                    continue;

                observablesToWait.Add(needWaitInitializable.IsInitilized);
            }

            Debug.Log($"StartState enter: {observablesToWait.Count}");

            if (observablesToWait.IsEmpty())
            {
                GameStateMachine.Enter<GameState>();
                return;
            }

            observablesToWait.CombineLatest().Subscribe(values =>
            {
                if (values.All(value => value)) OnAllInitializeblesWaited();
            }).AddTo(ActiveDisposable);
        }

        private void OnAllInitializeblesWaited()
        {
            GameStateMachine.Enter<GameState>();
        }

        protected override void HandleExit()
        {
        }
    }
}