using System;
using UniRx;
using Zenject;

namespace Game.GameStateMachine.States
{
    public abstract class AState : IState, IDisposable
    {
        [Inject] protected IGameStateMachine GameStateMachine;
        
        protected CompositeDisposable ActiveDisposable;

        public void Enter()
        {
            ActiveDisposable = new CompositeDisposable();
            HandleEnter();
        }

        public void Exit()
        {
            ActiveDisposable?.Dispose();
            HandleExit();
        }
        
        public void Dispose()
        {
            ActiveDisposable?.Dispose();
        }

        protected abstract void HandleEnter();
        protected abstract void HandleExit();
    }
}