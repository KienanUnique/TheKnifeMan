using System.Collections.Generic;
using System.Linq;
using Game.Utils;
using ModestTree;
using Services.Input;
using Services.Sound;
using UniRx;
using UnityEngine;

namespace Game.GameStateMachine.States.Impl
{
    public class StartState : AState
    {
        private readonly IInputService _inputService;
        private readonly List<INeedWaitInitializable> _needWaitInitializables;
        private readonly IBackgroundMusicService _backgroundMusicService;

        public StartState(
            IInputService inputService, 
            List<INeedWaitInitializable> needWaitInitializables,
            IBackgroundMusicService backgroundMusicService
        )
        {
            _inputService = inputService;
            _needWaitInitializables = needWaitInitializables;
            _backgroundMusicService = backgroundMusicService;
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
            _backgroundMusicService.Play();
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