using System.Collections.Generic;
using System.Linq;
using Game.Utils;
using ModestTree;
using Services.Input;
using UniRx;
using UnityEngine;

namespace Game.GameStateMachine.States.Impl
{
    public class StartState : AState
    {
        private readonly IInputService _inputService;
        private readonly List<INeedWaitInitializable> _needWaitInitializables;

        public StartState(
            IInputService inputService, 
            List<INeedWaitInitializable> needWaitInitializables
        )
        {
            _inputService = inputService;
            _needWaitInitializables = needWaitInitializables;
        }

        protected override void HandleEnter()
        {
            var observablesToWait = new List<IReactiveProperty<bool>>();

            foreach (var needWaitInitializable in _needWaitInitializables)
            {
                if (needWaitInitializable.IsInitilized.Value)
                    continue;

                observablesToWait.Add(needWaitInitializable.IsInitilized);
            }

            if (observablesToWait.IsEmpty())
            {
                GameStateMachine.Enter<GameState>();
                return;
            }

            observablesToWait.CombineLatest().Subscribe(values =>
            {
                if (values.All(value => value)) OnAllInitializeblesWaited();
            }).AddTo(ActiveDisposable);
            
            _inputService.SwitchToUiInput();
        }

        private void OnAllInitializeblesWaited()
        {
            GameStateMachine.Enter<GameState>();
        }

        protected override void HandleExit()
        {
            ActiveDisposable?.Dispose();
        }
    }
}