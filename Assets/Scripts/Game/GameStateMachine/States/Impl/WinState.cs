using UnityEngine;

namespace Game.GameStateMachine.States.Impl
{
    public class WinState : AState
    {
        protected override void HandleEnter()
        {
            Debug.Log("EnterWinState");
        }

        protected override void HandleExit()
        {
            Debug.Log("ExitWinState");
        }
    }
}