using System;
using System.Collections.Generic;
using Game.GameStateMachine.States;
using Game.GameStateMachine.States.Impl;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.GameStateMachine.Impl
{
    public class GameStateMachine : IInitializable, IDisposable, IGameStateMachine
    {
        private readonly DiContainer _rootDiContainer;
        private DiContainer _diContainer;

        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly Dictionary<Type, IState> _states = new();

        private IState _currentState;

        public GameStateMachine(DiContainer rootDiContainer)
        {
            _rootDiContainer = rootDiContainer;
        }

        public void Initialize()
        {
            _diContainer = _rootDiContainer.CreateSubContainer();
            _diContainer.Bind<IGameStateMachine>().FromInstance(this).AsSingle();

            _states.Clear();

            AddState<StartState>();
            AddState<GameState>();
            AddState<WinState>();
            AddState<LoseState>();
            AddState<PauseState>();

            Enter<StartState>();
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

        public void Enter<TState>() where TState : IState
        {
            _currentState?.Exit();
            _currentState = _states[typeof(TState)];
            _currentState.Enter();
        }

        private void AddState<TState>() where TState : AState
        {
            var state = _diContainer.Instantiate<TState>();
            _compositeDisposable.Add(state);
            _states.Add(typeof(TState), state);
        }
    }
}