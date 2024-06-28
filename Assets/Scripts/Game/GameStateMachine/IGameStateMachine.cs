using Game.GameStateMachine.States;

namespace Game.GameStateMachine
{
    public interface IGameStateMachine
    {
        void Enter<TState>() where TState : IState;
    }
}